using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject FinishChunk;
    public GameObject LevelChunk;
    public int MaxChunkCount;

    private float chunkSize;
    private GameObject latestChunk;
    private int chunkCounter;
    
    void Start()
    {
        chunkCounter = 0;
        chunkSize = LevelChunk.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    public void GenerateChunk(GameObject currentChunk)
    {
        if (chunkCounter < MaxChunkCount)
        {
            Vector3 chunkPosition = currentChunk.transform.position + new Vector3(chunkSize, 0, 0);
            Instantiate(LevelChunk, chunkPosition , currentChunk.transform.rotation);
            chunkCounter++;
        }
        else
        {
            Vector3 chunkPosition = currentChunk.transform.position + new Vector3(chunkSize, 0, 0);
            Instantiate(FinishChunk, chunkPosition , currentChunk.transform.rotation);
        } 
    }
}
