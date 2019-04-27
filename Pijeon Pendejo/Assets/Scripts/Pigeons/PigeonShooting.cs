using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonShooting : MonoBehaviour
{
    public GameObject ShootableObject;
    public float ShitInitialCooldown;
    public float ShitCooldown;
    
    private bool shitReady;
    private void Start()
    {
        StartCoroutine(CooldownShit(ShitInitialCooldown));
    }

    void Update()
    {
        if (Input.GetButtonDown("Deploy Shit"))
        {
            if (shitReady)
            {
                Debug.Log("S H I T !!!");
                DeployShit();
                shitReady = false;
                StartCoroutine(CooldownShit(ShitCooldown));
            }
            else
            {
                //TODO: handle shit not being ready
                Debug.Log("shit not ready :(");
            }
            
        }
    }

    private void DeployShit()
    {
        Instantiate(ShootableObject, transform.position, transform.rotation);
    }
    
    IEnumerator CooldownShit(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        shitReady = true;
    }
}
