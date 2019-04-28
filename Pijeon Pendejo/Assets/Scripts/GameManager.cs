using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PigeonAtStart;
    public GameObject BloodyExplosion;
    public GameObject EnemySpawner;
    public PigeonManager pigeonManager;

    public static bool GameStarted; //Turns false after first space press

    // Update is called once per frame
    void Update()
    {
        if(!GameStarted && Input.GetButtonDown("Pigeon Thrust"))
        {
            GameStarted = true;
            if (BloodyExplosion)
            {
                Instantiate(BloodyExplosion, PigeonAtStart.transform.position, Quaternion.identity);
            }
            Destroy(PigeonAtStart);

            pigeonManager.gameObject.SetActive(true);
            EnemySpawner.SetActive(true);
            pigeonManager.RestartTheGame(PigeonAtStart.transform);
        }
    }

}
