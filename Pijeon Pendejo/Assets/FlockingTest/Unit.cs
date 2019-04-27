using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public AllUnits manager;
	public Vector2 location = Vector2.zero;
	public Vector2 velocity;

	Vector2 goalPos = Vector2.zero;
	Vector2 currentForce;
	public bool ControlledByPlayer;

	private void Start()
	{
		if (!ControlledByPlayer)
		{ 
		velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
		location = new Vector2(transform.position.x, transform.position.y);
	}
	}

	private Vector2 Seek(Vector2 target)
	{
		return (target - location);
	}

	private void ApplyForce(Vector2 f)
	{
		Vector3 force = new Vector3(f.x, f.y, 0);
		force = force.normalized;
		force *= manager.maxForce;

		if (force.magnitude > manager.maxForce)
		{
			force = force.normalized;
			force *= manager.maxForce;
		}

		GetComponent<Rigidbody2D>().AddForce(force);

		if (GetComponent<Rigidbody2D>().velocity.magnitude > manager.maxVelocity)
		{
			GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized;
			GetComponent<Rigidbody2D>().velocity *= manager.maxVelocity;
		}

		Debug.DrawRay(transform.position, force, Color.white);
	}

	private Vector2 Align()
	{
		float neighbourDist = manager.neighbourDistance;
		Vector2 sum = Vector2.zero;
		int count = 0;

		foreach (GameObject other in manager.units)
		{
			if (other == gameObject)
			{
				continue;
			}

			float d = Vector2.Distance(location, other.GetComponent<Unit>().location);

			if (d < neighbourDist)
			{
				sum += other.GetComponent<Unit>().velocity;
				count++;
			}
		}

		if (count > 0)
		{
			sum /= count;
			Vector2 steer = sum - velocity;
			return steer;
		}

		return Vector2.zero;
	}

	private Vector2 Cohesion()
	{
		float neighbourDist = manager.neighbourDistance;
		Vector2 sum = Vector2.zero;
		int count = 0;

		foreach(GameObject other in manager.units)
		{
			if (other == gameObject)
			{
				continue;
			}

			float d = Vector2.Distance(location, other.GetComponent<Unit>().location);

			if (d < neighbourDist)
			{
				sum += other.GetComponent<Unit>().location;
				count++;
			}
		}

		if (count > 0)
		{
			sum /= count;
			return Seek(sum);
		}

		return Vector2.zero;
	}

	private void Flock()
	{
		location = transform.position;
		velocity = GetComponent<Rigidbody2D>().velocity;

		if (manager.obedient && Random.Range(0,50) <= 1)
		{
			Vector2 ali = Align();
			Vector2 coh = Cohesion();
			Vector2 gl;

			if (manager.seekGoal)
			{
				gl = Seek(goalPos);
				currentForce = gl + ali + coh;
			}
			else
			{
				currentForce = ali + coh;
			}

			currentForce = currentForce.normalized;
		}

		if (manager.GetComponent<AllUnits>().willful && Random.Range(0,50) <= 1)
		{
			if (Random.Range(0, 50) < 1)
			{
				currentForce = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
				currentForce = currentForce.normalized;
			}
		}

		ApplyForce(currentForce);
	}

	private void Update()
	{
		if (!ControlledByPlayer)
		{
			Flock();
			goalPos = manager.transform.position;
		}
		
	}

}
