using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainScene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Terrain", LoadSceneMode.Single);
    }
}
