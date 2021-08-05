/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-09
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private ClockSystem clock;
	private GameObject CalendarObject;
	private GameObject TimeObject;

	private void Awake()
	{
		clock = GameObject.Find("Clock").GetComponent<ClockSystem>();
		CalendarObject = GameObject.Find("Calendar");
		TimeObject = GameObject.Find("Time");
	}

	private void Update()
	{
		CalendarObject.GetComponent<Text>().text = "Day " + clock.Day;
		TimeObject.GetComponent<Text>().text = clock.Hour + ":" + clock.Min;
	}
}
