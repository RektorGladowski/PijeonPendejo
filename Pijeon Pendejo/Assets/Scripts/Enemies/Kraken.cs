﻿using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Kraken : Enemy
{
    public float AttackSpeed = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon"))
        {
            Attack(AttackSpeed);
        }
    }

    private void Attack(float time)
    {
        transform.DOMove(new Vector2(transform.position.x, 0), time);

        Debug.Log("Kraken attacking");
    }
}
