using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    private GameObject shinyFX;
    public GameObject item;
    [SerializeField] Transform player;
    public bool isFPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        shinyFX = GameObject.FindGameObjectWithTag("ShinyFX");
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFPressed = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isFPressed && other.CompareTag("Player"))
        {
            Destroy(gameObject);
            shinyFX.SetActive(false);
            item.SetActive(true);
            CombatPlayer combatPlayer = other.GetComponent<CombatPlayer>();
            if (combatPlayer != null)
            {
                combatPlayer.UsingSword();
            }
        }
    }
}
