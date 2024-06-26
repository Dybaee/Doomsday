using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAII : MonoBehaviour
{
    private BossState currentState;
    public float health = 100;
    private float currentHealth;

    public float range;
    public float aggroRange;
    public Transform player;
    [field:SerializeField] public Animator anim {get; private set;}
    public float speed;
    [field: SerializeField] public Rigidbody rb { get; private set; }
    public BoxCollider boxCollider;
    private bool isAlive = true;
    public float enemyDamage;
    public bool isUlti;
    public float timer;
    public float Attacktimer = 10f;
    public GameObject uiHP;
    

    void Start()
    {
        ChangeState(new BossIdleState(this));
        anim = GetComponent<Animator>();
        timer = Attacktimer;
    }

    void Update()
    {
        currentState.Update();

        if (timer < 0)
        {
            if (!isUlti)
            {
                isUlti = true;
                ChangeState(new BossSignatureAttack(this));
            }
        }
        timer -= Time.deltaTime;
    }

    public void ChangeState(BossState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) < range;
    }

    public bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) < aggroRange;
    }

    public void Patrol()
    {
        // Implement patrol behavior
    }

    public void ChasePlayer()
    {
        anim.SetFloat("MovementSpeed", 1);
        Vector3 moveDirection = (player.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.position) >= aggroRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position - new Vector3(0, 0, 1), speed * Time.deltaTime);
            transform.LookAt(player.position);
        }
    }

    public void AttackPlayer()
    {
        anim.SetTrigger("Attack");
        transform.LookAt(player.position);
    }

    public void Defeated()
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
            Defeated();
        }
    }

}