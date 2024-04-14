using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBehavior : MonoBehaviour
{
    public static bool isReeledIn;
    public static Vector3 bobStartingPos;
    public static float projectileSpeed = 10f;

    public GameObject fishingbob;
    public GameObject player;
    public float timerBetweenFires = 2f;
    public GameObject tipOfPole;

    LineRenderer lr;
    Rigidbody bobRB;
    bool canFire;
    float timer;

    void Start()
    {
        fishingbob = GameObject.FindGameObjectWithTag("Bob");
        tipOfPole = transform.GetChild(0).gameObject;
        lr = transform.GetComponent<LineRenderer>();
        bobRB = fishingbob.GetComponent<Rigidbody>();

        UpdateBobStartingPos();
        isReeledIn = true;
        canFire = true;
        timer = timerBetweenFires;
    }

    void Update()
    {
        if (!canFire)
        {
            timer-= Time.deltaTime;
            if(timer <= 0)
            {
                canFire = true;
            }
        }

        // Connect the pole and the bob with a line 
        lr.SetPosition(0, transform.GetChild(0).gameObject.transform.position); //RodEndConnection
        lr.SetPosition(1, fishingbob.transform.position); // bob Connection

        if (Input.GetButtonDown("Fire1") && isReeledIn && !PlayerAndRodController.isWalking && canFire)
        {
            castBob();
            timer = timerBetweenFires;
        } 
        else if (Input.GetButtonDown("Fire1") && !isReeledIn)
        {
            reelBob();
            canFire = false;
        } 
        else if (isReeledIn)
        {
            UpdateBobStartingPos();
        }
    }
    void UpdateBobStartingPos()
    {
        bobStartingPos = transform.GetChild(0).transform.position + Vector3.down * 0.3f;
        fishingbob.transform.position = bobStartingPos;
    }

    void castBob()
    {
        bobStartingPos = fishingbob.transform.position;
        bobRB.isKinematic = false;
        bobRB.useGravity = true;
        bobRB.AddForce(tipOfPole.transform.forward * projectileSpeed, ForceMode.VelocityChange);
        isReeledIn = false;
    }

    void reelBob()
    {
        isReeledIn = true;
    }
}
