using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameBehavior : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;

    void Start()
    {
        ShowTextOne();
    }

    void Update()
    {
        
    }

    void ShowTextOne()
    {
        text1.gameObject.SetActive(true);
        Invoke("ShowTextTwo", 7);
    }

    void ShowTextTwo()
    {
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(true);
        Invoke("ShowTextThree", 7);
    }


    void ShowTextThree()
    {
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(true);
        Invoke("LoadNextLevel", 7);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene according to build order
    }
}

