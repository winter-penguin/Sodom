/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-25
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWindowClass : MonoBehaviour, IManageWindow
{
    [SerializeField] protected GameObject targetWindow;
    [SerializeField] protected AudioSource buttonSpeaker;
    [SerializeField] protected static bool isWindowed;
    protected abstract void Init();
    
    public abstract void OpenSpecificWindow();

    public abstract void CloseSpecificWindow();

    public IEnumerator WaitUntillReady()
    {
        yield return new WaitUntil(() => !buttonSpeaker.isPlaying);
    }
}
