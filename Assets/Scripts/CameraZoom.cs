using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraZoom : MonoBehaviour
{
    public GameObject panel;
    public Transform player;
    public float zoomSpeed = 1f;
    public float heightOffset = 3f;
    public float playerZoomDistance = 5f;
    public TMP_Text congratulationsText;
    private bool isZooming = false;

    void Start()
    {
        if (player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
        }
        
        // hide text
        congratulationsText.alpha = 0;
        panel.SetActive(false);
    }

    void Update()
    {
        if (player != null && !isZooming)
        {
            
            Vector3 playerPosition = player.position + (transform.position - player.position).normalized * playerZoomDistance;
            playerPosition.y = player.position.y + heightOffset; 
            if (Vector3.Distance(transform.position, playerPosition) > 0.1f)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, zoomSpeed * Time.deltaTime);
            }
            else
            {
                // Stop zooming
                isZooming = true;
                // Show text
                congratulationsText.alpha = 1;
                panel.SetActive(true);
            }
        }
    }
}
