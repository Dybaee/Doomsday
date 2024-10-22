using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation2Guides : MonoBehaviour
{
    public Guides2Tutorial obj;  // Reference to the GuidesTutorial object

    public GameObject object1; // Reference to the first GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Any initialization can go here if needed
    }

    // Update is called once per frame
    void Update()
    {
        if (!obj.tutor.activeSelf)
        {
            RectTransform rectTransform = object1.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-96f, 15.77f); // Adjust to your desired position
        }
    }

}
