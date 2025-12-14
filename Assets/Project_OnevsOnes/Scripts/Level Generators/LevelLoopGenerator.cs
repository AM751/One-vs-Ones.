using UnityEngine;

public class LevelLoopGenerator : MonoBehaviour
{
    [Header("Level Segments")]
   
    public GameObject[] segmentPrefabs;

    [Header("Settings")]
    
    public float segmentLength = 30f;
  
    public int initialSegments = 3;
    
    public Transform playerTransform;

    // Internal variables
    private float spawnXPosition = 0f; 
    private int currentSegmentIndex = 0; 
   
    private float safeZone = 60f; 

    void Start()
    {
        
        spawnXPosition = 0f; 

        
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnNextSegment();
        }
    }

    void Update()
    {
        if (playerTransform.position.x > spawnXPosition - safeZone)
        {
            SpawnNextSegment();
        }
    }

    void SpawnNextSegment()
    {
        // 1. Pick the prefab based on the current index
        GameObject chunkToSpawn = segmentPrefabs[currentSegmentIndex];

        // 2. Instantiate it at the current spawn position
        Instantiate(chunkToSpawn, new Vector3(spawnXPosition, 0, 0), Quaternion.identity);

        // 3. Move the spawn position forward for the next time
        spawnXPosition += segmentLength;

        
        currentSegmentIndex = (currentSegmentIndex + 1) % segmentPrefabs.Length; //Random.Range(0, segmentPrefabs.Length);
    }
}
