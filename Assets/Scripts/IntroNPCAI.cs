using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IntroNPCAI : MonoBehaviour
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
    int currentDestinationIndex = 0;
    public float talkDistance = 1;
    public float runDistance = 4;
    float distanceToPlayer;
    public Canvas canvas;
    NavMeshAgent agent;
    public Transform enemyEyes;
    public float fieldOfView = 45f;
    Vector3 playerCenter;

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
        playerCenter = player.transform.position;
        playerCenter.y = enemyEyes.transform.position.y;
        distanceToPlayer = Vector3.Distance(playerCenter, agent.nextPosition);

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
        //Talking
        nextDestination = playerCenter;

        if (agent.remainingDistance <= talkDistance)
        {
            currentState = FSMStates.Idle;
        }
        else if (agent.remainingDistance > talkDistance && agent.remainingDistance <= runDistance)
        {
            currentState = FSMStates.Run;
        }
        else if (agent.remainingDistance > runDistance)
        {
            currentState = FSMStates.Walk;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        anim.SetInteger("animState", 0);
    }

    void UpdateWalkState()
    {
        //Patrolling
        anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;

        if (agent.remainingDistance < 0.5)
        {
            FindNextPoint();

        }
        else if ((distanceToPlayer <= runDistance) && IsPlayerInClearFOV())
        {
            currentState = FSMStates.Run;
        }

        canvas.gameObject.SetActive(false);
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateRunState()
    {
        //Chasing
        anim.SetInteger("animState", 2);

        nextDestination = playerCenter;

        agent.stoppingDistance = talkDistance;
        
        if (agent.remainingDistance <= talkDistance)
        {
            currentState = FSMStates.Idle;
        }
        else if (agent.remainingDistance > runDistance)
        {
            currentState = FSMStates.Walk;
        }

        canvas.gameObject.SetActive(true);
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    private void OnDrawGizmos() {
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * runDistance);

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
    }

    bool IsPlayerInClearFOV() {
        RaycastHit hit;

        Vector3 directionToPlayer = playerCenter - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView) {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, runDistance)) {
                if(hit.collider.CompareTag("Player")) {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
