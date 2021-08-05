/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-05
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
	#region Clock
// 현재 플레이 하고 있는 날짜
	private static int _day = 1;

	public int Day
	{
		get { return _day; }
		set
		{
			_day = value;
		}
	}

	/// <summary>
	/// 시간 계산을 위한 시계 타입 구조체
	/// </summary>
	public struct Clock
	{
		public int hour;
		public int min;
		public int sec;

		public Clock(int _hour, int _min) : this()
		{
			hour = _hour;
			min = _min;
		}
	}

	public int Hour
	{
		get { return DayTime.hour; }
		set
		{
			DayTime.hour = value;
			if (DayTime.hour >= 24)
			{
				Day++;
				DayTime.hour = 0;
			}

			if (Day == 30 && DayTime.hour == 18)
			{
				//TODO: 게임 클리어
			}
		}
	}

	public int Min
	{
		get { return DayTime.min; }
		set
		{
			DayTime.min = value;

			while (DayTime.min >= 60)
			{
				Hour++;
				DayTime.min = DayTime.min - 60;
			}
		}
	}

	public int Sec
	{
		get { return DayTime.sec; }
		set
		{
			DayTime.sec = value;

			while (DayTime.sec >= 60)
			{
				Min++;
				DayTime.sec = DayTime.sec - 60;
			}
		}
	}
	#endregion

	#region Variables
	private GameManager GM;

	#endregion

	private void Init()
	{
		if (GM == null) { GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); }
		
	}
	public Clock DayTime = new Clock(0, 0);
    
    	private IEnumerator TimeControl()
    	{
    		while (!GM.isEnd)
    		{
    			yield return new WaitForSeconds(0.417f); // 60초당 144분 추가 => 1초를 기다리고 
    			Min = Min + 1; // Default : 1, Day Test : 720
    			// DayTime.Sec = DayTime.Sec + 24;
    		}
    	}
}
