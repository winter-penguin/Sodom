using System.Collections;
using System.Collections.Generic;
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
	private static int _day;
	public static int Day
	{
		get { return _day; }
		set
		{
			_day = value;
			if (_day == 5)
			{
				eventHandler.CreateEvent();
			}
		}
	}

	/// <summary>
	/// 시간 계산을 위한 시계 타입 구조체
	/// </summary>
	private struct Clock
	{
		private static int _hour;
		private static int _min;

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
				if (_min >= 60)
				{
					Hour++;
					_min = 0;
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
	private IEnumerator TimeControl()
	{
		Clock DayTime = new Clock(0, 0);
		while (!isDead)
		{
#if UNITY_EDITOR
			Debug.Log("Day : " +Day + "\nTime : "+DayTime.Hour+ " : " + DayTime.Min );
#endif
			yield return new WaitForSeconds(1.0f); // 60초를 기다린 후 1분 추가
			Day++;

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