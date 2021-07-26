/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-25
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;
public class EventHandler : MonoBehaviour
{
	[SerializeField] private const int NPC_COUNT = 3;	// NPC의 총 개수
	[SerializeField] private GameObject[] npcGroup;
	private GameManager GM;
	
	private GameObject spawn;
	private GameObject trader;
	private GameObject criminal;
	private GameObject chaebol;
	private NPC_Moving traderFunc;
	private NPC_Moving criminalFunc;
	private NPC_Moving chaebolFunc;

	private bool doEvent = false;	// 이벤트가 현재 진행중인가?
	private void Init()
	{
		spawn = GameObject.Find("SpawnPoint");	// NPC가 생성될 위치
		GM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
		
		if (npcGroup.Length != NPC_COUNT)	// trader 객체가 배정되어있지 않을 경우
		{
			GameObject[] temp = GameObject.FindGameObjectsWithTag("NPC");	// NPC 태그를 가진 게임오브젝트를 모두 찾기
			for (var i = 0; i < temp.Length; i++)  
			{
				switch (temp[i].name)
				{
					case "Trader" :
						trader = temp[i];
						traderFunc = trader.GetComponent<NPC_Moving>();
						trader.SetActive(false);
						break;
					case "Criminal":
						criminal = temp[i];
						criminalFunc = criminal.GetComponent<NPC_Moving>();
						criminal.SetActive(false);
						break;
					case "Chaebol":
						chaebol = temp[i];
						chaebolFunc = chaebol.GetComponent<NPC_Moving>();
						chaebol.SetActive(false);
						break;
					default:
						Debug.Log("It is not a proper NPC");
						break;
				}
			}
		}
	}
	
	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		StartCoroutine(Handler());
	}
	
	/// <summary>
	/// 이벤트 발동 조건 및 발동 상황을 관리합니다.
	/// </summary>
	/// <returns></returns>
	private IEnumerator Handler()
	{
		while (!GM.isEnd)
		{
			if (GM.Day == 1 && !doEvent)
			{
				doEvent = true;
				TraderEvent();
				yield return new WaitUntil(() => traderFunc.activeOver);
				doEvent = false;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	/// <summary>
	/// 거래 상인 이벤트가 발생합니다.
	/// </summary>
	public void TraderEvent()
	{
		trader.transform.position = spawn.transform.position;
		trader.SetActive(true);
		traderFunc.Operate(GameObject.FindGameObjectWithTag("Player").transform.position, 2f);
	}
}