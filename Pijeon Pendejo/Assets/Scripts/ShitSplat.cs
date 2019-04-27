using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitSplat : MonoBehaviour
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
        if (!other.gameObject.CompareTag("MainPigeon"))
        {
            renderer.sprite = splattedShit;
            rb.isKinematic = true;
        }
    }
}
