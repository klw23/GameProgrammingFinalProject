using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPole : MonoBehaviour
{
    public Color[] fishingPoleColor;
    public float[] fishingPoleSpeed;
    public GameObject[] fishingPoleUI;
    public GameObject[] fishingPoleStars;

    int activePoleInt;
    int totalNumOfPoles;
    GameObject fishingBob;
    Renderer fishingBobRenderer;

    void Start()
    {
        // locate fishing Rod
        fishingBob = GameObject.FindGameObjectWithTag("Bob");
        fishingBobRenderer = fishingBob.GetComponent<Renderer>();

        // find total number of fishing rod 
        if (fishingPoleColor.Length == fishingPoleSpeed.Length) // if total amt doesnt match - it will throw error
        {
            totalNumOfPoles = fishingPoleColor.Length;
        } 

        //must be at least one 
        activePoleInt = 0;
        updatePole();
        updatePoleUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (PoleBehavior.isReeledIn) // if pole is reeled in
        {
            if (Input.GetMouseButtonDown(1)) // Right Click
            {
                activePoleInt = (activePoleInt + 1) % totalNumOfPoles;
                updatePole();
                updatePoleUI();
            }
        }
    }

    void updatePole()
    {
        // Update Bob color to indicate new fishing rod
        Color currColor = fishingPoleColor[activePoleInt];
        fishingBobRenderer.material.SetColor("_Color", currColor);

        // Update Fishing Pole Speed 
        float currSpeed = fishingPoleSpeed[activePoleInt];
        PoleBehavior.projectileSpeed = currSpeed;
    }

    void updatePoleUI()
    {
        for(int i = 0; i < totalNumOfPoles; i++)
        {
            if (i == activePoleInt)
            {
                fishingPoleUI[i].SetActive(true);
            } 
            else
            {
                fishingPoleUI[i].SetActive(false);
            }
        }

        for (int i = 0; i < fishingPoleStars.Length; i++)
        {
            if ( i <= activePoleInt )
            {
                fishingPoleStars[i].SetActive(true);
            } 
            else
            {
                fishingPoleStars[i].SetActive(false);
            }
        }
    }
}


