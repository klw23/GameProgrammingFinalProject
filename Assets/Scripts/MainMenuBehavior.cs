using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{

   


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene according to build order

    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

   
}
