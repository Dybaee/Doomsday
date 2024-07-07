using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossQuest : MonoBehaviour
{
    public TextMeshProUGUI questStatusText;



    public bool isBossKilled = false;

    void Start()
    {
        UpdateQuestStatus();
    }

    public void OnBossKilled()
    {
        isBossKilled = true;
        UpdateQuestStatus();
    }

    void UpdateQuestStatus()
    {
        if (isBossKilled)
        {
            questStatusText.text = "1/1";
        }
        else
        {
            questStatusText.text = "0/1";
        }
    }
}
