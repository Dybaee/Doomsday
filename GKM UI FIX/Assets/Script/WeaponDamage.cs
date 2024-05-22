using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage;

    public float addDamage = 7f;

    Item2OnGround itemCrown;

    BoxCollider triggerBox;
    EnemyAI enemy;
    BossAI bossAI;

    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        enemy = GetComponent<EnemyAI>();
        bossAI = GetComponent<BossAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            Debug.Log("Enemy Get Hit");
            enemy.TakeDamage(damage);
            if (itemCrown != null)
            {
                enemy.TakeDamage(addDamage);
            }
        }
        else
        {
            var boss = other.gameObject.GetComponent<BossAI>();
            {
                if (boss != null)
                {
                    Debug.Log("Boss get Hit");
                    boss.TakeDamage(damage);
                    boss.TakeDamage(addDamage);
                }
                
            }
        }
        
    }
}
