using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpStats : MonoBehaviour
{
    Player2Controller pController;
    public float addHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        pController = GetComponent<Player2Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Player2Controller pController = other.GetComponent<Player2Controller>();

        if (other.CompareTag("Player"))  
        {
            pController.HP += addHealth;
            pController.HP = Mathf.Min(pController.HP, pController.maxHP); // Ensure HP doesn't exceed maxHP
            Debug.Log("HP Collected! Current Health: " + pController.HP);
            Destroy(gameObject);
        }
    }
}
