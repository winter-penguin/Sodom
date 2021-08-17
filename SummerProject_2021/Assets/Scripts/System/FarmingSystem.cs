/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-01
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FarmingSystem : MonoBehaviour
{
	/// <summary>
	/// 파밍 정보
	/// itemID : 아이템 아이디
	/// amount : 파밍양
	/// </summary>
	public struct FarmingInfo
	{
		public int itemID;
		public float amount;

		public FarmingInfo(int _itemID, float _farmAmount)
		{
			itemID = _itemID;
			amount = _farmAmount;
		}
	}
	
	private DBManagerItem itemDB;
	private CharacterValue charValue;
	private ItemDB[] itemList;
	private int[] foodIDArr;
	private int[] medIDArr;
	private int[] matIDArr;
	private List<FarmingInfo> farmingInfos = new List<FarmingInfo>();

	private float foodAmount = 0;
	private float medAmount = 0;
	private float matAmount = 0;
	private float farmingBagAmount { get; set; }
	private bool isSet;


	private IEnumerator Init()
	{
		isSet = false;
		GameObject[] tempObject = GameObject.FindGameObjectsWithTag("Manager");
		foreach (var t in tempObject)
		{
			if (t.TryGetComponent(out DBManagerItem itemComp))
			{
				yield return new WaitUntil(() => itemComp.DataLoading);
				itemDB = itemComp;
			}
		}

		charValue = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterValue>();
		farmingBagAmount = charValue.farming_amount;
		itemList = itemDB.itemDB;
		isSet = true;
	}

	private void Awake()
	{
		StartCoroutine(Init());
	}

	private IEnumerator Start()
	{
		yield return new WaitUntil(() => isSet);
		SetFarmingAmount();
		SetItemIDLists();
	}

	/// <summary>
	/// 모험을 수행하고 결과를 반환합니다.
	/// </summary>
	private void SetFarmingAmount()
	{
		float totalFarmingAmount = Random.Range(farmingBagAmount * 0.7f, farmingBagAmount);
		foodAmount = Random.Range(0, totalFarmingAmount * 0.1f);
		medAmount = Random.Range(0, totalFarmingAmount * 0.02f);
		matAmount = totalFarmingAmount - (foodAmount + medAmount);
	}

	private void SetItemIDLists()
	{
		List<int> foodIDList = new List<int>();
		List<int> medIDList = new List<int>();
		List<int> matIDList = new List<int>();

		for (var i = 0; i < itemList.Length; i++)
		{
			switch (itemList[i].ID)
			{
				case 0:
					foodIDList.Add(itemList[i].ID);
					break;
				case 3:
					matIDList.Add(itemList[i].ID);
					break;
				case 4:
					medIDList.Add(itemList[i].ID);
					break;
			}
		}

		foodIDArr = foodIDList.ToArray();
		medIDArr = medIDList.ToArray();
		matIDArr = matIDList.ToArray();
	}

	/// <summary>
	/// 음식을 파밍합니다.
	/// </summary>
	/// <param name="amount">총 파밍 양</param>
	private void FoodFarming(float amount)
	{
		
	}

	/// <summary>
	/// 재료를 파밍합니다.
	/// </summary>
	/// <param name="amount">총 재료 파밍 양</param>
	private List<FarmingInfo> MaterialFarming(float amount)
	{
		float capacity = amount;
		
		List<int> indexArr = new List<int>{0, 1, 2, 3};
		
		int mat1Index = indexArr[Random.Range(0, indexArr.Count-1)];
		indexArr.RemoveAt(mat1Index);
		int mat2Index = indexArr[Random.Range(0, indexArr.Count-1)];
		indexArr.RemoveAt(mat2Index);
		int mat3Index = indexArr[Random.Range(0, indexArr.Count-1)];

		float mat1Amount = Random.Range(capacity * 0.3f, capacity * 0.5f) / 5;
		capacity -= mat1Amount;
		float mat2Amount = Random.Range(capacity * 0.2f, capacity* 0.8f) / 5;
		capacity -= mat2Amount;
		float mat3Amount = Random.Range(0, capacity) / 5;
		
		farmingInfos.Add(new FarmingInfo(matIDArr[mat1Index], mat1Amount));
		farmingInfos.Add(new FarmingInfo(matIDArr[mat2Index], mat2Amount));
		farmingInfos.Add(new FarmingInfo(matIDArr[mat3Index], mat3Amount));

		return farmingInfos;
	}

	
	/// <summary>
	/// 파밍 정보를 가져옵니다.
	/// </summary>
	/// <returns>파밍 결과</returns>
	public List<FarmingInfo> GetFarmingInfo()
	{
		return farmingInfos;
	}
}