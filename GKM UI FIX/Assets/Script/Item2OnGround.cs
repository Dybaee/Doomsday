using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2OnGround : MonoBehaviour
{
    public GameObject item;
    [SerializeField] Transform player;
    public GameObject interactionText;
    private GameObject shinyFX;
    private bool inArea = false;
    private bool isPlayerNearby = false;
    private Camera playerCamera;
    private Guides2Tutorial guide;
    public GameObject requiredObject;
    public PlayerNewCombat combatPlayer;
    public GameObject Reqtext;

    // Start is called before the first frame update
    void Start()
    {
        shinyFX = GameObject.FindGameObjectWithTag("ShinyFX1");
        guide = FindAnyObjectByType<Guides2Tutorial>();
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
                // Check if the requiredObject is active before allowing pickup
                if (requiredObject != null && requiredObject.activeSelf)
                {
                    ActivateItem();
                    if (interactionText != null)
                    {
                        Destroy(interactionText);
                        Debug.Log("Interaction Destroyed");
                    }
                }
                else
                {
                    Reqtext.SetActive(true);
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
            Reqtext.SetActive(false);
        }
    }

    private void ActivateItem()
    {
        Destroy(gameObject);
        shinyFX.SetActive(false);
        item.SetActive(true);

        combatPlayer.crownUsed.SetActive(true);
        guide.ItemTake();
        Debug.Log("ITEM COLLECTED");
    }
}
