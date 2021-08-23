/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : MonoBehaviour, IManageWindow
{
	[SerializeField] private GameObject settingWindow;
	[SerializeField] private GameObject closeSetting;
	[SerializeField] private Slider bgmScaler;
	[SerializeField] private Slider sfxScaler;
	private GameObject settingGroup;

	private void Init()
	{
		settingGroup = transform.Find("Setting").gameObject;

		if (settingWindow == null)
		{
			settingWindow = gameObject;
		}

		if (closeSetting == null)
		{
			closeSetting = settingGroup.transform.Find("Exit").gameObject;
		}

		if (bgmScaler == null)
		{
			bgmScaler = settingGroup.transform.Find("BGM").gameObject.transform.Find("SoundScaler").gameObject
				.GetComponent<Slider>();
		}

		if (sfxScaler == null)
		{
			sfxScaler = settingGroup.transform.Find("SFX").gameObject.transform.Find("SoundScaler").gameObject
				.GetComponent<Slider>();
		}
	}

	private void Awake()
	{
		Init();
	}
	
	public void OpenSpecificWindow()
	{
		settingWindow.SetActive(true);
	}

	public void CloseSpecificWindow()
	{
		settingWindow.SetActive(false);
	}

	public IEnumerator WaitUntillReady()
	{
		yield return null;
	}
}