using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PigeonAtStart;
    public GameObject BloodyExplosion;
    public GameObject EnemySpawner;
    public PigeonManager pigeonManager;

    public static bool GameStarted; //Turns false after first space press
    public static bool GameEnded;

    private void Start()
    {
        GameStarted = false;
        GameEnded = false;
    }

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
        else if(GameEnded && Input.GetButtonDown("Pigeon Thrust"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
