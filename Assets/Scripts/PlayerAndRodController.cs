using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRodController : MonoBehaviour
{

    // public variables
    public static bool isWalking = false;
    public float moveSpeed = 5;
    public float rotationSpeed = 700;
    public Animator anim;

    public float minX = 147f;
    public float maxX = 419f;
    public float minZ = -534f;
    public float maxZ = 810f;
    public GameObject islandCenter;


    // private variables
    CharacterController controller;
    Vector3 moveDirection;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        Debug.Log("Horizontal" + Input.GetAxisRaw("Horizontal") + "Vertical" + Input.GetAxisRaw("Vertical"));
        print("touching barrier " + PlayerBarrier.touchingCollider);

        if (!PoleBehavior.isReeledIn)
        {
            // Do not let player move
            isWalking = false;
            return; // Early return to prevent further execution in this case
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 intendedDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = currentPosition + intendedDirection * moveSpeed * Time.deltaTime;
        Vector3 centerPosition = islandCenter.transform.position;

        // Calculate distances
        float currentDistance = Vector3.Distance(currentPosition, centerPosition);
        float nextDistance = Vector3.Distance(nextPosition, centerPosition);

        // Determine if moving closer to the center
        bool movingTowardsCenter = nextDistance < currentDistance;


        print("center " + movingTowardsCenter);
        if (movingTowardsCenter || !PlayerBarrier.touchingCollider)
        {
            anim.SetInteger("FishingAnim", 1);
            moveDirection = intendedDirection;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime); // Allows us to move the character based on keyboard input
            isWalking = true;
        }
        else
        {
            anim.SetInteger("FishingAnim", 0);
            isWalking = false;
        }
    }


}
