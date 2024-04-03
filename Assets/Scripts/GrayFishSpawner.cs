using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayFishSpawner : MonoBehaviour
{

    private float countdown = 10f;
    private bool isBoostedModeOn = false;

    public GameObject fishPrefab;
    private int maxGrayFishSpawned = 3;
    private GameObject fishParent;

    private int grayFishSpawned = 1;

    GameObject[] spawnPoints;

    void Start()
    {
        fishParent = GameObject.FindGameObjectWithTag("FishParent");
        spawnPoints = GameObject.FindGameObjectsWithTag("FishSpawnPoint");
        InvokeRepeating("SpawnGrayFish", 5f, 10f);
    }

    private void Update()
    {

        if (isBoostedModeOn)
        {
            countdown -= Time.deltaTime;
        }
    }


    public void startBoostedMode()
    {
        print("im in boosted mode!");
        isBoostedModeOn = true;
        InvokeRepeating("SpawnBoostedFish", 0f, 3f);
    }

    private void SpawnBoostedFish()
    {
        if (countdown > 0 && isBoostedModeOn)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length - 1);
            Vector3 spawnPosition = spawnPoints[randomSpawnPoint].transform.position;

            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            if (fishParent != null)
            {
                newFish.transform.SetParent(fishParent.transform);
            }
        }
        else
        {
            isBoostedModeOn = false;
            countdown = 10f;
            print("im no longer in boosted mode!");

        }

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