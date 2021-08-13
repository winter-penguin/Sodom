/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-07
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : MonoBehaviour
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

	public void OpenSettings()
	{
		settingWindow.SetActive(true);
	}

	public void CloseSettings()
	{
		settingWindow.SetActive(false);
	}
}