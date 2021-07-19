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
	private GameManager GM;
	private GameObject CalendarObject;
	private GameObject TimeObject;

	private void Awake()
	{
		GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		CalendarObject = GameObject.Find("Calendar");
		TimeObject = GameObject.Find("Time");
	}

	private void Update()
	{
		CalendarObject.GetComponent<Text>().text = "Day " + GameManager.Day;
		TimeObject.GetComponent<Text>().text = GM.DayTime.Hour + ":" + GM.DayTime.Min;
	}
}
