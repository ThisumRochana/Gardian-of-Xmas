using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    public GameObject goblinPrefab;       // Your single goblin
    public Transform leftSpawn;           // Left side spawn
    public Transform rightSpawn;          // Right side spawn
    public float spawnInterval = 3f;      // Time between spawns

    void Start()
    {
        InvokeRepeating("SpawnGoblin", 1f, spawnInterval);
    }

    void SpawnGoblin()
    {
        // Choose randomly left or right
        Transform spawnPoint = (Random.value < 0.5f) ? leftSpawn : rightSpawn;

        Instantiate(goblinPrefab, spawnPoint.position, Quaternion.identity);
    }
}
