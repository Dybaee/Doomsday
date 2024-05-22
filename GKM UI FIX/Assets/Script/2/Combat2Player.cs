using System.Collections.Generic;
using UnityEngine;

public class Combat2Player : MonoBehaviour
{
    public bool IsAttacking;
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    public GameObject swordUsed;
    public GameObject[] enemyPrefab;
    public float range = 5f;

    private ItemOnGround itemSword;
    Animator anim;
    int comboIndex; // Current combo index

    void Start()
    {
        anim = GetComponent<Animator>();
        comboIndex = 0; // First attack in the combo
        itemSword = GetComponent<ItemOnGround>();
        IsAttacking = false;
    }

    void Update()
    {
        if (swordUsed != null)
        {
            if (!IsAttacking && Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    PerformComboAttack();
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
                    foreach (var hitCollider in hitColliders)
                    {
                        if (hitCollider.gameObject != null && hitCollider.gameObject.CompareTag("Enemies"))
                        {
                            transform.LookAt(hitCollider.transform);
                        }
                    }
                }
                else if (Input.GetButtonDown("Fire2"))
                {
                    PerformRightButtonAttack();
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
                    foreach (var hitCollider in hitColliders)
                    {
                        if (hitCollider.gameObject != null && hitCollider.gameObject.CompareTag("Enemies"))
                        {
                            transform.LookAt(hitCollider.transform);
                        }
                    }

                }
            }
        }

    }

    void PerformComboAttack()
    {
        IsAttacking = true;
        string[] comboAttacks = { "Attack1", "Attack2" };

        // Trigger Combo
        anim.SetTrigger(comboAttacks[comboIndex]);

        // next combo attack
        comboIndex = (comboIndex + 1) % comboAttacks.Length;

        // Cooldown timer
        nextAttackTime = Time.time + attackCooldown;

        // Reset 
        Invoke("ResetAttackFlag", attackCooldown);
    }

    void PerformRightButtonAttack()
    {
        IsAttacking = true;
        if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("Attack3");

            nextAttackTime = Time.time + attackCooldown;

            Invoke("ResetAttackFlag", attackCooldown);
        }
    }

    void ResetAttackFlag()
    {
        IsAttacking = false;
    }

    public void UsingSword()
    {
        if (!itemSword)
        {
            IsAttacking = false;
            swordUsed = null;
        }
        else
        {
            swordUsed = itemSword.item;
        }
    }
}
