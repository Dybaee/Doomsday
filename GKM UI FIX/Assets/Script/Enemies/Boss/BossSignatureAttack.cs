using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BossSignatureAttack : BossState
{
    public BossSignatureAttack(BossAII boss) : base(boss) { }
    

    public override void Enter()
    {
        boss.anim.SetTrigger("SignatureAttack");
    }

    public override void Exit()
    {
        boss.isUlti = false;
        boss.timer = boss.Attacktimer;
    }

    public override void Update()
    {
        if (boss.PlayerInAttackRange())
        {
            boss.ChangeState(new BossAttackState(boss));
        }

        if (!boss.PlayerInAttackRange())
        {
            boss.ChangeState(new BossChaseState(boss));
        }
    }
}
