using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2OnGround : MonoBehaviour
{
    public GameObject item;
    [SerializeField] Transform player;
    public bool isFPressed = false;
    Animator anim;

    WeaponDamage weaponDamage;

    // Start is called before the first frame update
    void Start()
    {
        weaponDamage = GetComponent<WeaponDamage>();
        anim = GetComponent<Animator>();
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
            Destroy(gameObject, 5f);
            item.SetActive(true);
        }
    }
}
