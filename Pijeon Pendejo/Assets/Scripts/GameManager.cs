﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PigeonAtStart;
    public GameObject BloodyExplosion;
    public GameObject EnemySpawner;
    public PigeonManager pigeonManager;
    public GameObject UpgradePanel;
    public GameObject GameOverScreen;
    public TextMeshProUGUI ScoreText;

    public static bool GameStarted; //Turns false after first space press
    public static bool GameEnded;
    public static int PigeonsReceived;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        GameStarted = false;
        GameEnded = false;
        PigeonsReceived = 0;
    }

    void Update()
    {
        if (!GameStarted && Input.GetButtonDown("Pigeon Thrust"))
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
        else if (GameEnded)
        {
            if(GameOverScreen.active == false)
            {
                SetGameOverScreen();
            }
            if (Input.GetButtonDown("Pigeon Thrust"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void SetGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        ScoreText.text = "SCORE: " + PigeonsReceived;
    }
}
