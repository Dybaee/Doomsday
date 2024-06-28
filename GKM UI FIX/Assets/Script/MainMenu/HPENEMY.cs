using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPENEMY : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        
    }
}
