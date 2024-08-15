using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS4 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
