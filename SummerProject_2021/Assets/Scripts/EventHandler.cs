/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-09
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
	private GameObject Player;
	private GameObject exclamationMark;

	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		exclamationMark = GameObject.Find("EventOccur");
	}

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		exclamationMark.GetComponent<Image>().enabled = false;
	}

	public void EventAlert()
	{
#if UNITY_EDITOR
		Debug.Log("Event Occured");
#endif
		Vector3 originPos = Player.transform.localPosition;
		Vector2 playerSize = Player.GetComponent<BoxCollider2D>().size;
		// 느낌표가 플레이어 머리 위에 나타나도록 계산
		// 느낌표의 위치 = vector2( 플레이어의 x 좌표 + 플레이어의 2D 콜라이더의 너비/2, 플레이어의 Y 좌표 + 플레이어의 2D 콜라이더의 높이)
		exclamationMark.transform.localPosition = new Vector2(originPos.x + playerSize.x/2, originPos.y + playerSize.y); 
		exclamationMark.GetComponent<Image>().enabled = true;
	}
}