using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    Animator anim;
    [SerializeField] WeaponDamage weapon;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
            if (Time.time - lastComboEnd > 0.5f && comboCounter < combo.Count)
            {
                if (Time.time - lastClickedTime >= 1f)
                {
                    anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                    anim.CrossFadeInFixedTime("Attack 1", 0, 0);
                    weapon.damage = combo[comboCounter].damage;
                    comboCounter++;
                    lastClickedTime = Time.time;
                }
            else
            {
                comboCounter = 0;
            }
        }
    }

    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 0.1f); // Decreased the delay to end combo
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
