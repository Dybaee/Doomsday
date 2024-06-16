using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpStats : MonoBehaviour
{
    PlayerController pController;
    public float addHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        pController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController pController = other.GetComponent<PlayerController>(); 

        if (pController != null)
        {
            Debug.Log("HP Collected!");
            Destroy(gameObject);
            pController.HP += addHealth;
            pController.HP = Mathf.Min(pController.HP, pController.maxHP); // Ensure HP doesnt exceed maxHP
            Debug.Log("Current Health: " + pController.HP);
        }
    }
}
