using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narwal : Enemy
{
    private void Attack(GameObject objectToAttack)
    {
        Debug.Log("Narwal attacking");
    }
}
