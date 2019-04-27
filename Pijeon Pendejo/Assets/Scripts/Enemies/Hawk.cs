using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Hawk : Enemy
{
    public float FlightSpeed = 25;
    public float AttackForce = 50;
    public List<Transform> PatrollingPoints;

    private Transform currentDestination;
    private int currentDestinationIndex;

    private void Start()
    {
        currentDestination = PatrollingPoints[0];
        currentDestinationIndex = 0;

        rb.AddForce((currentDestination.position - transform.position) * FlightSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon"))
        {
            StartCoroutine(Attack(collision.gameObject, 1));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PatrollingPoint"))
        {
            if (currentDestinationIndex + 1 != PatrollingPoints.Count)
            {
                currentDestinationIndex++;
            }
            else
            {
                currentDestinationIndex = 0;
            }

            currentDestination = PatrollingPoints[currentDestinationIndex];

            rb.velocity = new Vector2(0, 0);
            rb.AddForce((currentDestination.position - transform.position) * FlightSpeed);
        }
    }

    private IEnumerator Attack(GameObject objectToAttack, float time)
    {
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(time);
        rb.AddForce((objectToAttack.transform.position - transform.position) * AttackForce);

        Debug.Log("Hawk attacking");
    }
}
