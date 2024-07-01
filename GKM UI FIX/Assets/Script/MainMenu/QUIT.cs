using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QUIT : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(CloseApplicationAfterDelay(3f));
    }
    IEnumerator CloseApplicationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Save current scene 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Unload current scene 
        SceneManager.UnloadSceneAsync(currentSceneIndex);

        // Load scene
        SceneManager.LoadScene(0, LoadSceneMode.Additive);

        // Set the main menu scene 
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
