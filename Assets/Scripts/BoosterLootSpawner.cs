using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterLootSpawner : MonoBehaviour
{
    public GameObject lootPrefab;

    public GameObject obstaclePrefab;

    public int maxSpawnTimes = 2; // max number of loot
    private int currentSpawnTime = 0; // current amount of times it spawned

    private bool isLootInGame = false;

    public AudioClip lootSFX;

    void Start()
    {
        Invoke("SpawnLoot", 30f);
    }

    void SpawnLoot()
    {
        if (currentSpawnTime < maxSpawnTimes && obstaclePrefab != null && !isLootInGame)
        {
            Collider iceCollider = obstaclePrefab.GetComponent<Collider>();
            if (iceCollider != null)
            {
                // Use the island's collider bounds
                float spawnX = Random.Range(iceCollider.bounds.min.x + 3f, iceCollider.bounds.max.x - 3f);
                float spawnZ = Random.Range(iceCollider.bounds.min.z + 3f, iceCollider.bounds.max.z - 3f);

                float spawnY = iceCollider.bounds.max.y + 1f;


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
            currentSpawnTime++;
            Invoke("SpawnLoot", 30f);

          
            }
       }
    
}
