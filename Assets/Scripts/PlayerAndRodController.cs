using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRodController : MonoBehaviour
{

    // public variables
    public static bool isWalking = false;
    public float moveSpeed = 5;
    public float rotationSpeed = 700;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public Animator anim;

    public bool invertControls = false;

    // private variables
    CharacterController controller;
    Vector3 input, moveDirection;
    float moveHorizontal, moveVertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (PoleBehavior.isReeledIn)
        {
            if (controller.isGrounded)
            {
                if (moveHorizontal != 0 || moveVertical != 0)
                {
                    anim.SetInteger("MichelleMovement", 1);
                    onGroundMove();
                    isWalking = true;
                } 
                else if (Input.GetButton("Jump"))
                {
                    anim.SetInteger("MichelleMovement", 5);
                    moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                    isWalking = false;
                }
                else
                {
                    anim.SetInteger("MichelleMovement", 0);
                    moveDirection = Vector3.zero;
                    isWalking = false;
                }
            } 
            else
            {
                // midair 
                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime);
                isWalking = false;
            }
        } 
        else
        {
            anim.SetInteger("MichelleMovement", 0);
            moveDirection = Vector3.zero;
            isWalking = false;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void onGroundMove()
    {

        if (moveHorizontal > 0 )
        {
            anim.SetInteger("MichelleMovement", 3);
        } 
        else if (moveHorizontal < 0 )
        {
            anim.SetInteger("MichelleMovement", 4);
        }
        else if (moveVertical > 0)
        {
            anim.SetInteger("MichelleMovement", 1);
        }
        else if (moveVertical < 0)
        {
            anim.SetInteger("MichelleMovement", 2);
        }

        if(invertControls)
        {
            moveDirection = new Vector3(-moveHorizontal, 0, -moveVertical);
        }
        else
        {
            moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        }
      
        moveDirection.Normalize();

        controller.Move(moveDirection * moveSpeed * Time.deltaTime); //allows us to move the character based on keyboard input

    }
}