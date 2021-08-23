/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using UnityEngine;

public class SaveWindow : MonoBehaviour, IManageWindow
{
	[SerializeField] private GameObject mainWindow;
	public AudioSource gameStartButton;
	private Coroutine WaitingCoroutine;

	private void Init()
	{
		if (mainWindow == null)
		{
			mainWindow = GameObject.Find("MainWindow").gameObject;
		}
	}

	private void Awake()
	{
		Init();
	}

	public void OpenSpecificWindow()
	{
		if (WaitingCoroutine == null)
		{
			gameObject.SetActive(true);
			WaitingCoroutine = StartCoroutine(temp(mainWindow));
		}
	}

	public void CloseSpecificWindow()
	{
		if (WaitingCoroutine == null)
		{
			mainWindow.SetActive(true);
			WaitingCoroutine = StartCoroutine(temp(gameObject));
		}
	}

	public IEnumerator WaitUntillReady()
	{
		yield return new WaitUntil(() => !gameStartButton.isPlaying);
	}

	private IEnumerator temp(GameObject tempObject)
	{
		yield return new WaitUntil(() => !gameStartButton.isPlaying);
		tempObject.SetActive(false);
		WaitingCoroutine = null;
	}
}