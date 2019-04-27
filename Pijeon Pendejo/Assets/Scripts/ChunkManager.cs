using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject StartChunk;
    public GameObject FinishChunk;
    public GameObject LevelChunk;
    public int ChunkCount;

    private float chunkSize;
    private GameObject latestChunk;
    private int chunkCounter = 0;
    
    void Start()
    {
        latestChunk = StartChunk;
        chunkSize = LevelChunk.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        GenerateChunk(latestChunk);
    }

    public void GenerateChunk(GameObject currentChunk)
    {
            Debug.Log(chunkCounter);
            chunkSize = LevelChunk.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
            Vector3 chunkPosition = currentChunk.transform.position + new Vector3(chunkSize, 0, 0);
            Instantiate(LevelChunk, chunkPosition , currentChunk.transform.rotation);
            latestChunk = currentChunk;
            chunkCounter++; 
    }
}
