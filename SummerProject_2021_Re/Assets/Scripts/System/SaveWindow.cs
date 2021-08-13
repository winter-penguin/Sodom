/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-07
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWindow : MonoBehaviour
{
	[SerializeField] private GameObject mainWindow;

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

	public void OpenWindow()
	{
		mainWindow.SetActive(false);
		gameObject.SetActive(true);
	}

	public void CloseWindow()
	{
		mainWindow.SetActive(true);
		gameObject.SetActive(false);
	}
}