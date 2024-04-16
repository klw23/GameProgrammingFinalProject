using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootBehavior : MonoBehaviour
{
    public int timeAmount;
    public AudioClip collectLootSFX;

    public LootSpawner lootSpawner;

    private int maxSharksSpawned = 2;

    private int sharkSpawned = 0;
    public GameObject sharkPrefab;
    private GameObject sharkParent;
    GameObject[] spawnPoints;
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && gameObject.CompareTag("Hourglass"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectLootSFX, transform.position);

            var levelManager = FindObjectOfType<LevelManagerBehavior>();

            // loot gives 10 seconds if level is 3
            if (SceneManager.GetActiveScene().name == "3 - LevelThreeScene1")
            {
                timeAmount = 10;
            }
            else 
            {
                timeAmount = 5;
            }

            levelManager.IncreaseTime(timeAmount);
            levelManager.ShowAddedTime(timeAmount);

            lootSpawner.LootCollected("Hourglass");
            Destroy(gameObject, 0.5f);
        }

        else if (other.CompareTag("Player") && gameObject.CompareTag("Star"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectLootSFX, transform.position);
    
            if (sharkSpawned < maxSharksSpawned)
            {
                int randomSpawnPoint = Random.Range(0, spawnPoints.Length - 1);
                Vector3 spawnPosition = spawnPoints[randomSpawnPoint].transform.position;

                // Instantiate fish and set its parent
                GameObject newShark = Instantiate(sharkPrefab, spawnPosition, Quaternion.identity);
                if (sharkParent != null)
                {
                    newShark.transform.SetParent(sharkParent.transform);
                }

                sharkSpawned++;
            }

        }
    }
}