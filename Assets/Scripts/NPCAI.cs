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
    int currentDestinationIndex = 0;
    public float talkDistance = 1;
    public float runDistance = 4;
    float distanceToPlayer;
    public Canvas canvas;
    NavMeshAgent agent;
    public Transform npcEyes;
    public Transform chaseForward; //penguin's eyes face downward when in chase mode
    public float fieldOfView = 45f;

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
        else if (IsPlayerInClearFOV())
        {
            currentState = FSMStates.Run;
        }

        canvas.gameObject.SetActive(false);
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateRunState()
    {
        anim.SetInteger("animState", 2);

        nextDestination = player.transform.position;

        agent.stoppingDistance = talkDistance;

        if (distanceToPlayer <= talkDistance)
        {
            currentState = FSMStates.Idle;
        }
        else if (distanceToPlayer > runDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Walk;
        }

        canvas.gameObject.SetActive(true);
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    private void OnDrawGizmos() {

        Vector3 frontRayPoint = npcEyes.position + (npcEyes.forward * runDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        Vector3 front = chaseForward.position + (chaseForward.forward * runDistance);

        Debug.DrawLine(npcEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(npcEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(npcEyes.position, rightRayPoint, Color.yellow);

        Debug.DrawLine(chaseForward.position, front, Color.red);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;

        Vector3 directionToPlayer = player.transform.position - npcEyes.position;
        //directionToPlayer.y = npcEyes.position.y;

        if (Vector3.Angle(directionToPlayer, npcEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(npcEyes.position, directionToPlayer, out hit, runDistance))
            {
                print(Physics.Raycast(npcEyes.position, directionToPlayer, out hit, runDistance) + "hit");
                Debug.DrawRay(npcEyes.position, directionToPlayer, Color.green);
                print(player.transform.position + " position");
                print(directionToPlayer);
                print(hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Player"))
                {
                    print(hit.collider.CompareTag("Player") + "player");
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
