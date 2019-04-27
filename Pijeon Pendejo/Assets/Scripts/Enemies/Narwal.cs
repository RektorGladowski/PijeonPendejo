using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narwal : Enemy
{
    override public void Activate()
    {
        Debug.Log("Narwal attacking");
    }
}
