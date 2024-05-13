using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{

    public GameObject fishPrefab; // Prefab for the fish
    public float minSpawnDelay = 5f; // Minimum delay between fish spawns
    public float maxSpawnDelay = 10f; // Maximum delay between fish spawns

    private void Start()
    {
        // Start spawning fish at random intervals
        Invoke("SpawnFish", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void Update()
    {

    }

    public void SpawnFish()
    {
        // Spawn a fish at a random position within the pond collider
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        // Schedule the next fish spawn
        Invoke("SpawnFish", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Get a random point within the bounds of the pond collider
        Collider collider = GetComponent<Collider>();
        Vector3 center = collider.bounds.center;
        Vector3 extents = collider.bounds.extents;
        float x = Random.Range(center.x - extents.x, center.x + extents.x);
        float z = Random.Range(center.z - extents.z, center.z + extents.z);
        return new Vector3(x, center.y, z);
    }
}