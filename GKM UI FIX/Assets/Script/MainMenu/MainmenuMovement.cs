using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuMovement : MonoBehaviour
{
    
    private GameObject FTB;

    private void Start()
    {
        
        FTB = GameObject.FindGameObjectWithTag("ftb");
        FTB.SetActive(false);
    }
    // Start is called before the first frame update
    public void NextScene()
    {
        StartCoroutine(ChangeScene());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene()
    {
        FTB.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CutScene1");
    }
}
