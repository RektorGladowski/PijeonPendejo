using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PigeonManager : MonoBehaviour
{
	[HideInInspector]
	public List<PigeonUnit> pigeonUnits = new List<PigeonUnit>();
	public int AvailablePigeonFollowers { get; set; }

	[Header("Starting info")]
	public GameObject pigeonUnitPrefab;
	public float initialTeamRadius = 4f;

	[Header("Pigeon size randomization")]
	public float minPigeonSize = 0.7f;
	public float maxPigeonSize = 1.5f;

	[Header("Non followers spawner settings")]
	public float spawnTimer = 0.5f;
	public float initialSpeed = 10f;

	private CinemachineVirtualCamera cinemachineCamera;
	private TeamUpgrade teamStats;
	private float timer = 0f;


	private void Start()
    {
		cinemachineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
		RestartTheGame();
    }

	public void RestartTheGame()
	{
		ResetPigeonData();
		SpawnInitialPigeons();

	}

	private void ResetPigeonData()
	{
		foreach (PigeonUnit pUnit in pigeonUnits)
		{
			pUnit.ForceKillPigeon();
		}

		pigeonUnits.Clear();
		AvailablePigeonFollowers = 0;

		teamStats = Upgradeton.instance.GetTeamStats();
	}

	private void SpawnInitialPigeons()
	{
		// Spawn master pigeon
		GameObject masterPigeon = Instantiate(pigeonUnitPrefab, transform.position, Quaternion.identity) as GameObject;
		PigeonUnit pUnit = masterPigeon.GetComponent<PigeonUnit>();

		pUnit.SetStats();
		pUnit.SetPigeonManagerRef(this);
		pigeonUnits.Add(pUnit);

		PigeonUnit.SetAsMasterPigeon(masterPigeon);

		// Spawn team pigeons
		for (int i = 1; i <= teamStats.numberOfStarterFollowers; i++)
		{
			GameObject pigeon = Instantiate(pigeonUnitPrefab, transform.position + GetRandomV2Offset(initialTeamRadius), Quaternion.identity) as GameObject;
			PigeonUnit p = pigeon.GetComponent<PigeonUnit>();

			Vector3 localScale = pigeon.transform.localScale;
			localScale *= Random.Range(minPigeonSize, maxPigeonSize);
			pigeon.transform.localScale = localScale;

			p.SetStats();
			p.SetPigeonManagerRef(this);
			p.SetInitialPositionAndSpeed(transform.position, new Vector3(initialSpeed, 0f, 0f));
			p.SetCharacterTrait(PigeonUnitCharacter.GroupTraveler);//(PigeonUnitCharacter)Random.Range(0, 3));

			pigeonUnits.Add(p);
		}		
	}

	private void Update()
	{
		/*
		timer += Time.deltaTime;

		if (timer > spawnTimer)
		{
			timer = 0f;
			GameObject pigeon = Instantiate(pigeonUnitPrefab, transform.position, Quaternion.identity) as GameObject;
			PigeonUnit pUnit = pigeon.GetComponent<PigeonUnit>();

			pUnit.SetStats();
			pUnit.SetPigeonManagerRef(this);
			pUnit.SetInitialPositionAndSpeed(transform.position, new Vector3(initialSpeed, 0f, 0f));
			pUnit.SetCharacterTrait((PigeonUnitCharacter)Random.Range(0, 4));

			pigeonUnits.Add(pUnit);
		}
		*/
	}

	public void RemovePigeonUnit(PigeonUnit unit)
	{
		if (pigeonUnits.Contains(unit))
		{
			pigeonUnits.Remove(unit);
		}
	}

	public void MasterPigeonWasChosen()
	{
		cinemachineCamera.Follow = PigeonUnit.GetMasterPigeonTransform;
	}

	private Vector3 GetRandomV2Offset(float radius)
	{
		Vector3 offset = Vector3.zero;

		offset.x = Random.Range(-radius, radius);
		offset.y = Random.Range(-radius, radius);

		return offset;
	}
}
