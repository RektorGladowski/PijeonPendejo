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
		if (Upgradeton.instance == null)
		{
			upgradetonObject = Instantiate(upgradetonPrefab, transform.position, transform.rotation) as GameObject;
			DontDestroyOnLoad(upgradetonObject);
		}
		
		if (PlayerBanketon.instance == null)
		{
			banketonObject = Instantiate(banketonPrefab, transform.position, transform.rotation) as GameObject;
			DontDestroyOnLoad(banketonObject);
		}
		
	}
}
