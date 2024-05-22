using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class EnemyPatrol : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    Animator anim;
    Collider boxCollider;

    public Health health;

    // Patrol
    Vector3 destPoint;
    bool walkpointSet;
    [SerializeField] float range;

    // State change
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player"); // FindWithTag is preferred
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<Collider>(); // Changed to Collider interface
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSight && !playerInAttackRange)
            Patrol();
        else if (playerInSight && !playerInAttackRange)
            Chase();
        else if (playerInSight && playerInAttackRange)
            Attack();
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
            agent.ResetPath(); // Clear path when attacking
        }
    }

    void Patrol()
    {
        if (!walkpointSet)
            SearchForDest();
        else
            agent.SetDestination(destPoint);

        if (Vector3.Distance(transform.position, destPoint) < 1f) // Adjusted distance
            walkpointSet = false;
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        RaycastHit hit;
        if (Physics.Raycast(destPoint, Vector3.down, out hit, Mathf.Infinity, groundLayer)) // Fixed Physics.Raycast call
        {
            walkpointSet = true;
        }
    }

    void EnableAttack()
    {
        if (boxCollider != null)
            boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            Debug.Log("Hit");
        }
    }
}