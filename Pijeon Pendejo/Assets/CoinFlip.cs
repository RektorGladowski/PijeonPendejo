using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, 100);
        if (random < 75) transform.position += new Vector3(0, 400, 0);
        else transform.position += new Vector3(0, Random.Range(-3, 3), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
