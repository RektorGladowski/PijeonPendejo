using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBoundaries : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BloodyExplosion"))
        {
            Destroy(collision.gameObject);
        }
    }
}
