using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public GameObject player;
    public float kittyWalkSpeed = 0.8f;

    public float kittyRunSpeed = 1.5f;
    float kittySpeed;
    int currentDestinationIndex = 0;
    public float talkDistance = 1;
    public float runDistance = 4;
    float distanceToPlayer;
    public Canvas canvas;
    NavMeshAgent agent;

    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        ShuffleWanderPoints();

        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        
        agent = GetComponent<NavMeshAgent>();

        Initialize();       
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        ShuffleWanderPoints();
        switch (currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Walk:
                UpdateWalkState();
                break;
            case FSMStates.Run:
                UpdateRunState();
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
        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void UpdateIdleState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= talkDistance)
        {
            currentState = FSMStates.Idle;
        }
        else if (distanceToPlayer > talkDistance && distanceToPlayer <= runDistance)
        {
            currentState = FSMStates.Run;
        }
        else if (distanceToPlayer > runDistance)
        {
            currentState = FSMStates.Walk;
        }

        FaceTarget(nextDestination);

        anim.SetInteger("animState", 0);
    }

    void UpdateWalkState()
    {
        anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;

        if (Vector3.Distance(transform.position, nextDestination) < .5)
        {
            FindNextPoint();

        }
        else if (distanceToPlayer <= runDistance)
        {
            currentState = FSMStates.Run;
        }

        canvas.gameObject.SetActive(false);
        kittySpeed = kittyWalkSpeed;
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, kittySpeed * Time.deltaTime);
    }

    void UpdateRunState()
    {
        anim.SetInteger("animState", 2);

        agent.stoppingDistance = talkDistance;

        nextDestination = player.transform.position;

        if (distanceToPlayer <= talkDistance)
        {
            currentState = FSMStates.Idle;
        }
        else if (distanceToPlayer > runDistance)
        {
            currentState = FSMStates.Walk;
        }

        canvas.gameObject.SetActive(true);
        kittySpeed = kittyRunSpeed;
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, kittySpeed * Time.deltaTime);
    }
}
