using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Hawk : Enemy
{
    override public void Activate()
    {
        Debug.Log("Hawk attacking");
    }
}
