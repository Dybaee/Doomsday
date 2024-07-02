using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    private GameObject shinyFX;
    public GameObject item;
    [SerializeField] Transform player;
    public bool isFPressed = false;
    private bool inArea = false;

    // Start is called before the first frame update
    void Start()
    {
        shinyFX = GameObject.FindGameObjectWithTag("ShinyFX");
    }

    // Update is called once per frame
    private void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.F))
        {
            ActivateItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
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
    }
}
