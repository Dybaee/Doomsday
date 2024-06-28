using System;
using UnityEngine;

public class HpStats : MonoBehaviour
{
    Player2Controller pController;
    public float addHealth = 100f;



    // Event untuk memberi tahu perubahan HP

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
        if (other.CompareTag("Player"))
        {
            Player2Controller pController = other.GetComponent<Player2Controller>();
            if (pController != null)
            {
                pController.HP += addHealth; // Menambahkan HP ke player
                pController.HP = Mathf.Min(pController.HP, pController.maxHP);
                if (pController.healthSlider != null)
                {
                    pController.healthSlider.value = pController.HP; // Memperbarui nilai slider HP
                }
                Debug.Log("HP Collected! Current Health: " + pController.HP);
                Destroy(gameObject); // Menghapus objek yang memberi efek heal
            }
        }
    }
}
