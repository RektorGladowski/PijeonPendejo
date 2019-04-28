using UnityEngine;

public class GameInitializer : MonoBehaviour
{
	public GameObject upgradetonPrefab;
	public GameObject banketonPrefab;

	private GameObject upgradetonObject;
	private GameObject banketonObject;

	private void Awake()
	{
		SpawnTonPrefabs();
	}

	private void SpawnTonPrefabs()
	{
		upgradetonObject = Instantiate(upgradetonPrefab, transform.position, transform.rotation) as GameObject;
		banketonObject = Instantiate(banketonPrefab, transform.position, transform.rotation) as GameObject;
	}
}
