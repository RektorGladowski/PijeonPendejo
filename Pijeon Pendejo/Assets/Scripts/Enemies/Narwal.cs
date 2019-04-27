using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narwal : Enemy
{
    public float AttackForce = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainPigeon"))
        {
            StartCoroutine(Attack(collision.gameObject, 1));
        }
    }

    private IEnumerator Attack(GameObject objectToAttack, float time)
    {
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(time);
        rb.AddForce((objectToAttack.transform.position - transform.position) * AttackForce, ForceMode2D.Impulse);
        rb.gravityScale = 1;

        Debug.Log("Narwal attacking");
    }
}
