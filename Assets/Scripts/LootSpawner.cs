using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootPrefab; 

    public GameObject islandPrefab;

    public float spawnTime = 7f; // time between spawns
    private int maxLoot = 3; // max number of loot
    private int currentLootCount = 0; // current number of loot items

    public AudioClip lootSFX;

    void Start()
    {
        // call function
        InvokeRepeating("SpawnLoot", spawnTime, spawnTime);
    }

    void SpawnLoot()
    {
        if (currentLootCount < maxLoot)
        {
            if (islandPrefab != null)
            {
                Debug.Log("Spawning Loot");
                GameObject actualBoatObject = islandPrefab.transform.GetChild(0).gameObject;
                Collider boatCollider = actualBoatObject.GetComponent<Collider>();
                WaterFloat boatWaterFloat = islandPrefab.GetComponent<WaterFloat>();

                if (boatCollider != null)
                {
                    float yPadding = 1.0f;
                    // Use the collider's bounds to determine the spawn area
                    float spawnX = Random.Range(boatCollider.bounds.min.x + 3.0f, boatCollider.bounds.max.x - 3.0f);
                    float boatCenterY = boatCollider.bounds.center.y;
                    float spawnYOffset = Random.Range(-boatWaterFloat.MovingDistances.y, boatWaterFloat.MovingDistances.y);
                    float spawnY = boatCenterY + spawnYOffset + yPadding;
                    float spawnZ = Random.Range(boatCollider.bounds.min.z + 2f, boatCollider.bounds.max.z - 0.5f);

                    Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

                    Debug.Log($"Loot spawned at: {spawnPosition}");

                    GameObject spawnedLoot = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
                    spawnedLoot.transform.SetParent(transform);

                    currentLootCount++;

                    AudioSource.PlayClipAtPoint(lootSFX, spawnPosition);
                }
            }
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
