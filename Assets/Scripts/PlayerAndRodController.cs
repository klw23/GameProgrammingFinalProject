using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRodController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 500;
    CharacterController controller;
    Vector3 input, moveDirection;
    Rigidbody player;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (!PoleBehavior.isReeledIn)
        {
            // do not let player move
           
            player.velocity = Vector3.zero;
            player.angularVelocity = Vector3.zero;
        }
        else
        {
            moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            moveDirection.Normalize();

            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //allows us to move the character based on keyboard input

        }
    }
}
