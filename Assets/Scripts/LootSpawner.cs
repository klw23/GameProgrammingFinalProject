using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootPrefab; 

    public GameObject islandPrefab;

    public float spawnTime = 7f; // time between spawns
    private int maxLoot = 10; // max number of loot
    private int currentLootCount = 0; // current number of loot items

    public AudioClip lootSFX;

    void Start()
    {
        // call function
        InvokeRepeating("SpawnLoot", spawnTime, spawnTime);
    }

    void SpawnLoot()
    {
        if (currentLootCount < maxLoot && islandPrefab != null)
        {
            print("here");
            Collider islandCollider = islandPrefab.GetComponent<Collider>();
            if (islandCollider != null)
            {
                // Use the island's collider bounds
                float spawnX = Random.Range(islandCollider.bounds.min.x + 8f, islandCollider.bounds.max.x - 8f);
                float spawnZ = Random.Range(islandCollider.bounds.min.z + 8f, islandCollider.bounds.max.z - 8f);

                //float spawnY = islandCollider.bounds.max.y + 0.5f;

                float spawnY = 5f;

                Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

                // Instantiate the loot prefab at the calculated position
                GameObject spawnedLoot = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
                spawnedLoot.transform.SetParent(transform);

                currentLootCount++; // Increment the loot count

                // Play the loot spawn sound effect
                AudioSource.PlayClipAtPoint(lootSFX, spawnPosition);

                Debug.Log($"Loot spawned at: {spawnPosition}");

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
