using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleBehaviorSimple : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    void Start()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(
                GameObject.FindGameObjectWithTag("ProjectileParent").transform);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
