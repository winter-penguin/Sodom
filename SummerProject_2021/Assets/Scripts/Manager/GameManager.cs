/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-25
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	/// <summary>
	/// 싱글톤 패턴
	/// </summary>
	/// 게임 매니저 단일 인스턴스 유지를 위한 싱글톤 패턴 생성
	private static GameManager _instance = null;

	public static GameManager Instance
	{
		get { return _instance; }
	}

	// 이벤트 핸들러
	private static EventHandler eventHandler;

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
				GameClear();
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

	// 게임이 끝났는가?
	public bool isEnd = false;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad(gameObject);

		eventHandler = GetComponent<EventHandler>();
	}

	private void Start()
	{
		StartCoroutine(TimeControl());
	}

	/// <summary>
	/// 시간 계산
	/// </summary>
	/// <returns></returns>
	public Clock DayTime = new Clock(0, 0);

	private IEnumerator TimeControl()
	{
		while (!isEnd)
		{
			yield return new WaitForSeconds(0.417f); // 60초당 144분 추가 => 1초를 기다리고 
			Min = Min + 1; // Default : 1, Day Test : 720
			// DayTime.Sec = DayTime.Sec + 24;
		}
	}

	/// <summary>
	/// 게임 클리어
	/// </summary>
	/// <returns>함수 처리 완료 여부</returns>
	public bool GameClear()
	{
		// 게임 클리어 
		return true;
	}
}