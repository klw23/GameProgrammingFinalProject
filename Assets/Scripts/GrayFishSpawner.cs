using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayFishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    private int maxGrayFishSpawned = 5;
    private GameObject fishParent;

    private int grayFishSpawned = 1;

    GameObject[] spawnPoints;

    void Start()
    {
        fishParent = GameObject.FindGameObjectWithTag("FishParent");
        spawnPoints = GameObject.FindGameObjectsWithTag("FishSpawnPoint");
        InvokeRepeating("SpawnGrayFish", 5f, 10f);
    }



    void SpawnGrayFish()
    {


        if (grayFishSpawned < maxGrayFishSpawned)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length - 1);
            Vector3 spawnPosition = spawnPoints[randomSpawnPoint].transform.position;

            // Instantiate fish and set its parent
            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            if (fishParent != null)
            {
                newFish.transform.SetParent(fishParent.transform);
            }

            grayFishSpawned++;
        }

    }

}