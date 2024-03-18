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

    public AudioClip yaySFX;

    float countDown;
    public float timeToCatch = 1f;
    public static bool poleOccupied = false;

    public GameObject splashEffectPrefab;
    private GameObject splashEffectInstance;

    private bool splashEffectPlayed;

    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        fishingBob = GameObject.FindGameObjectWithTag("Bob");
        ShuffleWanderPoints();
        numberOfFishSpawned++;
        splashEffectPlayed = false;
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
        //print("Swimming!");
        if (Vector3.Distance(transform.position, nextDestination) < .5)
        {
            FindNextPoint();
        }
        else if (distanceToFishingBob <= chaseDistance && !poleOccupied && !PoleBehavior.isReeledIn) // only if pole is not cast and pole is not occupied and is within distance
        {
            poleOccupied = true;
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, fishSpeed * Time.deltaTime);

    }

    void UpdateChaseState()
    {
        //print("Chasing!");
        if (distanceToFishingBob <= chaseDistance) // if we are still within distance to fishing bob
        {
            FaceTarget(fishingBob.transform.position);
            Vector3 targetPosition = new Vector3(fishingBob.transform.position.x, transform.position.y, fishingBob.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, fishSpeed * 2 * Time.deltaTime);
        }
        else // if we are no longer within distance to fishing bob
        {
            currentState = FSMStates.Swim;
            Invoke("KeepPoleOccupied", 3); // don't allow other fish to chase the pole for X seconds
        }

    }

    void UpdateCaughtState()
    {
        print("Caught!");

        if (countDown > 0)
        {

            if (!splashEffectPlayed)
            {

                // play particle effect
                splashEffectInstance = Instantiate(splashEffectPrefab, transform.position, Quaternion.identity, transform);
                splashEffectPlayed = true;
                Destroy(splashEffectInstance, 3);

            }

            countDown -= Time.deltaTime;

            if (PoleBehavior.isReeledIn)
            {
                AudioSource.PlayClipAtPoint(yaySFX, transform.position);
                LevelManagerBehavior.currentScore += fishValue;
                countDown = 0;
                poleOccupied = false;
                // destroy particle effect
                Destroy(splashEffectInstance, 3);
                numberOfFishSpawned--; // decrease number of fish spawned
                Destroy(gameObject);

            }
            else if (!PoleBehavior.isReeledIn && countDown <= 0)
            {
                print("unsuccesful catch");
                Destroy(splashEffectInstance, 3);
                FindNextPoint();
                Invoke("KeepPoleOccupied", 3);
                currentState = FSMStates.Swim; // if the fish was unsuccessfuly caught, have it keep swimming
            }
        }

    }

    private void KeepPoleOccupied() // keep the pole occupied for 3 seconds to allow the fish that was unsuccessfully caught to swim to next wander point
    {
        poleOccupied = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bob")
        {
            countDown = timeToCatch;
            currentState = FSMStates.Caught;
        }
    }
}