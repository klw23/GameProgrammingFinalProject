using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; 
    public Transform playerTransform;
    void Start()
    {
        
    }

    private void Update()
    {
        if (!PoleBehavior.isReeledIn)
        {
            // do not let player move
        } else
        {
            // Vector3 mousePos = Input.mousePosition;
            // Vector3 WorldPoint = Camera.main.WorldToScreenPoint(mousePos);
            // WorldPoint.y = transform.position.y;
            // transform.LookAt(-WorldPoint);

            Vector3 mousePos = Input.mousePosition;

            mousePos -= new Vector3(Screen.width / 2, Screen.height / 2, 0);
            mousePos *= mouseSensitivity;

            // re-center mouse position
            mousePos += new Vector3(Screen.width / 2, Screen.height / 2, 0);

            mousePos.z = Camera.main.transform.position.y - playerTransform.position.y;

            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 lookDirection = (worldPoint - playerTransform.position).normalized;
            Vector3 farPoint = playerTransform.position + lookDirection * 1000;

            farPoint.y = playerTransform.position.y;

            // rotate the transform to look at the world space point
            playerTransform.LookAt(farPoint);

            
        }

    }

}
