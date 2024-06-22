using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXSlash : MonoBehaviour
{
    [SerializeField] private VisualEffect Slash1;
    [SerializeField] private VisualEffect Slash2;
    [SerializeField] private VisualEffect Slash3;
    [SerializeField] private VisualEffect StompBoss;
    [SerializeField] private VisualEffect DeadBoss;
    public void Attack1()
    {
        if (Slash1 == null) { return; }
        Slash1.Play();
    }

    public void Attack2()
    {
        if (Slash2 != null)
        {
            Slash2.Play();
        }
    }

    public void Attack3()
    {
        if (Slash3 != null)
        {
            Slash3.Play();
        }
        return;
    }

    public void StompAttack()
    {
        if (StompBoss != null)
        {
            StompBoss.Play();
        }
        return;
    }

    public void DeathBoss()
    {
        if (DeadBoss != null)
        {
            DeadBoss.Play();
        }
        return;
    }
}
