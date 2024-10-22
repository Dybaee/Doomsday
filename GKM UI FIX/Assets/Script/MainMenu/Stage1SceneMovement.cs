using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class Stage1SceneMovement : MonoBehaviour
{
    private bool doorArea = false;
    public GameObject swordActive;
    public GameObject crownActive;
    public GameObject Reqtext;
    public GameObject GuidesText;

    private Guides3Tutorial guide;


    [SerializeField] private Animator myAnimationController;
    public GameObject loadingScreen;
    public float transitionTime = 1f;

    private void Start()
    {
        guide = FindAnyObjectByType<Guides3Tutorial>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorArea = true;
            guide.OpenTheDoor();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorArea = false;
            Reqtext.SetActive(false);
        }

    }

    void Update()
    {
        if (!doorArea || (swordActive.activeSelf && crownActive.activeSelf))
        {
            Reqtext.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && doorArea)
        {
            if (swordActive.activeSelf && crownActive.activeSelf)
            {
                GuidesText.SetActive(false);
                StartCoroutine(LoadLevel("CutsceneSand"));
            }
            else
            {
                Reqtext.SetActive(true); // Show the requirement message only if conditions are not met
            }
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
