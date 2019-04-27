using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Kraken : Enemy
{
    private void Attack(GameObject objectToAttack)
    {
        Debug.Log("Kraken attacking");
    }
}
