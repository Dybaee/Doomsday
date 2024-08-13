using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMovement : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;
    private bool doorArea = false;
    

    public GameObject loadingScreen;
    public GameObject miniboss;

    public float transitionTime = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && doorArea && miniboss == null)
        {
            LoadNextLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorArea = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorArea = false;
        }

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        loadingScreen.SetActive(true);
        myAnimationController.SetBool("Start", true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}

