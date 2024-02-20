using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightBehavior : MonoBehaviour
{

    public Color startColor;
    public Color endColor;
    public float speed = 0.5f;
    public float duration = 5f;

    private Light directionalLight;
    private float elapsedTime = 0f;
    private bool isChanging = true;

    void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    void Update()
    {
        if (isChanging)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the current color based on the elapsed time and transition duration
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / duration);

            // Set the color of the light
            directionalLight.color = currentColor;

            // If the transition is complete, stop the script
            if (elapsedTime >= duration)
            {
                isChanging = false;
            }
        }
    
    }
}
