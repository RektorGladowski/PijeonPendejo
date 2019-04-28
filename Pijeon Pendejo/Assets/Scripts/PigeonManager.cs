using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PigeonManager : MonoBehaviour
{
	[HideInInspector]
	public List<PigeonUnit> pigeonUnits = new List<PigeonUnit>();
	public static int AvailablePigeonFollowers { get; set; }
	public static int GetCoveredDistanceInMeters { get { return (int)(PigeonUnit.GetMasterPigeonPosition.x - startPosition.position.x); } }
	private static Transform startPosition;

	[Header("Starting info")]
	public GameObject pigeonUnitPrefab;
	public float initialFollowerSpeed = 10f;
	public float initialTeamRadius = 4f;

	[Header("Pigeon size randomization")]
	public float minPigeonSize = 0.7f;
	public float maxPigeonSize = 1.5f;

	[Header("Non followers spawner settings")]
	public float minSpawnHeight = -2f;
	public float maxSpawnHeight = 9f;
	public float verticalOffset = -10f;

	public float initialNonFollowerLeftSideSpeed = 20f;
	public float initialNonFollowerRightSideSpeed = 3f;
	public float minSpawnTime = 1f;
	public float maxSpawnTime = 5f;
	
	private CinemachineVirtualCamera cinemachineCamera;
	private TeamUpgrade teamStats;
	private float currentSpawnTime = 3f;
	private  float timer = 0f;

	


	private void Start()
    {
		cinemachineCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();

		//To do remove this
		RestartTheGame(transform);
    }

	public void RestartTheGame(Transform startingTransform)
	{
		startPosition = startingTransform;
		ResetPigeonData();
		SpawnInitialPigeons(startingTransform);
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

	private void SpawnInitialPigeons(Transform point)
	{
		// Spawn master pigeon
		GameObject masterPigeon = Instantiate(pigeonUnitPrefab, point.position, Quaternion.identity) as GameObject;
		masterPigeon.tag = "MainPigeon";
		PigeonUnit pUnit = masterPigeon.GetComponent<PigeonUnit>();

		pUnit.SetStats();
		pUnit.SetPigeonManagerRef(this);
		pigeonUnits.Add(pUnit);

		PigeonUnit.SetAsMasterPigeon(masterPigeon);

		// Spawn team pigeons
		for (int i = 1; i <= teamStats.numberOfStarterFollowers; i++)
		{
			GameObject pigeon = Instantiate(pigeonUnitPrefab, point.position + GetRandomV2Offset(initialTeamRadius), Quaternion.identity) as GameObject;
			pigeon.tag = "Pigeon";
			PigeonUnit p = pigeon.GetComponent<PigeonUnit>();

			Vector3 localScale = pigeon.transform.localScale;
			localScale *= Random.Range(minPigeonSize, maxPigeonSize);
			pigeon.transform.localScale = localScale;

			p.SetStats();
			p.SetPigeonManagerRef(this);
			p.SetInitialSpeed(new Vector3(initialFollowerSpeed, 0f, 0f));
			p.SetCharacterTrait(PigeonUnitCharacter.GroupTraveler);

			pigeonUnits.Add(p);
		}		
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer >= currentSpawnTime)
		{
			ResetTimerAndGetRandomSpawnTime();
			SpawnRandomPigeon();
		}
	}

	private void ResetTimerAndGetRandomSpawnTime()
	{
		timer = 0f;
		currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
	}

	private void SpawnRandomPigeon()
	{
		bool leftSidePigeon = Random.Range(0, 2) == 1;

		Vector3 randomSpawnPosition = Vector3.zero;
		randomSpawnPosition.x = PigeonUnit.GetMasterPigeonPosition.x + (leftSidePigeon ? verticalOffset : (-2 * verticalOffset));
		randomSpawnPosition.y = Random.Range(minSpawnHeight, maxSpawnHeight);

		GameObject pigeon = Instantiate(pigeonUnitPrefab, randomSpawnPosition, Quaternion.identity) as GameObject;
		PigeonUnit p = pigeon.GetComponent<PigeonUnit>();

		Vector3 localScale = pigeon.transform.localScale;
		localScale *= Random.Range(minPigeonSize, maxPigeonSize);
		pigeon.transform.localScale = localScale;

		p.SetStats();
		p.SetPigeonManagerRef(this);
		p.SetInitialSpeed(new Vector3((leftSidePigeon ? initialNonFollowerLeftSideSpeed : initialNonFollowerRightSideSpeed), 0f, 0f));
		p.SetCharacterTrait(PigeonUnitCharacter.GroupTraveler);

		pigeonUnits.Add(p);
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
