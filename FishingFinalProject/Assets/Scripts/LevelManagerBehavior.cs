using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerBehavior : MonoBehaviour
{
    public float levelDuration = 10f;
    public float countdown;
    public Text TimerText;
    public Text GameOverText;
    public string nextLevel;

    void Start()
    {
        countdown = levelDuration;
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = 0.0f;
            LevelLost();
        }

        SetTimerText();
        
    }

    void SetTimerText()
    {
        TimerText.text = countdown.ToString("f2");

    }

    void LevelLost()
    {
        GameOverText.gameObject.SetActive(true);
        Invoke("LoadCurrentLevel", 2);
    }


    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel()
    {
        if (nextLevel != null)
        {
            SceneManager.LoadScene(nextLevel);

        }
    }


}


