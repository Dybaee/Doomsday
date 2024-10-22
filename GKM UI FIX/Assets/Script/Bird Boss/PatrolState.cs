using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    float patrolTimer;
    float idleTimer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    public float chaseRange = 8;
    public float patrolInterval = 10f;
    public float idleChance = 0.3f;  // 30% chance to remain idle
    public float minIdleTime = 2f;
    public float maxIdleTime = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        patrolTimer = 0;
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in go.transform)
            wayPoints.Add(t);

        // Randomize whether the enemy starts patrolling or idling
        if (Random.value > idleChance)
        {
            // Start patrolling
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }
        else
        {
            // Stay idle for a random amount of time
            idleTimer = Random.Range(minIdleTime, maxIdleTime);
            agent.SetDestination(agent.transform.position);  // Stop the agent
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolTimer += Time.deltaTime;

        if (agent.remainingDistance <= agent.stoppingDistance && patrolTimer >= idleTimer)
        {
            // Once idle time is over, start patrolling
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            patrolTimer = 0;  // Reset patrol timer
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            animator.SetBool("IsChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);  // Stop the agent on state exit
    }
}