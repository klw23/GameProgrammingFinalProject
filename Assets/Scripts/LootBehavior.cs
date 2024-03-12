using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public int timeAmount = 5;
    public AudioClip lootSFX;
    public LootSpawner lootSpawner;

    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && gameObject.CompareTag("Loot"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(lootSFX, transform.position);

            var levelManager = FindObjectOfType<LevelManagerBehavior>();
            levelManager.IncreaseTime(timeAmount);

            lootSpawner.LootCollected();
            Destroy(gameObject, 0.5f);
        }
    }
}
