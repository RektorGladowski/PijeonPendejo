using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Drone : Enemy
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
        if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            StartCoroutine(Attack(collision.gameObject, 0.5f));
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
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

            currentDestination = PatrollingPoints[currentDestinationIndex];

            rb.velocity = new Vector2(0, 0);
            rb.AddForce((currentDestination.position - transform.position) * FlightSpeed);
        }
        else if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            if (Collider)
                Collider.enabled = false;
        }
    }

    private IEnumerator Attack(GameObject objectToAttack, float time)
    {
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(time);
        if (objectToAttack)
        {
            transform.Rotate(new Vector3(0, 0, Vector3.SignedAngle(transform.position, objectToAttack.transform.position, new Vector3(0, 0, 1))), Space.World);
            rb.AddForce((objectToAttack.transform.position - transform.position) * AttackForce);
        }

        Debug.Log("Drone attacking");
    }
}
