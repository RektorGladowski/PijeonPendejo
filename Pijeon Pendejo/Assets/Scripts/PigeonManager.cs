using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PigeonManager : MonoBehaviour
{
	[HideInInspector]
	public List<PigeonUnit> pigeonUnits = new List<PigeonUnit>();
	
	public int AvailablePigeonFollowers { get; set; }
	

	public GameObject pigeonUnitPrefab;

	public float spawnTimer = 0.5f;
	public float initialSpeed = 10f;

	private float timer = 0f;

	private CinemachineVirtualCamera cinemachineCamera;
	private TeamUpgrade teamStats;
	

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
}
