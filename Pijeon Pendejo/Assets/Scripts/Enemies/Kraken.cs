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

    private void Attack(float time)
    {
        transform.DOMove(new Vector3(transform.position.x, 0, -3), time);

        Debug.Log("Kraken attacking");
    }
}
