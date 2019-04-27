using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    public float FlightSpeed = 25;
    public List<Transform> PatrollingPoints;

    private Transform currentDestination;
    private int currentDestinationIndex;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        currentDestination = PatrollingPoints[0];
        currentDestinationIndex = 0;

        rb.AddForce((currentDestination.position - transform.position) * FlightSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EaglePatrollingPoint"))
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
    }
}
