using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    public BossAttackState(BossAII boss) : base(boss) { }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        boss.rb.velocity = Vector3.zero;
    }

    public override void Update()
    {
        // Attack behavior
        boss.AttackPlayer();

        // Transition logic
        if (!boss.PlayerInAttackRange())
        {
            boss.ChangeState(new BossChaseState(boss));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}
