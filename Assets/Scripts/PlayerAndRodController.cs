using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        isChatting = false;
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        input = (Vector3.right * moveHorizontal + Vector3.forward * moveVertical).normalized;

        input *= moveSpeed;

        if (PoleBehavior.isReeledIn && !isChatting)
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

        if (isChatting && cutSceneCharacter != null)
        {
            FaceTarget(cutSceneCharacter.position);
        }
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
        // Determines what is the relationship the moveDirection is to the players current forward direction (mouseLook)
        float  facingMovementDirection = Vector3.Dot(transform.forward.normalized, moveDirection.normalized);

        if (Mathf.Round(facingMovementDirection) == 1)
        {
            anim.SetInteger("MichelleMovement", 1); // forward
        } 
        else if (Mathf.Round(facingMovementDirection) == -1)
        {
            anim.SetInteger("MichelleMovement", 2); // backwards
        }
        else if (Mathf.Round(facingMovementDirection) == 0)
        {
            if ( moveHorizontal > 0 || moveVertical > 0)
            {
                anim.SetInteger("MichelleMovement", 3);
            } 
            else if (moveHorizontal < 0 || moveVertical < 0)
            {
                anim.SetInteger("MichelleMovement", 4);
            }
        }
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}