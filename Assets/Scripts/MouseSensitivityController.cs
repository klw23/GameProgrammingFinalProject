using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MouseSensitivityController : MonoBehaviour
{

    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivityValue;
    float sliderValue;

    void Start()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
        sliderValue = PlayerPrefs.GetFloat("MouseSensitivity", 50.0f);
        mouseSensitivitySlider.value = sliderValue;
        UpdateSensitivityText(sliderValue);
    }

    private void Update()
    {
        if (sliderValue != mouseSensitivitySlider.value)
        {
            UpdateSensitivityText(mouseSensitivitySlider.value);
        }
    }
    void ChangeSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }


    void UpdateSensitivityText(float s)
    {
        mouseSensitivityValue.text = s.ToString();
    }
}
