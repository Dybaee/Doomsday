using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossAII : MonoBehaviour
{
    private BossState currentState;
    public float health;
    public float currentHealth;

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
    public float knockbackForce = 10f;
    bool isKnockback = false;
    private Animator FTB;
    private BossQuest questManager;
    [SerializeField] GameObject BossDeathFX;
    [SerializeField] GameObject healthUI_;
    public Slider healthSlider;

    //public string sceneName;

    void Start()
    {
        ChangeState(new BossIdleState(this));
        anim = GetComponent<Animator>();
        timer = Attacktimer;
        currentHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
        questManager = FindObjectOfType<BossQuest>();
        FTB = GameObject.FindGameObjectWithTag("ftb").GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAlive) return;
        currentState.Update();

        if (PlayerInRange())
        {
            healthUI_.SetActive(true);
        }
        else
        {
            healthUI_.SetActive(false);
        }

        if (timer <= 0 && !isUlti && PlayerInRange())
        {
            isUlti = true;
            ChangeState(new BossSignatureAttack(this));
            timer = Attacktimer;  // Reset 
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
        
    }

    public void ChasePlayer()
    {
        if (!isAlive) return;

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
        if (!isAlive) return;

        anim.SetTrigger("Attack");
        transform.LookAt(player.position);
    }

    public void Defeated()
    {
        StartCoroutine(DeathBoss());
    }

    private IEnumerator DeathBoss()
    {
        anim.SetTrigger("Die");
        isAlive = false;
        Destroy(healthUI_);
        rb.isKinematic = true;  // Disable Rigidbody physics 
        rb.velocity = Vector3.zero; // Stop movement
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        questManager?.OnBossKilled();
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        Instantiate(BossDeathFX, transform.position, Quaternion.identity);
        //FTB.Play("FadeToBlack");
        //SceneManager.LoadScene(sceneName);
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        boxCollider.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player2Controller>();

        if (player != null || other.CompareTag("Player"))
        {
            Debug.Log(enemyDamage);
            player.TakeDamage(enemyDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        currentHealth = health;
        if (health <= 0 )
        {
            Defeated();
        }
        UpdateBossHealthUI();
    }

    void UpdateBossHealthUI()
    {
        healthSlider.value = health;
    }

}