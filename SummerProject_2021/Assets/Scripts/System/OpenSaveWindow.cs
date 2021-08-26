using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSaveWindow : AWindowClass
{
    protected override void Init()
    {
        targetWindow = FindObjectOfType<CloseSaveWindow>().gameObject;
    }

    public override void OpenSpecificWindow()
    {
        if (!isWindowed)
        {
            isWindowed = true;
            StartCoroutine(CoOpenSpecificWindow());            
        }
    }

    private IEnumerator CoOpenSpecificWindow()
    {
        yield return new WaitUntil(() => !buttonSpeaker.isPlaying);
        targetWindow.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void CloseSpecificWindow()
    { }
}
