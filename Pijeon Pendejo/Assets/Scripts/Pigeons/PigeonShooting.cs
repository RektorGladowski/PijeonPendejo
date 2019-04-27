using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonShooting : MonoBehaviour
{
    public List<GameObject> ShootableObjects;
    public Animator PigeonAnimator;
    public float ShitInitialCooldown;
    public float ShitCooldown;
    
    private bool shitReady;
    private Rigidbody2D pigeonRb;
    private static readonly int ShitCooledDown = Animator.StringToHash("ShitCooledDown");

    private void Start()
    {
        pigeonRb = gameObject.GetComponentInParent<Rigidbody2D>();
        StartCoroutine(CooldownShit(ShitInitialCooldown));
    }

    void Update()
    {
        if (Input.GetButtonDown("Deploy Shit"))
        {
            if (shitReady)
            {
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
        GameObject deployedShit = Instantiate(GetRandomShit(), transform.position, transform.rotation);
        Rigidbody2D shitRb = deployedShit.GetComponent<Rigidbody2D>();
        shitRb.velocity = pigeonRb.velocity;
        shitRb.angularVelocity = pigeonRb.angularVelocity;
    }

    private GameObject GetRandomShit()
    {
        return ShootableObjects[Random.Range(0, ShootableObjects.Count)];
    }
    
    IEnumerator CooldownShit(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        shitReady = true;
        PigeonAnimator.SetTrigger(ShitCooledDown);
    }
}
