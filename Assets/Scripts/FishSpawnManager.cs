using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int maxFishInGame = 4;
    public int maxTotalFish = 10;
    private int totalFishSpawned = 0;
    private GameObject fishParent;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 30f;
    public GameObject island;


    void Start()
    {
        fishParent = GameObject.FindGameObjectWithTag("FishParent");
        InvokeRepeating("SpawnFishNearPlayer", 2f, 5f); 
    }



    void SpawnFishNearPlayer()
    {

        if (FishAI.numberOfFishSpawned < maxFishInGame && totalFishSpawned < maxTotalFish)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            // Random distance within the specified range
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            // Calculate spawn position
            Vector3 spawnPosition = island.transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * randomDistance;

            // Instantiate fish and set its parent
            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            if (fishParent != null)
            {
                newFish.transform.SetParent(fishParent.transform);
            }

            totalFishSpawned++;
        }



    }
}