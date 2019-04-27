using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
   public ChunkManager manager;
   private bool isAlreadyEntered;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (!isAlreadyEntered && other.CompareTag("MainPigeon"))
      {
         manager.GenerateChunk(gameObject);
         isAlreadyEntered = true;
      }
   }
}
