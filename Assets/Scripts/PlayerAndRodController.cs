using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRodController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 700;
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (!PoleBehavior.isReeledIn)
        {
            // do not let player move
        }
        else
        {
            moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            moveDirection.Normalize();

            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //allows us to move the character based on keyboard input

        }
    }
}
