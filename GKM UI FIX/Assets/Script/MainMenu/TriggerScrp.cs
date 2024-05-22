using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerScrp : MonoBehaviour
{
    public GameObject pic;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pic.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pic.SetActive(false);
        }
    }
}
