using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRodController : MonoBehaviour
{

    // public variables
    public static bool isWalking = false;
    public static bool isChatting = false;
    public float moveSpeed = 5;
    public float rotationSpeed = 700;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public float airControl = 10;
    public Animator anim;
    public Transform cutSceneCharacter;

    public bool isCutScene = false;
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

        input = (Vector3.right * moveHorizontal + Vector3.forward * moveVertical).normalized;

        input *= moveSpeed;

        if (PoleBehavior.isReeledIn)
        {
            if (controller.isGrounded)
            {
                if (moveHorizontal != 0 || moveVertical != 0)
                {
                    onGroundAnimate();
                    onMove();
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
                    if (isCutScene)
                    {
                        anim.SetInteger("MichelleMovement", 6);
                    }
                    else
                    {
                        anim.SetInteger("MichelleMovement", 0);
                    }
                    moveDirection = Vector3.zero;
                    isWalking = false;
                }
            }
            else
            {
                // midair 
                if (invertControls)
                {
                    input.x = -input.x;
                    input.z = -input.z;
                }

                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);

            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime); //allows us to move the character based on keyboard input
    }

    void onMove()
    {
        if (invertControls)
        {
            moveDirection = -input;
        }
        else
        {
            moveDirection = input;
        }

        moveDirection.Normalize();

        moveDirection = moveDirection * moveSpeed;

    }

    void onGroundAnimate()
    {
        if (moveHorizontal > 0)
        {
            anim.SetInteger("MichelleMovement", 3);
        }
        else if (moveHorizontal < 0)
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
    }
}