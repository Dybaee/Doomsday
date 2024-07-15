using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public BossIdleState(BossAII boss) : base(boss) { }


    public override void Enter()
    {
        boss.anim.SetFloat("MovementSpeed", 0);
    }

    public override void Update()
    {
        // Transition logic
        if (boss.PlayerInRange())
        {
            boss.ChangeState(new BossChaseState(boss));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

