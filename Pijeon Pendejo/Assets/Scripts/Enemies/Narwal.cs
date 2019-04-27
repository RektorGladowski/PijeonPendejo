using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narwal : Enemy
{
    public float AttackForce = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            StartCoroutine(Attack(collision.gameObject, 0.1f));
        }
    }

    private IEnumerator Attack(GameObject objectToAttack, float time)
    {
        yield return new WaitForSeconds(time);
        if (objectToAttack)
        {
            rb.AddForce((objectToAttack.transform.position - transform.position) * AttackForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
        }

        Debug.Log("Narwal attacking");
    }
}
