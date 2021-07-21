/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-21
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

	public static int Day
	{
		get { return _day; }
		set
		{
			_day = value;
			if (_day == 5)
			{
				eventHandler.EventAlert();
			}
		}
	}

	/// <summary>
	/// 시간 계산을 위한 시계 타입 구조체
	/// </summary>
	public struct Clock
	{
		private int _hour;
		private int _min;
		private int _sec;

		public int Hour
		{
			get { return _hour; }
			set
			{
				_hour = value;
				if (_hour >= 24)
				{
					Day++;
					_hour = 0;
				}

				if (Day == 30 && _hour == 18)
				{
					GameClear();
				}
			}
		}

		public int Min
		{
			get { return _min; }
			set
			{
				_min = value;

				while (_min >= 60)
				{
					Hour++;
					_min = _min - 60;
				}
			}
		}

		public int Sec
		{
			get { return _sec; }
			set
			{
				_sec = value;

				while (_sec >= 60)
				{
					Min++;
					_sec = _sec - 60;
				}
			}
		}

		public Clock(int _hour, int _min) : this()
		{
			Hour = _hour;
			Min = _min;
		}
	}

	// 플레이어 정보
	public bool isDead;

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

	private void EventRise()
	{
	}

	/// <summary>
	/// 게임 오버
	/// </summary>
	/// <returns>함수 처리 완료 여부</returns>
	public bool GameOver()
	{
		if (isDead)
		{
			// GameOver UI
			return true;
		}

		return false;
	}

	/// <summary>
	/// 시간 계산
	/// </summary>
	/// <returns></returns>
	public Clock DayTime = new Clock(0, 0);

	private IEnumerator TimeControl()
	{
		while (!isDead)
		{
			yield return new WaitForSeconds(0.417f); // 60초당 144분 추가 => 1초를 기다리고 
			DayTime.Min = DayTime.Min + 1; // Default : 1, Day Test : 720
			// DayTime.Sec = DayTime.Sec + 24;
		}
	}

	/// <summary>
	/// 게임 클리어
	/// </summary>
	/// <returns>함수 처리 완료 여부</returns>
	public static bool GameClear()
	{
		// 게임 클리어 
		return true;
	}
}