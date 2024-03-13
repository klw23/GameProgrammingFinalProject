using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int maxFishInGame = 5;
    public int maxTotalFish = 25;
    private int totalFishSpawned = 0;
    private GameObject fishParent;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 30f;
    public GameObject boat;


    void Start()
    {
        fishParent = GameObject.FindGameObjectWithTag("FishParent");
        InvokeRepeating("SpawnFishNearPlayer", 3f, 3f); // 3f delay before first spawn, then every 3 seconds
    }

  

    void SpawnFishNearPlayer()
    {
       
         if (FishAI.numberOfFishSpawned < maxFishInGame && totalFishSpawned < maxTotalFish)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            // Random distance within the specified range
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            // Calculate spawn position
            Vector3 spawnPosition = boat.transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * randomDistance;

            // Instantiate fish and set its parent
            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            if (fishParent != null)
            {
                newFish.transform.SetParent(fishParent.transform);
            }

            totalFishSpawned++;
            FishAI.numberOfFishSpawned++;
        }
           
        

    }
}
