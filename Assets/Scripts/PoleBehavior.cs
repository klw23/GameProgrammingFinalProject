using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBehavior : MonoBehaviour
{
    public static bool isReeledIn;
    public static Vector3 bobStartingPos;
   
    public GameObject bob;
    public GameObject player;
    public float projectileSpeed = 10f;

    LineRenderer lr;
    Rigidbody bobRB;
    

    void Start()
    {
        isReeledIn = true;
        lr = transform.GetComponent<LineRenderer>();
        bobRB = bob.GetComponent<Rigidbody>();
        UpdateBobStartingPos();

    }

    void Update()
    {
        // Connect the pole and the bob with a line 
        lr.SetPosition(0, transform.GetChild(0).gameObject.transform.position); //RodEndConnection
        lr.SetPosition(1, bob.transform.position); // bob Connection

        if (Input.GetButtonDown("Fire1") && isReeledIn && !PlayerAndRodController.isWalking)
        {
            castBob();
        } 
        else if (Input.GetButtonDown("Fire1") && !isReeledIn)
        {
            reelBob();
        } 
        else if (isReeledIn)
        {
            UpdateBobStartingPos();
        }
    }
    void UpdateBobStartingPos()
    {
        bobStartingPos = transform.GetChild(0).transform.position + Vector3.down * 0.3f;
        bob.transform.position = bobStartingPos;
    }

    void castBob()
    {
        bobStartingPos = bob.transform.position;
        bobRB.isKinematic = false;
        bobRB.useGravity = true;
        bobRB.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
        isReeledIn = false;
    }

    void reelBob()
    {
        isReeledIn = true;
    }
}
