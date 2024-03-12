using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootPrefab; 
    public float spawnTime = 7f; // time between spawns
    private int maxLoot = 3; // max number of loot
    private int currentLootCount = 0; // current number of loot items

    // Spawn boundaries
    public float xMin = -17f;
    public float xMax = -5f;
    public float yMin = 1f;
    public float yMax = 1.5f;
    public float zMin = 82f;
    public float zMax = 84f;

    void Start()
    {
        // call function
        InvokeRepeating("SpawnLoot", spawnTime, spawnTime);
    }

    void SpawnLoot()
    {
        if (currentLootCount < maxLoot)
        {

            Vector3 spawnPosition = new Vector3(
                Random.Range(xMin, xMax),
                Random.Range(yMin, yMax),
                Random.Range(zMin, zMax)
            );

            
            GameObject spawnedLoot = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
            
            spawnedLoot.transform.SetParent(transform);

            currentLootCount++;
        }
    }

    public void LootCollected()
    {
        if (currentLootCount > 0)
        {
            currentLootCount--;
        }
    }
}
