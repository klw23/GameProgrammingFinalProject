using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

            Vector3 direction = mousePos - playerPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);

            // References: https://stackoverflow.com/questions/29559280/unity-rotate-towards-mouse-in-3d-topdown-view
        }
        
    }

}
