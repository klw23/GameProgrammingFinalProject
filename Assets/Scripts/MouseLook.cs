using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (!PoleBehavior.isReeledIn)
        {
            // do not let player move
        } else
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 WorldPoint = Camera.main.WorldToScreenPoint(mousePos);
            WorldPoint.y = transform.position.y;
            transform.LookAt(-WorldPoint);
        }

    }

}
