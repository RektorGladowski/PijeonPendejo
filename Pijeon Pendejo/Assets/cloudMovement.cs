﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMovement : MonoBehaviour
{
    public GameObject cloud1, cloud2, cloud3;
    public float randomX, randomY;
    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(0, 3);

        switch (choice)
        {
            case 0:
                break;

            case 1:
                cloud1.transform.localPosition = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), 0);
                break;

            case 2:
                cloud1.transform.localPosition = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), 0);
                cloud2.transform.localPosition = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), 0);


                break;

            case 3:
                cloud1.transform.localPosition = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), 0);
                cloud2.transform.localPosition = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), 0);
                cloud3.transform.localPosition = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), 0);
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
