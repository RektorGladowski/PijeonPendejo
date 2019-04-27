using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static GameObject MainPigeon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MainPigeon"))
        {
            Activate();
        }
    }

    public abstract void Activate();
}
