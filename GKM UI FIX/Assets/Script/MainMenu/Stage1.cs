using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Video", LoadSceneMode.Single);
    }
}