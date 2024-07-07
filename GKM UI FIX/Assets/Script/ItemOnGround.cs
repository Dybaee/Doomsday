using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    public GameObject item;
    [SerializeField] Transform player;
    public GameObject interactionText;
    private GameObject shinyFX;
    private bool inArea = false;
    private bool isPlayerNearby = false;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        shinyFX = GameObject.FindGameObjectWithTag("ShinyFX");
        interactionText.SetActive(false);
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby)
        {
            interactionText.transform.LookAt(playerCamera.transform);
            interactionText.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);

            if (inArea && Input.GetKeyDown(KeyCode.F))
            {
                ActivateItem();
                if (interactionText != null)
                {
                    Destroy(interactionText);
                    Debug.Log("Interaction Destroyed");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            isPlayerNearby = true;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
            isPlayerNearby = false;
            interactionText.SetActive(false);
        }
    }

    private void ActivateItem()
    {
        Destroy(gameObject);
        shinyFX.SetActive(false);
        item.SetActive(true);
        CombatPlayer combatPlayer = FindObjectOfType<CombatPlayer>();
        if (combatPlayer != null)
        {
            combatPlayer.UsingSword();
        }
        Debug.Log("ITEM COLLECTED");
    }
}
