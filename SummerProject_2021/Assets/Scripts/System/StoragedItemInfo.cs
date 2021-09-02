using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoragedItemInfo : MonoBehaviour
{
	private itemStorage itemInfo;
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

	private void SetItemName()
	{
		itemTitle.text = $"{itemInfo.itemName} X {itemInfo.itemAmount}"; // $ = String.Format
	}


	private void ExchangeInfo()
	{
		itemInfo = tradingSystem.VaultToTable(itemInfo);
	}
}