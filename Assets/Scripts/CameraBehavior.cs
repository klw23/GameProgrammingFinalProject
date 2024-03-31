using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehavior : MonoBehaviour
{
    public float zoomOutSpeed = 0.1f;
    public float zoomOutDuration = 5;
    Camera mainCamera;
    bool zoomingOut = true;
    float zoomOutTimer = 0f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (zoomingOut)
        {
            mainCamera.transform.Translate(Vector3.back * zoomOutSpeed * Time.deltaTime);
            zoomOutTimer += Time.deltaTime;

            if (zoomOutTimer >= zoomOutDuration)
            {
                zoomingOut = false;
                Invoke("LoadNextLevel", 3);
            }
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene according to build order
    }
}
