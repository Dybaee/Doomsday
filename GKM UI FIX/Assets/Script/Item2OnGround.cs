using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2OnGround : MonoBehaviour
{
    public GameObject interactionText;
    public GameObject shinyFX;
    public GameObject item;
    public GameObject birdBoss;
    [SerializeField] private Transform player;

    private bool isPlayerNearby = false;
    private bool inArea = false;
    private Camera playerCamera;
    private Animator anim;
    private WeaponDamage weaponDamage;

    void Start()
    {
        interactionText.SetActive(false);
        playerCamera = Camera.main;
        anim = GetComponent<Animator>();
        weaponDamage = GetComponent<WeaponDamage>();
        shinyFX = GameObject.FindGameObjectWithTag("ShinyFX");
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            interactionText.transform.LookAt(playerCamera.transform);
            interactionText.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);

            if (Input.GetKeyDown(KeyCode.F) && birdBoss == null)
            {
                Interact();
                Destroy(interactionText);
            }
        }

        if (inArea && Input.GetKeyDown(KeyCode.F) && birdBoss == null)
        {
            ActivateItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            inArea = true;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            inArea = false;
            interactionText.SetActive(false);
        }
    }

    private void Interact()
    {
        Debug.Log("ITEM COLLECTED");
        ActivateItem();
    }

    private void ActivateItem()
    {
        shinyFX.SetActive(false);
        anim.SetBool("OpenChest", true);
        Destroy(gameObject, 5f);
        item.SetActive(true);
    }
}
