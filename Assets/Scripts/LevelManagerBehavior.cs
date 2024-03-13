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
    public Text scoreText;
    public Text GameOverText;
    public Slider moneySlider;
    public string nextLevel;
    public static bool isGameOver = false;
    public int winScore = 100;


    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    

    void Start()
    {
        isGameOver = false;
        countdown = levelDuration;
        currentScore = 0;
        SetScoreText();
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        moneySlider.value = currentScore;
        if (!isGameOver)
        {
            if (countdown > 0)
            {
                if (currentScore == winScore)
                {
                    LevelBeat();
                }
                countdown -= Time.deltaTime;
            }
            else
            {
                countdown = 0.0f;
                LevelLost();
            }
        }
        SetScoreText();
        SetTimerText();
    }

    public void SetScore(int scoreValue)
    {
        currentScore += scoreValue;
    }

    void SetScoreText()
    {
        scoreText.text = currentScore.ToString() + " / " + winScore.ToString();
    }

    void SetTimerText()
    {
        TimerText.text = countdown.ToString("f2");
    }

    void LevelLost()
    {
        isGameOver = true;
        GameOverText.text = "LEVEL LOST!";
        GameOverText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 1;
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        GameOverText.text = "LEVEL WON!";
        GameOverText.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }

        else 
        {
            Camera.main.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        }
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
    public void IncreaseTime(int amount) 
    {
        countdown += amount; 
        SetTimerText(); 
    }


}


