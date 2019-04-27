using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnits : MonoBehaviour
{
	public GameObject[] units;
	public GameObject unitPrefab;
	public int numUnits = 100;
	public Vector3 range = new Vector3(5, 5, 5);

	[Range(0, 200)]
	public int neighbourDistance = 50;

	[Range(0, 500f)]
	public float maxForce = 0.5f;

	[Range(0, 20f)]
	public float maxVelocity = 2.0f;

	public bool willful, obedient, seekGoal;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, range * 2);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, 0.2f);
	}

	void Start()
    {
		units = new GameObject[numUnits];

		for (int i = 0; i < numUnits; i++)
		{
			Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), 0);
			units[i] = Instantiate(unitPrefab, transform.position + unitPos, Quaternion.identity) as GameObject;
			units[i].GetComponent<Unit>().manager = this;
		}
    }

}
