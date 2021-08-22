/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-23
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using UnityEngine;

public class QuitGameProccess : MonoBehaviour
{
	[SerializeField] private AudioSource exitButton;
	private Coroutine waitCoroutine;
	
    public void QuitGaming()
    {
	    if (waitCoroutine == null) waitCoroutine = StartCoroutine(WaitUntillReady());
    }

    private IEnumerator WaitUntillReady()
    {
	    yield return new WaitUntil(() => !exitButton.isPlaying);
#if UNITY_EDITOR
	    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
