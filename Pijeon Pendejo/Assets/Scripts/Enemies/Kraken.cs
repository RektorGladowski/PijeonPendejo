using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Kraken : Enemy
{
    override public void Activate()
    {
        Debug.Log("Kraken attacking");
    }
}
