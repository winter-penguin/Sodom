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
			WaitingCoroutine = StartCoroutine(WaitUntillReady(DoLast(false)));
			
		}
	}

	public void CloseSpecificWindow()
	{
		if (WaitingCoroutine == null)
		{
			mainWindow.SetActive(true);
			WaitingCoroutine = StartCoroutine(WaitUntillReady());
			
			gameObject.SetActive(false);
		}
	}

	private Action DoLast(bool _targetActive)
	{
		gameObject.SetActive(!_targetActive);
		mainWindow.SetActive(_targetActive);
	}
	
	public IEnumerator WaitUntillReady(Action func)
	{
		yield return new WaitUntil(() => !gameStartButton.isPlaying);
		WaitingCoroutine = null;
	}
}