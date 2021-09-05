/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-09-05
///  Contact : kjhcorgi99@gmail.com
///  Content : Trader에 넣어 모든 ItemCell 과의 상호작용을 담당
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 정보 저장
/// </summary>
public struct itemStorage
{
	public int itemID;
	public string itemName;
	public int itemAmount;
	public int itemValue;

	public itemStorage(int _itemID, string _itemName, int _itemAmount, int _itemValue)
	{
		itemID = _itemID;
		itemName = _itemName;
		itemAmount = _itemAmount;
		itemValue = _itemValue;
	}
}

public class TradingSystem : MonoBehaviour
{
	public FarmingSystem traderFarming; // 거래 상인이 가져올 아이템들을 파밍

	private List<itemStorage> playerTable = new List<itemStorage>();
	private List<itemStorage> playerVault = new List<itemStorage>();
	private List<itemStorage> traderTable = new List<itemStorage>();
	private List<itemStorage> traderVault = new List<itemStorage>();

	[SerializeField] private GameObject traderContent;
	[SerializeField] private GameObject playerContent;
	

	private void Init()
	{
		// TODO : item 스크립트에서 playerVault에 넣을 아이템(소유하고 있는 아이템) 리스트를 가져와 저장할것
		// playerTable 과 traderTable 에 있는 버튼들 가져오기
	}

	/// <summary>
	/// 거래 상인이 가지고 있는 아이템을 설정합니다.
	/// </summary>
	private void TraderVaultSetting()
	{
		for (var i = 0; i < traderVault.Count; i++)
		{
			itemStorage tempItem = traderVault[i];
			GameObject button = Instantiate(Resources.Load("RItemCell", typeof(GameObject)), traderContent.transform, true) as GameObject;
			button.GetComponent<StoragedItemInfo>().ItemInfo = new itemStorage(tempItem.itemID, tempItem.itemName,
				tempItem.itemAmount, tempItem.itemValue);
		}
	}

	private void TraderTableSetting()
	{
		for (var i = 0; i < transform.Find("TraderTable").childCount; i++)
		{
			traderTable.Add(transform.GetChild(i).GetComponent<StoragedItemInfo>().ItemInfo);
		}
	}

	/// <summary>
	/// 플레이어가 가지고 있는 아이템을 설정합니다.
	/// </summary>
	private void PlayerVaultSetting()
	{
		for (var i = 0; i < playerVault.Count; i++)
		{
			itemStorage tempItem = playerVault[i];
			GameObject button = Instantiate(Resources.Load("RItemCell", typeof(GameObject)), playerContent.transform, true) as GameObject;
			button.GetComponent<StoragedItemInfo>().ItemInfo = new itemStorage(tempItem.itemID, tempItem.itemName,
				tempItem.itemAmount, tempItem.itemValue);
		}
	}

	private void PlayerTableSetting()
	{
		for (var i = 0; i < transform.Find("PlayerTable").childCount; i++)
		{
			playerTable.Add(transform.GetChild(i).GetComponent<StoragedItemInfo>().ItemInfo);
		}
	}

	/// <summary>
	/// 두 수의 가치를 비교합니다.
	/// </summary>
	/// <param name="traderValue"></param>
	/// <param name="playerValue"></param>
	/// <returns>거래 성공 여부</returns>
	private bool ValueWeighing(int traderValue, int playerValue)
	{
		bool result = false;


		return result;
	}

	/// <summary>
	/// 아이템을 거래 하기 위에 아이템을 Table로 옮깁니다.
	/// </summary>
	/// <returns>교환하고 남은 아이템 정보</returns>
	public itemStorage VaultToTable(itemStorage _itemStorage)
	{
		itemStorage vaultItem;
		itemStorage tableItem;
		
		for (var i = 0; i < playerTable.Count; i++)
		{
			if (playerTable[i].itemID == _itemStorage.itemID)
			{
				tableItem = playerTable[i];
				break;
			}
		}
		
		for (var i = 0; i < playerVault.Count; i++)
		{
			if (playerVault[i].itemID == _itemStorage.itemID)
			{
				vaultItem = playerVault[i];
				break;
			}
		}
		
		/*vaultItem.itemAmount -= 1;	// Vault에 있는 아이템 갯수 1개 감소*/
		return new itemStorage(0,"0",0,0);	// 임시로 작성해 놓은것 나중에 수정할 것
	}
}