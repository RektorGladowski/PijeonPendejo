using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonShooting : MonoBehaviour
{
    public List<GameObject> ShootableObjects;
    public Animator PigeonAnimator;

    public float MaxShitDelay;
    
    private bool shitReady;
    private Rigidbody2D pigeonRb;
    private static readonly int ShitCooledDown = Animator.StringToHash("ShitCooledDown");
	private ShitUpgrade shitStats;

    private void Start()
    {
        pigeonRb = gameObject.GetComponentInParent<Rigidbody2D>();

		shitStats = Upgradeton.instance.GetShitStats();
        StartCoroutine(CooldownShit(shitStats.shitInitialCooldown));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Deploy Shit"))
        {
            if (shitReady)
            {
                StartCoroutine(DeployShit(Random.Range(0.0f, MaxShitDelay)));
                shitReady = false;
                StartCoroutine(CooldownShit(shitStats.shitCooldown));
            }
            else
            {
                //TODO: handle shit not being ready
                Debug.Log("shit not ready :(");
            }
        }
    }

    private IEnumerator DeployShit(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject deployedShit = Instantiate(GetRandomShit(), transform.position, transform.rotation);
		Vector3 localScale = deployedShit.transform.localScale;
		deployedShit.transform.localScale = localScale * shitStats.shitSizeMultiplier;

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
