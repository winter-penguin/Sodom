using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPageButton : MonoBehaviour
{
    public GameObject BoxGridSetting1;
    public GameObject BoxGridSetting2;
    public Text Page;
    private void OnEnable()
    {
        LeftButton();
    }
    public void LeftButton()
    {
        if(BoxGridSetting2.active == true)
        {
            BoxGridSetting1.SetActive(true);
            BoxGridSetting2.SetActive(false);
            Page.text = "1 / 2";
        }
    }
    public void RightButton()
    {
        if (BoxGridSetting1.active == true)
        {
            BoxGridSetting2.SetActive(true);
            BoxGridSetting1.SetActive(false);
            Page.text = "2 / 2";
        }
    }
    public void CancelButton()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
