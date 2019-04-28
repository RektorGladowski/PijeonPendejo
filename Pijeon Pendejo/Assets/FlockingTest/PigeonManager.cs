using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonManager : MonoBehaviour
{
	[HideInInspector]
	public List<PigeonUnit> pigeonUnits = new List<PigeonUnit>();
	public int AvailablePigeonFollowers { get; set; }

	public GameObject pigeonUnitPrefab;
	public float spawnTimer = 0.5f;
	public float initialSpeed = 10f;

	private float timer = 0f;
	

	private void Start()
    {
		AvailablePigeonFollowers = 0;
    }

	private void Update()
	{
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
	}
}
