using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : BossState
{
    public BossDeadState(BossAII boss) : base(boss) { }

    public override void Enter()
    {
        Debug.Log("Entering Defeated State");
        // Defeated behavior
        boss.Defeated();
    }

    public override void Update() { }

    public override void Exit() { }
}