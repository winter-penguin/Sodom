/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-25
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.SceneManagement;
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
	}
}