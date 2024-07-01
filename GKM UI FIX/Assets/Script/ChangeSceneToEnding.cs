using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToEnding : MonoBehaviour
{
    [SerializeField] private GameObject boss; 
    [SerializeField] private float delay = 5f;
    [SerializeField] private string sceneName = "NextScene";

    private void Start()
    {
        if (boss == null)
        {
            Debug.LogError("Not Assigned");
            return;
        }
    }

    private void Update()
    {
        if (boss == null)
        {
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
