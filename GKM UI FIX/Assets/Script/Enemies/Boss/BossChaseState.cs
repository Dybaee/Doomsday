using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossState
{
    public BossChaseState(BossAII boss) : base(boss) { }

    public override void Enter()
    {
        Debug.Log("Entering Chase State");
    }

    public override void Update()
    {
        // Chase behavior
        boss.ChasePlayer();

        // Transition logic
        if (boss.PlayerInAttackRange())
        {
            boss.ChangeState(new BossAttackState(boss));
        }

        if (!boss.PlayerInRange())
        {
            boss.ChangeState(new BossIdleState(boss));
        }
        //else if (!boss.PlayerInRange())
        //{
        //    boss.ChangeState(new BossPatrolState(boss));
        //}
    }

    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
    }
}
