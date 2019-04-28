using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonUnit : MonoBehaviour
{
	private static GameObject MasterPigeon;
    
	public static Vector3 GetMasterPigeonPosition { get { return MasterPigeon.transform.position; } }
	public static Transform GetMasterPigeonTransform { get { return MasterPigeon.transform; } }

    public GameObject bloodyExplosion;
    public GameObject pigeonPosition;

	[Header("Follower Stats")]
	public float initialAttractionDistance = 15f;
	public float neighbourhoodDistance = 25f;
	private float followForce = 50f;
	private float maxFollowVelocity = 14f;
	public float rotationSpeed = 90f;

	[Header("Special options")]
	[Range(0f, 100f)] public float behaviourChangeChance = 5f;
	public float alignmentWeight = 1f;
	public float separationWeight = 1f;
	public float cohesionWeight = 1f;

	private PigeonManager pigeonManager;
	private Rigidbody2D pigeonRb;

	private bool isMasterPigeon = false;
	[HideInInspector] public bool isFollowingMaster = false;
	private PigeonUnitCharacter characterTrait = PigeonUnitCharacter.GroupTraveler;

	private Vector2 goalPosition = Vector2.zero;
	private Vector2 currentForce = Vector2.zero;

	private SpeedUpgrade speedStats;
	private Vector3 initialVelocity;

	#region Setting stuff
	public static void SetAsMasterPigeon(GameObject go)
	{
		MasterPigeon = go;
		MasterPigeon.GetComponent<PigeonUnit>().SetThisInstanceAsMasterPigeon();
	}

	public void SetThisInstanceAsMasterPigeon()
	{
		isMasterPigeon = true;
		
		if (gameObject.GetComponent<MasterPigeonMovement>() == null)
		{
			gameObject.AddComponent<MasterPigeonMovement>();
		}

		pigeonManager.MasterPigeonWasChosen();
	}

	public void SetPigeonManagerRef(PigeonManager pmRef)
	{
		pigeonManager = pmRef;
	}

	public void SetInitialSpeed(Vector3 vel)
	{
		initialVelocity = vel;
		pigeonRb.velocity = vel;
	}

	public void SetCharacterTrait(PigeonUnitCharacter trait)
	{
		characterTrait = trait;
	}
	#endregion

	public void SetStats()
	{
		pigeonRb = GetComponent<Rigidbody2D>();
		speedStats = Upgradeton.instance.GetSpeedStats();

		pigeonRb.mass = Random.Range(speedStats.followerStats.minMass, speedStats.followerStats.maxMass);
		pigeonRb.drag = Random.Range(speedStats.followerStats.minLinearDrag, speedStats.followerStats.maxLinearDrag);
		pigeonRb.angularDrag = speedStats.followerStats.angularDrag;

		followForce = Random.Range(speedStats.followerStats.minFollowForce, speedStats.followerStats.maxFollowForce);
		maxFollowVelocity = speedStats.followerStats.maxFollowSpeed;
	}

	private void Update()
	{		
		/*
        // Suicide button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //to jest to złe miejsce
            Instantiate(bloodyExplosion, pigeonPosition.transform.position, Quaternion.identity);
            Debug.Log("FEEL IT YOU GOT IT EXPLOSIOOOON");
        }
		*/

        if (!isMasterPigeon)
		{
            if (!isFollowingMaster)
			{
				pigeonRb.velocity = initialVelocity;

				if (Vector2.Distance(transform.position, MasterPigeon.transform.position) <= initialAttractionDistance)
				{
					isFollowingMaster = true;
					gameObject.tag = "Pigeon";
					pigeonManager.AvailablePigeonFollowers += 1;
				}
			}
			else
			{
				Flock();
				RotateBody();
			}
		}
	}

	#region Flocking
	private Vector2 Seek(Vector2 target)
	{
		return (target - (Vector2)transform.position);
	}

	public Vector2 GetVelocity()
	{
		return pigeonRb.velocity;
	}

	private void ApplyForce(Vector2 force)
	{
		Vector3 forceToApply = new Vector3(force.x, force.y, 0);
		forceToApply = forceToApply.normalized;
		forceToApply *= followForce;

		pigeonRb.AddForce(forceToApply);
		pigeonRb.velocity = Vector2.ClampMagnitude(pigeonRb.velocity, maxFollowVelocity);

		Debug.DrawRay(transform.position, force, Color.white);
	}

	private Vector2 Align()
	{
		Vector2 sum = Vector2.zero;
		int count = 0;

		foreach (PigeonUnit pUnit in pigeonManager.pigeonUnits)
		{
			if (pUnit == this)
			{
				continue;
			}

			if (Vector2.Distance(transform.position, pUnit.transform.position) < neighbourhoodDistance)
			{
				sum += pUnit.GetVelocity();
				count++;
			}
		}

		if (count > 0)
		{
			sum /= count;
			Vector2 steer = sum - pigeonRb.velocity;
			steer = steer.normalized;
			return steer;
		}

		return Vector2.zero;
	}

	private Vector2 Cohesion()
	{
		Vector2 sum = Vector2.zero;
		int count = 0;

		foreach (PigeonUnit pUnit in pigeonManager.pigeonUnits)
		{
			if (pUnit == this)
			{
				continue;
			}

			if (Vector2.Distance(transform.position, pUnit.transform.position) < neighbourhoodDistance)
			{
				sum += (Vector2)pUnit.transform.position;
				count++;
			}
		}

		if (count > 0)
		{
			sum /= count;
			sum = Seek(sum);
			sum = sum.normalized;
			return sum;
		}

		return Vector2.zero;
	}

	private Vector2 Separation()
	{
		Vector2 sum = Vector2.zero;
		int count = 0;

		foreach (PigeonUnit pUnit in pigeonManager.pigeonUnits)
		{
			if (pUnit == this)
			{
				continue;
			}

			if (Vector2.Distance(transform.position, pUnit.transform.position) < neighbourhoodDistance)
			{
				Vector2 distanceVector = pUnit.transform.position - transform.position;
				sum += distanceVector;
				count++;
			}
		}

		if (count > 0)
		{
			sum /= count;
			sum *= -1;
			sum = sum.normalized;
			return sum;
		}

		return Vector2.zero;
	}

	private void Flock()
	{
		if (isFollowingMaster)
		{
			if (Random.Range(0f, (100f / behaviourChangeChance)) >= 1f)
			{
				switch (characterTrait)
				{
					case PigeonUnitCharacter.GroupTraveler:
						{
							currentForce = Seek(GetMasterPigeonPosition) + (Align() * alignmentWeight) + (Cohesion() * cohesionWeight) + (Separation() * separationWeight);
							currentForce = currentForce.normalized;
							break;
						}

					case PigeonUnitCharacter.Individualist:
						{
							currentForce = new Vector2(Random.Range(0.01f, 1f), Random.Range(0.01f, 1f));
							currentForce = currentForce.normalized;
							break;
						}

					case PigeonUnitCharacter.Aligned:
						{
							currentForce = Seek(GetMasterPigeonPosition) + Align();
							currentForce = currentForce.normalized;
							break;
						}

					case PigeonUnitCharacter.Cohesive:
						{
							currentForce = Seek(GetMasterPigeonPosition) + Cohesion();
							currentForce = currentForce.normalized;
							break;
						}
				}
			}
			else
			{
				currentForce = Seek(GetMasterPigeonPosition);
				currentForce = currentForce.normalized;
			}

			ApplyForce(currentForce);		
		}
	}

	private void RotateBody()
	{
		Quaternion targetQ = MasterPigeon.transform.rotation;
		transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, rotationSpeed * Time.deltaTime);
	}
	#endregion

	#region Please commit not existing
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Death"))
		{
			PrepareToDie();
		}
	}

	private void PrepareToDie()
	{
		pigeonManager.RemovePigeonUnit(this);

		if (isMasterPigeon)
		{
			if (FindNewMasterPigeon())
			{
				ExplodeNicely();
			}
			else
			{
				Debug.LogError("Player should lose now, fucking put something here");
				// TODO Stop the game and show game over screen
			}
		}
		else
		{
			if (isFollowingMaster)
			{
				pigeonManager.AvailablePigeonFollowers -= 1;
			}

			ExplodeNicely();
		}		
	}

	private bool FindNewMasterPigeon()
	{
		if (pigeonManager.AvailablePigeonFollowers == 0)
		{
			return false;
		}

		// Remove follower from counter since it is certain that we find new master pigeon
		pigeonManager.AvailablePigeonFollowers -= 1;

		// Pick random follower as a new Master pigeon
		PigeonUnit newMaster;

		do {
			newMaster = pigeonManager.pigeonUnits[Random.Range(0, pigeonManager.pigeonUnits.Count)];
		} while (newMaster == this);

		SetAsMasterPigeon(newMaster.gameObject);

	
		return true;
	}

	private void ExplodeNicely()
	{
		Instantiate(bloodyExplosion, pigeonPosition.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void ForceKillPigeon()
	{
		ExplodeNicely();
	}
	#endregion
}

public enum PigeonUnitCharacter
{
	Aligned,
	Cohesive,
	GroupTraveler,
	Individualist,
}
