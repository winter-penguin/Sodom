using System.Collections;
using System.Collections.Generic;
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
