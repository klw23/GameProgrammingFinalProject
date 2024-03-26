using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Walk,
        Run,
    }

    public FSMStates currentState;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;
    public float kittySpeed = 2.0f;
    int currentDestinationIndex = 0;

    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        ShuffleWanderPoints();

        anim = GetComponent<Animator>();

        Initialize();
    }

    void Update()
    {
        ShuffleWanderPoints();
        switch (currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Walk:
                UpdateWalkState();
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
        currentState = FSMStates.Walk;
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

    void UpdateIdleState()
    {
        anim.SetInteger("animState", 0);
    }

    void UpdateWalkState()
    {
        anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) < .5)
        {
            FindNextPoint();

        }

        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, kittySpeed * Time.deltaTime);
    }
}
