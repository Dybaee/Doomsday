using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXSlash : MonoBehaviour
{
    [SerializeField] private VisualEffect Slash1;
    [SerializeField] private VisualEffect Slash2;
    [SerializeField] private VisualEffect Slash3;
    public void Attack1()
    {
        Slash1?.Play();
    }

    public void Attack2()
    {
        Slash2?.Play();
    }

    public void Attack3()
    {
        Slash3?.Play();
    }
}
