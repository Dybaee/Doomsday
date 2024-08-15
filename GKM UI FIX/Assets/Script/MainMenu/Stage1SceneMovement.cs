using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class Stage1SceneMovement : MonoBehaviour
{
    private bool doorArea = false;
    public GameObject swordActive;

    [SerializeField] private Animator myAnimationController;
    public GameObject loadingScreen;
    public float transitionTime = 1f;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && doorArea && swordActive.activeSelf)
        {
            StartCoroutine(LoadLevel("CS2"));
        }
    }

    IEnumerator LoadLevel(string CutsceneName)
    {
        loadingScreen.SetActive(true);
        myAnimationController.SetBool("Start", true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(CutsceneName);
    }
}
