using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBobBehavior : MonoBehaviour
{
    public Transform fishingPole;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = fishingPole.GetChild(0).gameObject.transform.position - new Vector3(0, 0.3f, 0);
    }
}
