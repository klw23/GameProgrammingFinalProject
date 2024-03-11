using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Swim,
        Chase, // for when it gets close enough to the bob
        Caught
    }

    public FSMStates currentState;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    public float fishSpeed = 2.0f;
    int currentDestinationIndex = 0;
    public float chaseDistance = 3.0f;
    public GameObject fishingBob;
    float distanceToFishingBob;
    public static int numberOfFishSpawned = 0;
    public int fishValue = 20;


   
    void Start()
    { 
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        fishingBob = GameObject.FindGameObjectWithTag("Bob");
        ShuffleWanderPoints();
        numberOfFishSpawned++;
        Initialize();
    }

    void Update()
    {
        distanceToFishingBob = Vector3.Distance(transform.position, fishingBob.transform.position);
     


        switch (currentState)
        {
            case FSMStates.Swim:
                UpdateSwimState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Caught:
                UpdateCaughtState();
                break;
        }
    }

    void ShuffleWanderPoints()
    {
        for (int i = 0; i < wanderPoints.Length; i++)
        {
            GameObject temp = wanderPoints[i];
            int randomIndex = Random.Range(i, wanderPoints.Length);
            wanderPoints[i] = wanderPoints[randomIndex];
            wanderPoints[randomIndex] = temp;
        }
    }

    void Initialize()
    {
        currentState = FSMStates.Swim;
        FindNextPoint(); 
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void UpdateSwimState()
    {
        print("Swimming!");
        if (Vector3.Distance(transform.position, nextDestination) < .5)
        {
            FindNextPoint();
        }
        else if (distanceToFishingBob <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, fishSpeed * Time.deltaTime);

    }

    void UpdateChaseState()
    {
        print("Chasing!");
        if (distanceToFishingBob <= chaseDistance) // if we are still within distance to fishing bob
        {
            FaceTarget(fishingBob.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, fishingBob.transform.position, fishSpeed * 2 * Time.deltaTime);
        }
        else // if we are no longer within distance to fishing bob
        {
            currentState = FSMStates.Swim;
        }

    }

    void UpdateCaughtState()
    {
        print("Caught!");
        numberOfFishSpawned--; // decrease number of fish spawned

        // fish value is currently 20, add this to the level manager to update points

        Destroy(gameObject); // temporary, it should follow the pole behavior of having the user click fire within a timeframe
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bob")
        {
            currentState = FSMStates.Caught;
        }
    }
}
