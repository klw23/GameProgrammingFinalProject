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

            moveDirection = new Vector3(-moveHorizontal, 0, -moveVertical);
            moveDirection.Normalize();

            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //allows us to move the character based on keyboard input
            anim.SetInteger("MichelleMovement", 1);

            isWalking = true;
        }
        else
        {
            anim.SetInteger("MichelleMovement", 0);
            isWalking = false;
        }
    }

}