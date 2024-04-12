using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuBehavior : MonoBehaviour
{

    public static bool isGamePaused = false;
    public GameObject pauseMenu;

    void Start()
    {
        isGamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ExitScreenButton()
    {
        print("clicking exit screen");
        pauseMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        //Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
