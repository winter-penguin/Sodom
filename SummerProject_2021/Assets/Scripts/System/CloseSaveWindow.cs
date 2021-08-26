using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSaveWindow : AWindowClass
{
    protected override void Init()
    {
        targetWindow = FindObjectOfType<OpenSaveWindow>().gameObject;
    }

    public override void OpenSpecificWindow()
    { return; }

    public override void CloseSpecificWindow()
    {
        StartCoroutine(CoCloseSpecificWindow());
    }

    private IEnumerator CoCloseSpecificWindow()
    {
        yield return new WaitUntil(() => !buttonSpeaker.isPlaying);
        targetWindow.SetActive(true);
        isWindowed = false;
        gameObject.SetActive(false);
    }
}
