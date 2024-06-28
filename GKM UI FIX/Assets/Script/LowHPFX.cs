using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LowHPFX : MonoBehaviour
{
    public float Intensity = 0f;
    public float WhenIsLowFXPlay = 0f;

    private Player2Controller playerController;

    private Vignette vignette;
    private VolumeProfile volume;
    private Animator animator;
    private Animator FTB;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2Controller>();
        volume = GetComponent<Volume>()?.profile;

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        FTB = GameObject.FindGameObjectWithTag("ftb").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Rendering.Universal.Vignette vignette;

        if (!volume.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        vignette.intensity.Override(Intensity);

        if (playerController.HP < WhenIsLowFXPlay)
        {
            if (animator.GetBool("isHealed") == true)
            {
                animator.SetBool("isHealed", false);
            }
            else
            {
                animator.Play("LowHealth");
            }           
        }
        else if (playerController.HP >= WhenIsLowFXPlay)
        {
            animator.SetBool("isHealed", true);
        }
    }
}
