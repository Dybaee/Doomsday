using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    Player2Controller pController;
    private BossQuest questManager;

    public float speed;
    public float range;
    public float aggroRange;
    private Animator anim;
    private Rigidbody rb;

    BoxCollider boxCollider;

    private bool isAlive = true;

    public Transform player;

    public float health = 1000f;
    public float enemyDamage;
    public float HP;


    private float signatureAttackCooldown = 0f;
    private bool isSignatureAttackReady = true;

    [SerializeField] private HPENEMY _healthbar;

    void Start()
    {
        pController = GetComponent<Player2Controller>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        questManager = FindObjectOfType<BossQuest>();
        HP = health;
        _healthbar.UpdateHealthBar(health, HP);
    }

    void Update()
    {
        if (!isDead())
        {
            if (!inRange())
            {
                Idle();
            }
            else
            {
                Chase();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && inAggroRange())
                {
                    if (isSignatureAttackReady) // Check if signature attack is ready
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
            Die();
        }
    }

    bool inRange()
    {

        if (Vector3.Distance(transform.position, player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool inAggroRange()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Idle()
    {
        anim.SetBool("isRunning", false);
    }

    void Chase()
    {
        transform.LookAt(player.position);
        //controller.SimpleMove(transform.forward * speed);
        Vector3 direction = player.position - transform.position;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        GetComponent<Animator>().SetBool("isRunning", true);
    }

    void Attack()
    {
       anim.SetTrigger("Attack");
       transform.LookAt(transform.position);
    }

    void SignatureAttack()
    {
        anim.SetTrigger("SignatureAttack");
        //transform.LookAt(transform.position);

        // Set cooldown 
        StartCoroutine(SignatureAttackCooldown());
        isSignatureAttackReady = false;
    }

    IEnumerator SignatureAttackCooldown()
    {
        // Set cooldown 
        float cooldownDuration = 10f;

        // Wait for cooldown duration
        yield return new WaitForSeconds(cooldownDuration);

        isSignatureAttackReady = true;
    }

    void Die()
    {
        anim.SetTrigger("Die");
        isAlive = false;
        Destroy(gameObject, 5f);
        questManager.OnBossKilled();
    }
    bool isDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public void TakeDamage(float amount)
    {
        HP -= Random.Range(0.5f, 1.5f);
        if (HP <= 0) 
        {
            Die();
        }
        else
        {
            _healthbar.UpdateHealthBar(health, HP);
        }
    }

}
