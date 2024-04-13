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
        print("FOUND NEXT " + nextDestination);
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
        print("IDLE");
        nextDestination = playerCenter;

        if (agent.remainingDistance <= talkDistance)
        {
            currentState = FSMStates.Idle;
            PlayerAndRodController.isChatting = true;
        }
        else if (agent.remainingDistance > talkDistance && agent.remainingDistance <= runDistance)
        {
            currentState = FSMStates.Run;
            PlayerAndRodController.isChatting = false;
        }
        else if (agent.remainingDistance > runDistance)
        {
            currentState = FSMStates.Walk;
            PlayerAndRodController.isChatting = false;
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
        anim.SetInteger("animState", 0);
    }

    void UpdateWalkState()
    {
        //Patrolling
        print("WALK");
        anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;
        print(distanceToPlayer + " DIST PLAY");

        if (agent.remainingDistance < 0.5)
        {
            FindNextPoint();

        }
        else if ((distanceToPlayer <= runDistance) && IsPlayerInClearFOV())
        {
            //nextDestination = playerCenter;
            currentState = FSMStates.Run;
        }

        canvas.gameObject.SetActive(false);
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateRunState()
    {
        //Chasing
        print("RUN");
        anim.SetInteger("animState", 2);

        nextDestination = playerCenter;

        agent.stoppingDistance = talkDistance;
        // print(agent.transform.position + " agent's position");
        // print(agent.destination + " agent destination");
        // print(agent.remainingDistance + " remain dist");
        
        if (agent.remainingDistance <= talkDistance)
        {
            currentState = FSMStates.Idle;
        }
        else if (agent.remainingDistance > runDistance)
        {
            //FindNextPoint();
            currentState = FSMStates.Walk;
        }

        canvas.gameObject.SetActive(true);
        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * runDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) *frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        //Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        //Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }

    bool IsPlayerInClearFOV() {
        RaycastHit hit;

        Vector3 directionToPlayer = playerCenter - enemyEyes.position;
        //print(directionToPlayer + " dir to player");
        //print("angle " + Vector3.Angle(directionToPlayer, enemyEyes.forward));
        print(Vector3.Angle(directionToPlayer, enemyEyes.forward) + " ANGLE");
        print(fieldOfView + " FOV");
        print(directionToPlayer + " DIR PLAYER FOV");
        Debug.DrawRay(enemyEyes.position, directionToPlayer);

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView) {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, runDistance)) {
                print(hit.collider.gameObject.name + " HIT COLLIDER");
                //Debug.DrawRay(enemyEyes.position, directionToPlayer); //raycast is not shooting?
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
