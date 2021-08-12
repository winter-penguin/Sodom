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
	[SerializeField] private ClockSystem clock;
	[SerializeField] private GameObject calendarObject;
	[SerializeField] private GameObject timeObject;

	private void Awake()
	{	
		if(clock == null) clock = GameObject.Find("Clock").GetComponent<ClockSystem>();
		calendarObject = GameObject.Find("Calendar");
		timeObject = GameObject.Find("Time");
	}

	private void Update()
	{
		calendarObject.GetComponent<TextMeshProUGUI>().text = "Day " + clock.Day;
		timeObject.GetComponent<TextMeshProUGUI>().text = clock.Hour + ":" + clock.Min;
		
	}
}
