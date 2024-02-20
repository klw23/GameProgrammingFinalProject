using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    CharacterController controller;
    Vector3 input, moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized; //if not normalized, combining directions/going diagonal is faster 

        input *= moveSpeed;

        moveDirection = input;

        controller.Move(moveDirection * Time.deltaTime); //allows us to move the character based on keyboard input
    }
}
