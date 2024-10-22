using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guides3Tutorial : MonoBehaviour
{
    public GameObject guidesStatus;
    private Animator anim;

    public bool openDoor = false;

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

    public void OpenTheDoor()
    {
        openDoor = true;
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (openDoor)
        {
            StartCoroutine(AnimationDelay());
        }
    }
    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetTrigger("Ceklist");
    }
}
