using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject FinishChunk;
    public GameObject LevelChunk;
    public PolygonCollider2D CameraConfine;
    public int MaxChunkCount;

    private float chunkSize;
    private GameObject latestChunk;
    private int chunkCounter;
    
    void Start()
    {
        chunkSize = LevelChunk.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    public void GenerateChunk(GameObject currentChunk)
    {
        if (chunkCounter < MaxChunkCount)
        {
            Vector3 chunkPosition = currentChunk.transform.position + new Vector3(chunkSize, 0, 0);
            Instantiate(LevelChunk, chunkPosition , currentChunk.transform.rotation);
            chunkCounter++;
            UpdateCameraConfine(currentChunk, false);
        }
        else
        {
            Vector3 chunkPosition = currentChunk.transform.position + new Vector3(chunkSize, 0, 0);
            Instantiate(FinishChunk, chunkPosition , currentChunk.transform.rotation);
            UpdateCameraConfine(currentChunk, true);
        } 
    }

    private void UpdateCameraConfine(GameObject currentChunk, bool isFinishChunk)
    {
        float divider = isFinishChunk ? 0.75f : 2f;
        CameraConfine.transform.position = new Vector3(currentChunk.transform.position.x - (chunkSize / divider), CameraConfine.transform.position.y, CameraConfine.transform.position.z);
    }
}
