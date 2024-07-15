using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CST : MonoBehaviour
{
    public GameObject CSText;
    private bool hasPlayed = false;
    public PlayableDirector timeline;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            CSText.SetActive(true);
            timeline.Play(); // Memainkan timeline
            hasPlayed = true; // Menandai bahwa cutscene telah diputar
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasPlayed && other.CompareTag("Player"))
        {
            CSText.SetActive(false);
            timeline.Stop(); // Menghentikan timeline saat keluar dari collider
        }
    }
}
