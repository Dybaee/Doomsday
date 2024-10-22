using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNewScript : MonoBehaviour
{
    public float chaseSpeed = 2f;
    public float aggroRange = 3f;
    public float attackRange = 5f;
    public float attackCD = 2f; // Set a default cooldown (seconds)
    public Transform player;
    public LayerMask obstacleMask; // Layer mask for obstacles that can hide the player

    private Vector3 startPosition;
    private float nextAttackTime = 0f;
    private bool isChasing = false;
    private bool isAttacking = false;
    private bool inRange = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !isAttacking)
        {
            if (PlayerInSight())
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }
        else
        {
            isChasing = false;
        }

        if (isChasing && !isAttacking)
        {
            ChasePlayer();
        }

        if (isAttacking)
        {
            AttackPlayer();
        }

        anim.SetFloat("MovementSpeed", isChasing ? chaseSpeed : 0f);
    }


    private bool PlayerInSight()
    {
        // Cast a ray from the boss to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the ray hits anything between the boss and the player
        if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
        {
            // player is visible
            return true;
        }
        // player is hidden
        return false;
    }

    private void ChasePlayer()
    {
        anim.SetFloat("MovementSpeed", 1);
        Vector3 moveDirection = (player.position - transform.position).normalized;

        // Move towards the player
        if (Vector3.Distance(transform.position, player.position) > aggroRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            transform.LookAt(player.position);
        }

        // Check if player is within attack range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            isChasing = false;
            isAttacking = true;
        }
    }

    private void AttackPlayer()
    {
        // Attack the player when cooldown 
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Enemy is attacks!");
            anim.SetTrigger("Attack");
            transform.LookAt(player.position);

            nextAttackTime = Time.time + attackCD;
        }

        // If the player is still within attack range, keep attacking
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
            isChasing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            isChasing = true;
            anim.SetFloat("MovementSpeed", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            isChasing = false;
            anim.SetFloat("MovementSpeed", 0);
        }
    }
}
