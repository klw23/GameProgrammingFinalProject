using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRodController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 700;
    public Animator anim;

    CharacterController controller;
    Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Horizontal" + Input.GetAxisRaw("Horizontal") + "Vertical" + Input.GetAxisRaw("Vertical"));
        if (!PoleBehavior.isReeledIn)
        {
            // do not let player move
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            anim.SetInteger("FishingAnim", 1);
            moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            moveDirection.Normalize();

            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //allows us to move the character based on keyboard input

        } else
        {
            anim.SetInteger("FishingAnim", 0);
        }
    }
}
