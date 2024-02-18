using UnityEngine;

public class FishMovement : MonoBehaviour
{
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

        transform.Translate(Vector3.left * horizontalMovement);
        transform.position = new Vector3(transform.position.x, startPos.y + verticalMovement, transform.position.z);
        swimTimer += Time.deltaTime;

        if (swimTimer >= swimDuration)
        {
            swimTimer = 0f;
            transform.Rotate(0f, 180f, 0f); // switch directions
        }
    }
}
