using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParalax : MonoBehaviour
{
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    public GameObject wave4;
    public float power, speed, offset1, offset2, offset3, offset4;
    private Vector3 wave1_pos, wave2_pos, wave3_pos, wave4_pos;
    // Start is called before the first frame update
    void Start()
    {
        power = 1.0f;
        speed = 2.0f;
        wave1_pos = wave1.transform.position;
        wave2_pos = wave2.transform.position;
        wave3_pos = wave3.transform.position;
        wave4_pos = wave4.transform.position;
        offset1 = Random.Range(-power, power);
        offset2 = Random.Range(-power, power);
        offset3 = Random.Range(-power, power);
        offset4 = Random.Range(-power, power);
    }

    // Update is called once per frame
    void Update()
    {
        wave1.transform.position = wave1_pos + new Vector3(Mathf.Sin(speed*Time.time + offset1) * power, Mathf.Cos(speed * Time.time) * power / 5, wave1_pos.z);
        wave2.transform.position = wave2_pos + new Vector3(Mathf.Sin(speed * Time.time + offset2) * power, Mathf.Cos(speed * Time.time) * power / 5, wave2_pos.z);
        wave3.transform.position = wave3_pos + new Vector3(Mathf.Sin(speed * Time.time + offset3) * power, Mathf.Cos(speed * Time.time) * power / 5, wave3_pos.z);
        wave4.transform.position = wave4_pos + new Vector3(Mathf.Sin(speed * Time.time + offset4) * power, Mathf.Cos(speed * Time.time) * power / 5, wave4_pos.z);

    }
}
