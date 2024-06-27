using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrolState : BossState
{
    public BossPatrolState(BossAII boss) : base(boss) { }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Update()
    {
        // Patrol behavior
        boss.Patrol();

        // Transition logic
        if (boss.PlayerInRange())
        {
            boss.ChangeState(new BossChaseState(boss));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}
