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
        if (!PoleBehavior.isReeledIn)
        {
            // do not let player move
            isWalking = false;
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            anim.SetInteger("FishingAnim", 1);
            moveDirection = new Vector3(-moveHorizontal, 0, -moveVertical);
            moveDirection.Normalize();

            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //allows us to move the character based on keyboard input

            isWalking = true;
        }
        else
        {
            anim.SetInteger("FishingAnim", 0);
            isWalking = false;
        }
    }

}
