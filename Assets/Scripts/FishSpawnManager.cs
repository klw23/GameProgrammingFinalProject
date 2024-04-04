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

    GameObject[] spawnPoints;

    void Start()
    {
        fishParent = GameObject.FindGameObjectWithTag("FishParent");
        spawnPoints  = GameObject.FindGameObjectsWithTag("FishSpawnPoint");
        InvokeRepeating("SpawnFishNearPlayer", 2f, 5f);
        FishAI.numberOfFishSpawned = 0;
    }



    void SpawnFishNearPlayer()
    {
        print("numOfFishSpawned" + FishAI.numberOfFishSpawned + " max" + maxFishInGame + "Total" + totalFishSpawned);

        if (FishAI.numberOfFishSpawned < maxFishInGame && totalFishSpawned < maxTotalFish)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length - 1);
            Vector3 spawnPosition = spawnPoints[randomSpawnPoint].transform.position;

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