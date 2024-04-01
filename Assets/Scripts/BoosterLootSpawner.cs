using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterLootSpawner : MonoBehaviour
{
    public GameObject lootPrefab;

    public GameObject obstaclePrefab;

    private int maxLoot = 3; // max number of loot
    private int currentLootCount = 0; // current number of loot items

    private bool isLootInGame = false;

    public AudioClip lootSFX;

    void Start()
    {
        SpawnLoot();
    }

    void SpawnLoot()
    {
        if (currentLootCount < maxLoot && obstaclePrefab != null && !isLootInGame)
        {
            currentLootCount++;
            Collider islandCollider = obstaclePrefab.GetComponent<Collider>();
            if (islandCollider != null)
            {
                // Use the island's collider bounds
                float spawnX = Random.Range(islandCollider.bounds.min.x + 2f, islandCollider.bounds.max.x - 2f);
                float spawnZ = Random.Range(islandCollider.bounds.min.z + 2f, islandCollider.bounds.max.z - 2f);

                float spawnY = islandCollider.bounds.max.y + 1.5f;


                Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

                // Instantiate the loot prefab at the calculated position
                GameObject spawnedLoot = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
                spawnedLoot.transform.SetParent(transform);

                isLootInGame = true; 

                // Play the loot spawn sound effect
                AudioSource.PlayClipAtPoint(lootSFX, spawnPosition);

                Debug.Log($"Loot spawned at: {spawnPosition}");

            }
        }

    }



    public void CollectedLoot()
    {
      
       if (isLootInGame) {
            isLootInGame = false;
            Invoke("SpawnLoot", 5f);

          
            }
       }
    
}
