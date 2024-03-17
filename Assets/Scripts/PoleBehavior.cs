using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBehavior : MonoBehaviour
{
    public static bool isReeledIn;
   
    public GameObject bob;
    public GameObject player;
    public float projectileSpeed = 10f;

    LineRenderer lr;
    Rigidbody bobRB;
    Vector3 bobStartingPos;

    void Start()
    {
        isReeledIn = true;
        lr = transform.GetComponent<LineRenderer>();
        bobRB = bob.GetComponent<Rigidbody>();
        UpdateBobStartingPos();

    }
    void UpdateBobStartingPos()
    {
        bobStartingPos = transform.GetChild(0).transform.position + Vector3.down + player.transform.forward * 0.5f;
    }

    void Update()
    {
        // Connect the pole and the bob with a line 
        lr.SetPosition(0, transform.GetChild(0).gameObject.transform.position); //RodEndConnection
        lr.SetPosition(1, bob.transform.position); // bob Connection

        if (Input.GetButtonDown("Fire1") && isReeledIn)
        {
            castBob();
        } 
        else if (Input.GetButtonDown("Fire1") && !isReeledIn)
        {
            reelBob();
        }
    }

    void castBob()
    {
        bobStartingPos = bob.transform.position;
        bobRB.useGravity = true;
        bobRB.AddForce(player.transform.forward * projectileSpeed, ForceMode.VelocityChange);
        isReeledIn = false;
    }

    void reelBob()
    {
        isReeledIn = true;
        //bobRB.velocity = Vector3.zero; // Reset velocity
        //bobRB.angularVelocity = Vector3.zero; // Reset angular velocity
        //bobRB.useGravity = false; // Optionally turn off gravity
        bob.transform.position = bobStartingPos;
    }
}
