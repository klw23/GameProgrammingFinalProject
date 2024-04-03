using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("triggered");
            var lootSpawner = FindObjectOfType<BoosterLootSpawner>();
            var GrayFishSpawner = FindObjectOfType<GrayFishSpawner>();
            var levelManager = FindObjectOfType<LevelManagerBehavior>();
            levelManager.ShowBoostedMode();
            GrayFishSpawner.startBoostedMode();

            Destroy(gameObject);
            lootSpawner.CollectedLoot();
        }
    }
}
