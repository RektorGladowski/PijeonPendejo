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

	private PigeonManager pigeonManager;
	private Rigidbody2D pigeonRb;

	private bool isMasterPigeon = false;
	public bool isFollowingMaster = false;
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

	public void SetInitialPositionAndSpeed(Vector3 pos, Vector3 vel)
	{
		pigeonRb.position = pos;

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

		pigeonRb.mass = speedStats.followerStats.mass;
		pigeonRb.drag = speedStats.followerStats.linearDrag;
		pigeonRb.angularDrag = speedStats.followerStats.angularDrag;

		followForce = speedStats.followerStats.maxFollowForce;
		maxFollowVelocity = speedStats.followerStats.maxFollowSpeed;
	}

	private void Update()
	{
		
        //suicide button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //to jest to złe miejsce
            Instantiate(bloodyExplosion, pigeonPosition.transform.position, Quaternion.identity);
            Debug.Log("FEEL IT YOU GOT IT EXPLOSIOOOON");
        }
		

        if (!isMasterPigeon)
		{
            if (!isFollowingMaster)
			{
				pigeonRb.velocity = initialVelocity;
				if (Vector2.Distance(transform.position, MasterPigeon.transform.position) <= initialAttractionDistance)
				{
					isFollowingMaster = true;
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
			return Seek(sum);
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

					case PigeonUnitCharacter.GroupTraveler:
						{
							currentForce = Seek(GetMasterPigeonPosition) + Align() + Cohesion();
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
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("Pigeon collided with enemy");
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
				Debug.LogWarning("Player should lose now");
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
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Instantiate(bloodyExplosion, pigeonPosition.transform.position, Quaternion.identity);
		}

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
	Individualist,
	Aligned,
	Cohesive,
	GroupTraveler, 
}
