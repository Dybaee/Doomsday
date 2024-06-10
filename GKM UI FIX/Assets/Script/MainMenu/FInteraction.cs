using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInteraction : MonoBehaviour
{
    public GameObject interactionText;  
    private bool isPlayerNearby = false;
    private Camera playerCamera;  

    void Start()
    {
        interactionText.SetActive(false);  
        playerCamera = Camera.main;  
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            interactionText.transform.LookAt(playerCamera.transform);  
            interactionText.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);  

            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
                Destroy(interactionText);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionText.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionText.SetActive(false); 
        }
    }

    private void Interact()
    {
        Debug.Log("ITEM COLLECTED");
    }
}
