using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleBehavior : MonoBehaviour
{
    public Transform bobAnchor;
    SpringJoint spring;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = transform.GetComponent<LineRenderer>();
        spring = transform.GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.GetChild(0).gameObject.transform.position);
        lr.SetPosition(1, bobAnchor.position);
    }
}
