using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBobBehavior : MonoBehaviour
{
    public float sphereRadius = 3f;
    Vector3 startingBobPosition;

    Rigidbody bobRB;
    void Start()
    {
        bobRB = transform.GetComponent<Rigidbody>();
        startingBobPosition = transform.position;
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
        //else if (collision.gameObject.tag == "BobBorder") {
        //    bobRB.velocity = Vector3.zero;
        //    bobRB.useGravity = false;
        //    FishingPoleBehavior2.reeledIn = true;
        //    FishingPoleBehavior2.stayLocked = false;
        //}
    }
}
