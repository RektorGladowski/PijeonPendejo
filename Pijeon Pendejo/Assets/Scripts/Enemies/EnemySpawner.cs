using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables._Definitions;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("The distance between enemies spawn points")]
    public int Distance = 10;
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

    private void Start()
    {
        SpawnEnemy(new Vector3(0, 0, -5));
    }

    private void Update()
    {
        
    }

    public void SpawnEnemy(Vector3 position)
    {
        // Sky prefabs spawning
        int rand = Random.Range(1, 100);
        Debug.Log("Spawning enemies with rand " + rand);

        if (rand <= 65)
        {
            return;
        }
        else if (rand <= 75 && Hawk)
        {
            Instantiate(Hawk);
        }
        else if (rand <= 85 && Eagle)
        {
            Instantiate(Eagle);
        }
        else if (rand <= 95 && Drone)
        {
            Instantiate(Drone);
        }
        else if (Blimp)
        {
            Instantiate(Blimp);
        }

        // Ground prefabs spawning
        rand = Random.Range(1, 100);

        if (rand <= 55)
        {
            return;
        }
        else if (rand <= 75 && Kraken)
        {
            Instantiate(Kraken);
        }
        else if (rand <= 95 && Narwal)
        {
            Instantiate(Narwal);
        }
        else if (Ship)
        {
            Instantiate(Ship);
        }

        GameObject obj = Instantiate(Kraken);
        //obj.transform.position = Hawk.transform.position;
    }
}
