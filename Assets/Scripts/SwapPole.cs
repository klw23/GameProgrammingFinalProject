using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPole : MonoBehaviour
{
    public Color[] fishingPoleColor;
    public float[] fishingPoleSpeed;

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
        updatePoles();
    }

    // Update is called once per frame
    void Update()
    {
        print("swipswap" + PoleBehavior.isReeledIn + Input.GetMouseButton(2));
        if (PoleBehavior.isReeledIn) // if pole is reeled in
        {
            if (Input.GetMouseButtonDown(1)) // Right Click
            {
                print("hit");
                activePoleInt = (activePoleInt + 1) % totalNumOfPoles;
                updatePoles();
            }
        }
    }

    void updatePoles()
    {
        // Update Bob color to indicate new fishing rod
        Color currColor = fishingPoleColor[activePoleInt];
        fishingBobRenderer.material.SetColor("_Color", currColor);

        // Update Fishing Pole Speed 
        float currSpeed = fishingPoleSpeed[activePoleInt];
        PoleBehavior.projectileSpeed = currSpeed;
    }
}


