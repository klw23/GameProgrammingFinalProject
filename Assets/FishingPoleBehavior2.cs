using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FishingPoleBehavior2 : MonoBehaviour
{
    public static bool reeledIn;
    public static bool stayLocked;
    public static float reelInSpeed = 3f;

    public GameObject fishingBob;
    public GameObject player;
    public float projectileSpeed = 10f;

    Vector3 startingBobPosition;

    Rigidbody bobRB;
    LineRenderer lr;

    void Start()
    {
        reeledIn = true;
        stayLocked = false;
        bobRB = fishingBob.GetComponent<Rigidbody>();
        lr = transform.GetComponent<LineRenderer>();
    }


    void Update()
    {
        // Connect the pole and the bob with a line 
        lr.SetPosition(0, transform.GetChild(0).gameObject.transform.position); //RodEndConnection
        lr.SetPosition(1, fishingBob.transform.position); // bob Connection

        // If player fires rod ("fire1") and the bob has not been casted yet,
        // shoot bob towards water 
        if (Input.GetButtonDown("Fire1") && reeledIn && !stayLocked)
        {
            castBob();
        }
        else if (Input.GetButtonDown("Fire1") && !reeledIn && !stayLocked)
        {
            stayLocked = true;
        }
        else if (!reeledIn && stayLocked) // if the bob is not reeled in but it stays locked
        {
            reelInBob(fishingBob, startingBobPosition, reelInSpeed);
        } 
        else if (reeledIn && !stayLocked)
        {
            fishingBob.transform.position = transform.GetChild(0).transform.position + Vector3.down + player.transform.forward * 0.5f;
        }
    }
    void castBob()
    {
        Debug.Log("Down" + transform.forward * projectileSpeed);
        startingBobPosition = fishingBob.transform.position;
        Vector3 playerForward = player.transform.forward;
        bobRB.useGravity = true;
        bobRB.AddForce( playerForward * projectileSpeed, ForceMode.VelocityChange);
        stayLocked = true;
        //Vector3.MoveTowards(fishingBob.transform.position, )
    }
    public static void reelInBob(GameObject fishingBob, Vector3 startingBobPos, float reelInSpeed)
    {
        Vector3 fishingBobPos = fishingBob.transform.position;
        Debug.Log("Up" + fishingBob.transform.position + startingBobPos);
        //bobRB.AddForce(fishingBob.transform.forward * reelInSpeed, ForceMode.VelocityChange);
        fishingBob.transform.position = Vector3.MoveTowards(fishingBob.transform.position, startingBobPos, reelInSpeed * Time.deltaTime);
        //fishingBobPos = startingBobPos;
        if(fishingBob.transform.position == startingBobPos)
        {
            reeledIn = true;
            stayLocked = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bob")
    //    {
    //        bobRB.velocity = Vector3.zero;
    //        FishingPoleBehavior2.stayLocked = false;
    //    }
    //}
}
