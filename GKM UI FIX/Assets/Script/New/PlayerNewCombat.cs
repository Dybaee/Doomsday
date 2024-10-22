using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewCombat : MonoBehaviour
{

    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;

    //Item Interact
    public GameObject crownUsed;
    public GameObject swordUsed;

    private bool swordWasActive;

    public GameObject[] enemyPrefab;
    public float range = 5f;

    Animator anim;
    [SerializeField] WeaponDamage weapon;
    [SerializeField] Player2Controller playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (playerMovement == null)
        {
            playerMovement = GetComponent<Player2Controller>();
        }
        swordWasActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (swordUsed != null && swordUsed.activeInHierarchy)
        {
            swordWasActive = true;

            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
                foreach (var hitCollider in hitColliders)
                {
                    // If an enemy is detected, look at the enemy
                    if (hitCollider.gameObject != null && hitCollider.gameObject.CompareTag("Enemies"))
                    {
                        transform.LookAt(hitCollider.transform);
                    }
                }
            }
            else
            {
                
            }
        }

        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.8f)
            {
                playerMovement.enabled = false;

                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                weapon.damage = combo[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter + 1 > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
            playerMovement.enabled = true;
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
