using UnityEngine;

public class ShitHit : MonoBehaviour
{
    public Sprite splattedShit;

    private SpriteRenderer renderer;
    private Rigidbody2D rb;
    
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            renderer.sprite = splattedShit;
            rb.isKinematic = true;
        }
        else
        {
            KillEnemy(other.gameObject);
        }
    }

    private void KillEnemy(GameObject enemy)
    {
        Destroy(enemy);
        Destroy(this.gameObject);
    }
}
