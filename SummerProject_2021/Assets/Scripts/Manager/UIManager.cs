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
	[SerializeField] private ClockSystem clock;
	[SerializeField] private GameObject calendarObject;
	[SerializeField] private GameObject timeObject;

	private void PropInit()
    {
		if (clock == null)
		{
			clock = GameObject.Find("clock").GetComponent<ClockSystem>();
		}
		if (calendarObject == null)
		{
			calendarObject = GameObject.Find("Calendar");
		}
		if (timeObject == null)
		{
			timeObject = GameObject.Find("Time");
		}
	}

	private void Awake()
	{
		PropInit();
	}

	private void Update()
	{
		TimeController();
	}

	private void TimeController()
    {
		calendarObject.GetComponent<Text>().text = "Day " + clock.Day;
		timeObject.GetComponent<Text>().text = clock.Hour + ":" + clock.Min;
	}
}
