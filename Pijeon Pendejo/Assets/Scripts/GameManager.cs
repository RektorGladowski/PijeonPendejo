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
    public GameObject UpgradePanel;

    public static bool GameStarted; //Turns false after first space press
    public static bool GameEnded;

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
            UpgradePanel.SetActive(false);
            pigeonManager.RestartTheGame(PigeonAtStart.transform);
        }
        else if(GameEnded && Input.GetButtonDown("Pigeon Thrust"))
        {
            pigeonManager.RestartTheGame(PigeonAtStart.transform);
        }
    }
}
