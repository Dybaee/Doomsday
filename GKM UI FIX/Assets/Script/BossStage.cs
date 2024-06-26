using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStage : MonoBehaviour
{
    private Player2Controller playerController;

    public float speed;
    public float range;
    public float aggroRange;
    private Animator anim;
    private Rigidbody rb;

    private BoxCollider boxCollider;
    private bool isAlive = true;

    public Transform player;

    public float health = 1000f;
    public float enemyDamage;
    private float currentHealth;

    private bool isSignatureAttackReady = true;
    private float attackCooldown = 2f;
    private float attackTimer;


    [SerializeField] private HPENEMY healthBar;

    private state currentState;
    enum state{Idle, Chase, Attack, SignatureAttack }


    void Start()
    {
        playerController = player.GetComponent<Player2Controller>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        currentHealth = health;
    }

    void Update()
    {
        if (isAlive)
        {
            currentState = state.Idle;

            attackTimer -= Time.deltaTime;

            if (InRange() && currentState == state.Idle) 
            {
                currentState = state.Chase;
                if (currentState == state.Chase)
                {
                    Chase();
                }

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && InAggroRange())
                {
                    if (attackTimer <= 0)
                    {
                        if (isSignatureAttackReady)
                        {
                            currentState = state.SignatureAttack;
                            SignatureAttack();
                        }
                        else
                        {
                            currentState = state.Attack; 
                            Attack();
                        }
                    }
                }
            }
            else
            {
                Idle();
            }
        }
    }


    bool InRange()
    {
        return Vector3.Distance(transform.position, player.position) < range;
    }

    bool InAggroRange()
    {
        return Vector3.Distance(transform.position, player.position) < aggroRange;
    }

    void Idle()
    {
        anim.SetBool("isRunning", false);
    }

    void Chase()
    {
        anim.SetBool("isRunning", true);
        Vector3 moveDirection = (player.position - transform.position).normalized;
        if (Vector3.Distance (transform.position, player.position) >= aggroRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position - new Vector3(0, 0, 1), speed * Time.deltaTime);
            transform.LookAt(player.position);
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        transform.LookAt(player.position);
        attackTimer = attackCooldown;
    }

    void SignatureAttack()
    {
        anim.SetTrigger("SignatureAttack");
        StartCoroutine(SignatureAttackCD());
        isSignatureAttackReady = false;
        attackTimer = attackCooldown;
    }

    IEnumerator SignatureAttackCD()
    {
        yield return new WaitForSeconds(10f);
        isSignatureAttackReady = true;
    }

    void Die()
    {
        anim.SetTrigger("Die");
        isAlive = false;
        Destroy(gameObject, 5f);
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player2Controller>();

        if (player != null || other.CompareTag("Player"))
        {
            Debug.Log("Player Get Hit Bird!");
            player.TakeDamage(enemyDamage);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}