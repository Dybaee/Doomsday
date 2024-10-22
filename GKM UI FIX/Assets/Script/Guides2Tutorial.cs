using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guides2Tutorial : MonoBehaviour
{
    public GameObject guidesStatus;
    private Animator anim;
    public GameObject tutor;

    public bool itemUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatus();
    }

    public void ItemTake()
    {
        itemUsed = true;
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (itemUsed)
        {
            StartCoroutine(AnimationDelay());
        }
    }
    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetTrigger("Ceklist");

        yield return new WaitForSeconds(2.5f);
        tutor.gameObject.SetActive(false);
    }
}

