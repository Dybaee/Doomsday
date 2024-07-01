using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EnemyAI : MonoBehaviour
{

    GameObject player;

    PlayerController pController;
    HpOnGround hpDrop;

    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] GameObject SmokeDeathFX;

    Animator anim;
    BoxCollider boxCollider;

    //public Health health;
    public float HP;
    public float maxHP = 25;
    public Slider healthSlider;



    public float enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        pController = GetComponentInChildren<PlayerController>();
        hpDrop = GetComponentInChildren<HpOnGround>();
        HP = maxHP;
        healthSlider.maxValue = maxHP;
        healthSlider.value = HP;
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

        if (player != null)
        {
            Debug.Log("Player Get Hit");
            player.TakeDamage(enemyDamage);
        }
    }

    public void Die()
    {
        //anim.SetTrigger("Die");
        //Debug.Log("Enemy Died!");

        //Destroy(gameObject, 3f);
        StartCoroutine(DeathSequence());
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Destroy(healthSlider);
            Die();
            hpDrop.DropHP();
        }
        Debug.Log("Current Health after damage: " + HP);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthSlider.value = HP;
    }

    private IEnumerator DeathSequence()
    {
        anim.SetTrigger("Die");
        Debug.Log("Enemy Died!");
        yield return new WaitForSeconds(3f);
        Instantiate(SmokeDeathFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
