using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Kraken : Enemy
{
    public float AttackSpeed = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            Attack(AttackSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            if (Collider)
                Collider.enabled = false;
        }
    }

    private void Attack(float time)
    {
        transform.DOMove(new Vector3(transform.position.x, -0.3f, -3), time);

        Debug.Log("Kraken attacking");
    }
}
