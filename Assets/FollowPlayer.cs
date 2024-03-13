using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // The player's transform
    public Vector3 offset; // Offset from the player
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the player

    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player); // Camera always looks at the player
    }
}
