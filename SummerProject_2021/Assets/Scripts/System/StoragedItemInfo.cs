/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-09-05
///  Contact : kjhcorgi99@gmail.com
///  Content : RItemCell 에 넣어 ItemCell 이 저장하고 있는
///  아이템 정보를 가져옴 
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoragedItemInfo : MonoBehaviour
{
	private enum CellType
	{
		Table,
		Vault
	}

	[SerializeField] private CellType cellType = 0;
	
	private itemStorage itemInfo; // 교환한 아이템 정보
	private TradingSystem tradingSystem;

	public itemStorage ItemInfo
	{
		get { return itemInfo; }
		set
		{
			itemInfo = value;
			SetItemName();
		}
	}

	[SerializeField] private Text itemTitle; // TEXT : 아이템 이름과 수량 

	private void Init()
	{
		itemTitle = transform.GetComponentInChildren<Text>();
		GameObject[] tempGameObject = GameObject.FindGameObjectsWithTag("NPC");
		for (var i = 0; i < tempGameObject.Length; i++)
		{
			if (tempGameObject[i].TryGetComponent(out TradingSystem _tradingSystem)) { tradingSystem = _tradingSystem; }
		}
	}

	private void Awake()
	{
		Init();
	}


	/// <summary>
	/// Vault 혹은 Table에 있는 아이템 이름 설정 
	/// </summary>
	private void SetItemName()
	{
		itemTitle.text = $"{itemInfo.itemName} X {itemInfo.itemAmount}"; // $ = String.Format
	}
	
	/// <summary>
	/// Vault 혹은 Table에 있는 아이템과 Table에 있는 아이템 갯수 변경
	/// </summary>
	private void ExchangeInfo()
	{
		if (cellType == CellType.Table)
		{
			// itemInfo = tradingSystem.VaultToTable(ref itemInfo)[0];
		}else if (cellType == CellType.Vault)
		{
			// itemInfo = tradingSystem.VaultToTable(ref itemInfo)[1];
		}
		
	}
}