using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public GameObject Opendoor;
    //public GameObject player;

    public float Range = 1f;

    public Transform player;

    public bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inRange = true;

            if (Opendoor != null)
            {
                Opendoor.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            inRange = false;

            if (Opendoor != null)
            {
                Opendoor.SetActive(false);
            }
        }
    }


}
