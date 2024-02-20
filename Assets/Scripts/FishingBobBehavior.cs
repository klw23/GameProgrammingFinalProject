using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBobBehavior : MonoBehaviour
{ 

    Rigidbody bobRB;
    void Start()
    {
        bobRB = transform.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log("Hit Water");
            bobRB.velocity = Vector3.zero;
            bobRB.useGravity = false;
            FishingPoleBehavior2.reeledIn = false;
            FishingPoleBehavior2.stayLocked = false;
        } 
    }
}
