using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Transform tipOfFishingRod;
    public float Xoffset;
    public float Yoffset;
    public float Zoffset;
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the player

    void Update()
    {
        
        //Vector3 desiredPosition = player.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;

        Vector3 cameraPos = player.position;

        cameraPos.x = player.position.x + Xoffset;
        cameraPos.y = player.position.y + Yoffset;
        cameraPos.z = player.position.z + Zoffset;
         
        transform.LookAt(tipOfFishingRod); // Camera always looks at the player
    }
}
