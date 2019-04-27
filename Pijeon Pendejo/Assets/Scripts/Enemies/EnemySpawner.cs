using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables._Definitions;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("The distance between enemies spawn points")]
    public int Distance = 20;
    public GOVariable MainPigeon;

    [Header("Sky prefabs")]
    public GameObject Hawk;
    public GameObject Eagle;
    public GameObject Drone;
    public GameObject Blimp;

    [Header("Ground prefabs")]
    public GameObject Kraken;
    public GameObject Narwal;
    public GameObject Ship;

    private float lastSpawn = 0;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (PigeonUnit.GetMasterPigeonPosition().x - lastSpawn >= Distance / 2)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject spawnedObject;

        // Sky prefabs spawning
        int rand = Random.Range(1, 100);

        if (rand <= 65)
        {
            return;
        }
        else if (rand <= 75 && Hawk)
        {
            spawnedObject = Instantiate(Hawk);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));
        }
        else if (rand <= 85 && Eagle)
        {
            spawnedObject = Instantiate(Eagle);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));
        }
        else if (rand <= 95 && Drone)
        {
            spawnedObject = Instantiate(Drone);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));
        }
        else if (Blimp)
        {
            spawnedObject = Instantiate(Blimp);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));

        }

        // Ground prefabs spawning
        rand = Random.Range(1, 100);

        if (rand <= 55)
        {
            return;
        }
        else if (rand <= 75 && Kraken)
        {
            spawnedObject = Instantiate(Kraken);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));
        }
        else if (rand <= 95 && Narwal)
        {
            spawnedObject = Instantiate(Narwal);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));
        }
        else if (Ship)
        {
            spawnedObject = Instantiate(Ship);
            spawnedObject.transform.Translate(new Vector3(lastSpawn + Distance, 0, 0));
        }

        lastSpawn += Distance;
    }
}
