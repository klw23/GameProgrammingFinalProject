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

    public Text addedTimeText;
    public Text boostedModeText;

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
                if (currentScore >= winScore)
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

    void SetScoreText()
    {
        scoreText.text = currentScore.ToString() + " / " + winScore.ToString();
    }

    void SetTimerText()
    {
        TimerText.text = countdown.ToString("f2");
    }

    public void ShowAddedTime(int amount)
    {
        addedTimeText.text = "+" + amount.ToString() + "SECONDS"; 
        addedTimeText.gameObject.SetActive(true);

        StartCoroutine(FadeOutAddedTimeText()); // Start the fade out coroutine
    }

    IEnumerator FadeOutAddedTimeText()
    {
        float duration = 3f; // Duration in seconds
        float startTime = Time.time;

        // Ensure the text is fully opaque at the start
        Color textColor = addedTimeText.color;
        textColor.a = 1;
        addedTimeText.color = textColor;

        // Gradually fade the text
        while (Time.time < startTime + duration)
        {
            // Fade the text based on the elapsed time
            float t = (Time.time - startTime) / duration;
            textColor.a = Mathf.Lerp(1, 0, t);
            addedTimeText.color = textColor;
            yield return null;
        }

        addedTimeText.gameObject.SetActive(false); // Hide the text after fading
    }

    public void ShowBoostedMode()
    {
        boostedModeText.text = "Boosted Mode: 10 seconds!";
        boostedModeText.gameObject.SetActive(true);
        StartCoroutine(FadeOutBoostedTimeText());
    }

    IEnumerator FadeOutBoostedTimeText()
    {
        float duration = 3f; // Duration in seconds
        float startTime = Time.time;

        // Ensure the text is fully opaque at the start
        Color textColor = boostedModeText.color;
        textColor.a = 1;
        boostedModeText.color = textColor;

        // Gradually fade the text
        while (Time.time < startTime + duration)
        {
            // Fade the text based on the elapsed time
            float t = (Time.time - startTime) / duration;
            textColor.a = Mathf.Lerp(1, 0, t);
            boostedModeText.color = textColor;
            yield return null;
        }

        boostedModeText.gameObject.SetActive(false); // Hide the text after fading
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


