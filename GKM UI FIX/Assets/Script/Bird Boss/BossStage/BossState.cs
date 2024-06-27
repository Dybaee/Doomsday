using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected BossAII boss;

    public BossState(BossAII boss)
    {
        this.boss = boss;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

