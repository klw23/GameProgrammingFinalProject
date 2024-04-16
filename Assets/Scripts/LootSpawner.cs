using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootSpawner : MonoBehaviour
{
    public GameObject hourglassPrefab; 
    public GameObject starPrefab; 

    public GameObject islandPrefab;

    public float hourglassSpawnTime = 7f; 
    public float starSpawnTime = 30f; 

    private int maxHourglass = 10; 
    private int maxStar = 2; 

    private int currentHourglassCount = 0; 
    private int currentStarCount = 0; 

    public AudioClip lootSFX;

    void Start()
    {
        // call function
        if (SceneManager.GetActiveScene().name == "3 - LevelThreeScene1")
        {
            InvokeRepeating("SpawnStar", starSpawnTime, starSpawnTime);
        }

        InvokeRepeating("SpawnHourglass", hourglassSpawnTime, hourglassSpawnTime);
    }

    
    void SpawnLoot(GameObject prefab, ref int currentCount, int maxCount, string lootTag)
    {
        if (currentCount < maxCount && islandPrefab != null)
        {
            Collider islandCollider = islandPrefab.GetComponent<Collider>();

            if (islandCollider != null)
            {
                // Use the island
                float spawnX = Random.Range(islandCollider.bounds.min.x + 8f, islandCollider.bounds.max.x - 8f);
                float spawnZ = Random.Range(islandCollider.bounds.min.z + 8f, islandCollider.bounds.max.z - 8f);

                //float spawnY = islandCollider.bounds.max.y + 0.5f;

                float spawnY = 5f;

                Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

                // Instantiate loot
                GameObject spawnedLoot = Instantiate(prefab, spawnPosition, Quaternion.identity);
                spawnedLoot.transform.SetParent(transform);

                currentCount++; 

                // Play the loot spawn sound effect
                AudioSource.PlayClipAtPoint(lootSFX, spawnPosition);

                Debug.Log($"{lootTag} spawned at: {spawnPosition}");

            }
        }

    }

    void SpawnHourglass()
    {
        SpawnLoot(hourglassPrefab, ref currentHourglassCount, maxHourglass, "Hourglass");
    }

    void SpawnStar()
    {
        SpawnLoot(starPrefab, ref currentStarCount, maxStar, "Star");
    }



    public void LootCollected(string tag)
    {
        if (tag == "hourglass" && currentHourglassCount > 0)
        {
            currentHourglassCount--;
        }
        else if (tag == "star" && currentStarCount > 0)
        {
            currentStarCount--;
        }
    }
   
}
