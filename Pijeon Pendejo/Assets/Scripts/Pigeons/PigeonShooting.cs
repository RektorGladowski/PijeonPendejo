using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonShooting : MonoBehaviour
{
    public List<GameObject> ShootableObjects;
    public float ShitInitialCooldown;
    public float ShitCooldown;
    public float ShitForce;
    
    private bool shitReady;
    private void Start()
    {
        StartCoroutine(CooldownShit(ShitInitialCooldown));
    }

    private Vector3 lastPosition;

    void Update()
    {
        if (Input.GetButtonDown("Deploy Shit"))
        {
            if (shitReady)
            {
                Vector3 movementVector = transform.position - lastPosition;
                DeployShit(movementVector);
                shitReady = false;
                StartCoroutine(CooldownShit(ShitCooldown));
            }
            else
            {
                //TODO: handle shit not being ready
                Debug.Log("shit not ready :(");
            }
        }

        lastPosition = transform.position;
    }

    private void DeployShit(Vector3 movementVector)
    {
        GameObject deployedShit = Instantiate(GetRandomShit(), transform.position, transform.rotation);
        deployedShit.GetComponent<Rigidbody2D>().AddForce(movementVector * Time.deltaTime * ShitForce);
    }

    private GameObject GetRandomShit()
    {
        return ShootableObjects[Random.Range(0, ShootableObjects.Count)];
    }
    
    IEnumerator CooldownShit(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        shitReady = true;
    }
}
