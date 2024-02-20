using UnityEngine;
using UnityEngine.Apple;

public class FishMovement : MonoBehaviour
{
    public GameObject bob;
    public float distanceToCatch = 1f;
    public float swimSpeed = 2f; 
    public float swimDuration = 20f; 
    public float swimHeight = 0.5f; 
    public float swimFrequency = 2f;
    
    private float swimTimer = 0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Swim();
    }

    private void Swim()
    {
        float horizontalMovement = swimSpeed * Time.deltaTime;
        float verticalMovement = Mathf.Sin(Time.time * swimFrequency) * swimHeight;

        transform.Translate(Vector3.forward * horizontalMovement);
        float fishToBobDist = Vector3.Distance(transform.position, bob.transform.position);

        if (fishToBobDist <= distanceToCatch)
        {
            transform.LookAt(bob.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, bob.transform.position, 1f * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, startPos.y + verticalMovement, transform.position.z);
        }

        swimTimer += Time.deltaTime;

        if (swimTimer >= swimDuration)
        {
            swimTimer = 0f;
            transform.Rotate(0f, 180f, 0f); // switch directions
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(FishingPoleBehavior2.reeledIn)
        {
            Destroy(gameObject);
        }
    }

}
