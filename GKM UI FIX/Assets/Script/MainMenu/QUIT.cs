using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QUIT : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(CloseApplicationAfterDelay(5f));
    }
    IEnumerator CloseApplicationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
