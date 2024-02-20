using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // GameObject projectile = Instantiate(projectilePrefab, transform.GetChild(0).gameObject.transform.position,
            //    transform.rotation) as GameObject; //instantiates projectile 1m in front of camera

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.GetChild(0).gameObject.transform.forward * projectileSpeed, ForceMode.VelocityChange);

        } //else
        //{
        //    // Keep the bob 0.3f below the tip of the fishing pole
        //    projectile.transform.position = transform.GetChild(0).gameObject.transform.position - new Vector3(0, 0.3f, 0);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.CompareTag("Water"))
        {
            Debug.Log("Water Hit");
        }
    }
}
