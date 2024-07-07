using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneIndex;
    [SerializeField] private BossQuest bossQuest;

    private int buildIndex;

    private void Start()
    {
        
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        buildIndex = Int32.Parse(sceneIndex);
        if (bossQuest != null)
        {
            if (bossQuest.isBossKilled)
            {
                StartCoroutine(DelayScene());
            }
        }
    }

    IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
