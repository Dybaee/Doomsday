using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    GameObject player;

    PlayerController pController;
    HpOnGround hpDrop;

    [SerializeField] LayerMask groundLayer, playerLayer;

    Animator anim;
    BoxCollider boxCollider;

    //public Health health;
    public float HP;
    public float maxHP = 25;

    [SerializeField] private HPENEMY _healthbar;

    public float enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        pController = GetComponentInChildren<PlayerController>();
        hpDrop = GetComponentInChildren<HpOnGround>();
        HP = maxHP;
        _healthbar.UpdateHealthBar(maxHP, HP);
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
        anim.SetTrigger("Die");
        Debug.Log("Enemy Died!");

        Destroy(gameObject, 3f);
    }

    public void TakeDamage(float amount)
    {
        HP -= Random.Range(0.5f, 1.5f);
        if (HP <= 0)
        {
            Die();
            hpDrop.DropHP();
        }
        else
        {
            _healthbar.UpdateHealthBar(maxHP, HP);
        }
    }


}
