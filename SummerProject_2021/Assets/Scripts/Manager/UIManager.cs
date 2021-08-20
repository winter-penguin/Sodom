/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-09
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private ClockSystem clock;
	private GameObject calendarObject;
	private GameObject timeObject;

	private void Awake()
	{
		clock = FindObjectOfType<ClockSystem>();
		calendarObject = clock.transform.GetChild(0).gameObject;
		timeObject = clock.transform.GetChild(1).gameObject;
	}

	private void Update()
	{
		calendarObject.GetComponent<TextMeshProUGUI>().text = String.Format("Day {0:00}", clock.Day);
		timeObject.GetComponent<TextMeshProUGUI>().text = String.Format("{0:00}:{1:00}",clock.Hour ,clock.Min);
	}
}