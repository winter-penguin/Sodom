/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-12
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadScene(sceneIndex));
	}

	private IEnumerator LoadScene(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress/0.9f);
			yield return null;
		}
	}
}
