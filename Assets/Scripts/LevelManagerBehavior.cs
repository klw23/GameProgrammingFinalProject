using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerBehavior : MonoBehaviour
{
    public static int currentScore = 0;

    public float levelDuration = 10f;
    public float countdown;
    public Text TimerText;
    public Text GameOverText;
    public Slider moneySlider;
    public string nextLevel;
    public int winScore = 100;

    void Start()
    {
        countdown = levelDuration;
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        moneySlider.value = currentScore;
        if (countdown > 0)
        {
            if(currentScore == winScore)
            {
                LoadNextLevel();
            }
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


