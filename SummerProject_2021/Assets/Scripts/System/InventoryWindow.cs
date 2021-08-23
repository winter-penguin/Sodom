/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour, IManageWindow
{
	[SerializeField] private GameObject inventoryWindow;
	private void Init()
	{
#if UNITY_EDITOR
		if (inventoryWindow == null)
		{
			Debug.LogWarningFormat("NULL Reference : 인벤토리 창을 {0}에 참조해주세요. ", name);
		}
#endif
	}

	private void Awake()
	{
		Init();
	}

	public void ControlWindow()
	{
		if (inventoryWindow.activeSelf) { CloseSpecificWindow(); }
		else { OpenSpecificWindow(); }
	}

	public void OpenSpecificWindow()
	{
		inventoryWindow.SetActive(true);
	}

	public void CloseSpecificWindow()
	{
		inventoryWindow.SetActive(false);
	}

	public IEnumerator WaitUntillReady()
	{
		yield return null;
	}
}