using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (collision.gameObject.tag == "Water" || collision.gameObject.tag == "Fish")
        {
            bobRB.isKinematic = true;
        }
        else
        {
            PoleBehavior.isReeledIn = true;
        }
    }

    //void reelBob()
    //{
    //    PoleBehavior.isReeledIn = true;
    //    transform.position = PoleBehavior.bobStartingPos;
    //}
}
