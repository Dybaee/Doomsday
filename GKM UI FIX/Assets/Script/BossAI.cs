using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    private Player2Controller playerController;
    private BossQuest questManager;

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
    public float knockbackForce = 10f;
    bool isKnockback = false;

    [SerializeField] private HPBOSS healthBar;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] GameObject healthUI_;

    void Start()
    {
        playerController = player.GetComponent<Player2Controller>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        questManager = FindObjectOfType<BossQuest>();
        currentHealth = health;
        healthBar.UpdateHealthBar(health, currentHealth);
        navAgent = GetComponent<NavMeshAgent>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isAlive)
        {
            attackTimer -= Time.deltaTime;

            if (InRange())
            {
                healthUI_.SetActive(true);
                Chase();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && InAggroRange())
                {
                    if (attackTimer <= 0)
                    {
                        if (isSignatureAttackReady)
                        {
                            SignatureAttack();
                        }
                        else
                        {
                            Attack();
                        }
                    }
                }
            }
            else
            {
                healthUI_.SetActive(false);
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
        navAgent.isStopped = true;
    }

    void Chase()
    {
        anim.SetBool("isRunning", true);
        navAgent.isStopped = false;
        navAgent.SetDestination(player.position);
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
        navAgent.isStopped = true;
        Destroy(gameObject, 5f);
        questManager.OnBossKilled();
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
            Debug.Log("Player Get Hit BOSS!");
            player.TakeDamage(enemyDamage);
        }
    }

    public void ApplyKnockback()
    {
        if (!isKnockback)
        {
            Vector3 knockbackDirection = (player.position - transform.position).normalized;
            player.GetComponent<Player2Controller>().ApplyKnockback(knockbackDirection, knockbackForce);
            isKnockback = true;
        }
    }
    void ResetKnockback()
    {
        isKnockback = false;
    }

    void TriggerKnockback()
    {
        ApplyKnockback();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            healthBar.UpdateHealthBar(health, currentHealth);
        }
    }
}