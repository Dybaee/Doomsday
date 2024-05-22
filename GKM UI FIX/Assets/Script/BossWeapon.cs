using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    BoxCollider boxCollider;
    BossAI bossAI;
    PlayerController pController;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        bossAI = GetComponent<BossAI>();
        pController = GetComponent<PlayerController>();
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
        var player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            Debug.Log("Player Get Hit BOSS!");
            player.TakeDamage(bossAI.enemyDamage);
        }
    }


}
