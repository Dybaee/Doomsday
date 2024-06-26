using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    // Variable
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

    private bool isSignatureReady = true;
    private float attackCooldown = 2f;
    private float attackTimer;
    public float knockbackForce = 10f;
    bool isKnockback = false;

    [SerializeField] private HPBOSS healthBar;
    [SerializeField] GameObject healthUI_;
    [SerializeField] GameObject BossDeathFX;

    void Start()
    {
        playerController = player.GetComponent<Player2Controller>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        questManager = FindObjectOfType<BossQuest>();
        currentHealth = health;
        healthBar.UpdateHealthBar(health, currentHealth);
        rb.isKinematic = false;
    }

    void Update()
    {
        if (isAlive)
        {
            attackTimer -= Time.deltaTime;  // Decrease the attack timer

            if (InRange())
            {
                healthUI_.SetActive(true);
                Chase();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && InAggroRange()) // if not attacking and player is in aggro range
                {
                    if (attackTimer <= 0) // if attack cooldown is over
                    {
                        if (isSignatureReady)
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
        return Vector3.Distance(transform.position, player.position) < range; // Check if the player is within range
    }

    bool InAggroRange()
    {
        return Vector3.Distance(transform.position, player.position) < aggroRange; // Check if the player is within aggro range
    }

    void Idle()
    {
        anim.SetBool("isRunning", false);
        rb.velocity = Vector3.zero; // Stop movement
    }

    void Chase()
    {
        anim.SetBool("isRunning", true);
        Vector3 direction = (player.position - transform.position).normalized; // Calculate direction towards the player
        rb.velocity = direction * speed;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); // Boss look at player
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        transform.LookAt(player.position);
        attackTimer = attackCooldown; // reset attack timer
    }

    void SignatureAttack()
    {
        anim.SetTrigger("SignatureAttack");
        StartCoroutine(SignatureAttackCD());
        isSignatureReady = false;
        attackTimer = attackCooldown;
    }

    IEnumerator SignatureAttackCD()
    {
        yield return new WaitForSeconds(10f);
        isSignatureReady = true;
    }

    void Die()
    {
        StartCoroutine(DeathBoss());
    }

    private IEnumerator DeathBoss()
    {
        anim.SetTrigger("Die");
        isAlive = false;
        Destroy(healthUI_);
        rb.velocity = Vector3.zero; // Stop movement
        yield return new WaitForSeconds(5f);
        Instantiate(BossDeathFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
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
            Vector3 knockbackDirection = (player.position - transform.position).normalized; // Calculate knockback direction
            player.GetComponent<Player2Controller>().ApplyKnockback(knockbackDirection, knockbackForce); // Apply knockback to player
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
