/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-24
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonFunction : MonoBehaviour
{
    private QuitGameProccess terminator;
    private AudioSource buttonSpeaker;

    private void Init()
    {
        terminator = FindObjectOfType<QuitGameProccess>();
        buttonSpeaker = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        Init();
    }

    public void ExitGame()
    {
        StartCoroutine(CoExitGaming());
    }

    private IEnumerator CoExitGaming()
    {
        yield return new WaitUntil(() => !buttonSpeaker.isPlaying);
        
        terminator.QuitGaming();
    }
}
