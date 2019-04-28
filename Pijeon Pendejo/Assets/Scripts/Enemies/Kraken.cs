using UnityEngine;
using DG.Tweening;
using System.Collections;

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
        StartCoroutine(HideKraken(time + 1));

        Debug.Log("Kraken attacking");
    }

    private IEnumerator HideKraken(float time)
    {
        yield return new WaitForSeconds(time);
        transform.DOMove(new Vector3(transform.position.x, -8, -3), time);
    }
}
