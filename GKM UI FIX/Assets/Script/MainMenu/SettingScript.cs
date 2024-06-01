using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScript : MonoBehaviour
{
    public GameObject _control;
    public GameObject _closeSetting;

    public void openControl()
    {
        _control.SetActive(true);
    }

    public void closeControl()
   {
        _control.SetActive(false);
   }

    public void closeSetting()
   {
       _closeSetting.SetActive(false);
    }
}
