using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public float spawnHeight = 15f; // Height above the car to spawn
    public float forwardOffset = 5f;   // Offset in front of the car
    public GameObject playerCar; // Assign your player car in the Inspector
    public GameObject[] fallingObjects; // Array of prefabs to fall (assign in Inspector)

    public float spawnRadius = 10f; // Radius around the player where objects can spawn
    public float spawnInterval = 2f; // Time between object spawns

    private float nextSpawnTime; 

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObject()
    {
        // Generate a random position around the player
        Vector3 spawnOffset = Random.insideUnitSphere * spawnRadius;
        spawnOffset.y = 0; // Flatten to the ground level
        Vector3 spawnPosition = playerCar.transform.position + spawnOffset;

        // Adjust spawn position for height and offset
        spawnPosition.y += spawnHeight; 
        spawnPosition += playerCar.transform.forward * forwardOffset;
        
        // Choose a random object and Instantiate 
        int randomIndex = Random.Range(0, fallingObjects.Length);
        GameObject fallingObject = fallingObjects[randomIndex];
        Instantiate(fallingObject, spawnPosition, Quaternion.identity); 
    }
}
