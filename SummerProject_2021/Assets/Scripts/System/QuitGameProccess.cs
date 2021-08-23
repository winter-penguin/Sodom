/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-23
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using UnityEngine;

public class QuitGameProccess : MonoBehaviour
{
    public void QuitGaming()
    {
#if UNITY_EDITOR
	    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
