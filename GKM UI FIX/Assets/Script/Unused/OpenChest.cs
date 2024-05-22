using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    Animator anim;
    Item2OnGround item;
    public bool isFPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        item = GetComponent<Item2OnGround>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFPressed = true;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isFPressed && other.CompareTag("Player"))
        {
            anim.SetBool("OpenChest", true);
        }
    }
}
