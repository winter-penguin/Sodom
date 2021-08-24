/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-24
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
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
		public int amount;

		public FarmingInfo(int _itemID, int _farmAmount)
		{
			itemID = _itemID;
			amount = _farmAmount;
		}
	}

	private struct ImpliedItem
	{
		public int itemID;
		public int capacity;

		public ImpliedItem(int _itemID, int _capacity)
		{
			itemID = _itemID;
			capacity = _capacity;
		}
	}

	private DBManagerItem itemDB;
	private CharacterValue charValue;
	private SurvivalGauge survivalGauge;
	private ItemDB[] itemList;
	private ImpliedItem[] foodItemArr;
	private ImpliedItem[] medItemArr;
	private ImpliedItem[] matItemArr;
	private List<FarmingInfo> farmingInfos = new List<FarmingInfo>();

	private float foodAmount = 0;
	private float medAmount = 0;
	private float matAmount = 0;
	private int farmingBagAmount { get; set; }
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
		survivalGauge = FindObjectOfType<SurvivalGauge>();
		farmingBagAmount = (int)charValue.farming_amount;
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
		List<ImpliedItem> foodItemList = new List<ImpliedItem>();
		List<ImpliedItem> medItemList = new List<ImpliedItem>();
		List<ImpliedItem> matItemList = new List<ImpliedItem>();

		for (var i = 0; i < itemList.Length; i++)
		{
			switch ((int)itemList[i].Item_Type)
			{
				case 0:
					foodItemList.Add(new ImpliedItem((int)itemList[i].Item_Type, (int)itemList[i].Charge_Space));
					break;
				case 3:
					matItemList.Add(new ImpliedItem((int)itemList[i].Item_Type, (int)itemList[i].Charge_Space));
					break;
				case 4:
					medItemList.Add(new ImpliedItem((int)itemList[i].Item_Type, (int)itemList[i].Charge_Space));
					break;
			}
		}

		foodItemArr = foodItemList.ToArray();
		medItemArr = medItemList.ToArray();
		matItemArr = matItemList.ToArray();
	}

	/// <summary>
	/// 음식을 파밍합니다.
	/// </summary>
	/// <param name="amount">총 음식 파밍 양</param>
	private void FoodFarming(float amount)
	{
		float capacity = amount;

		List<int> indexArr = new List<int>(); // 파밍할 음식 종류를 결정하기 위한 index 리스트
		for (var i = 0; i < foodItemArr.Length; i++) { indexArr.Add(i); } // DB에 있는 음식 아이템 갯수만큼 index 생성

		int foodIndex = indexArr[Random.Range(0, indexArr.Count - 1)];
		ImpliedItem targetFood = foodItemArr[foodIndex];
		indexArr.RemoveAt(foodIndex);
		int lfoodAmount = (int)(Random.Range(capacity * 0.3f, capacity * 0.5f) / 5);
		farmingInfos.Add(new FarmingInfo(targetFood.itemID, lfoodAmount));

		capacity -= lfoodAmount*targetFood.capacity;

		if (capacity < 5) { return; } // (예상 음식 파밍양 - 음식 1 파밍양) / 5 < 1 일 경우 파밍 종료

		foodIndex = indexArr[Random.Range(0, indexArr.Count - 1)];
		targetFood = foodItemArr[foodIndex];
		indexArr.RemoveAt(foodIndex);
		lfoodAmount = (int)(Random.Range(capacity * 0.3f, capacity * 0.7f) / 5);
		farmingInfos.Add(new FarmingInfo(targetFood.itemID, lfoodAmount));
		capacity -= lfoodAmount*targetFood.capacity;

		if (capacity < 5) { return ; }

		foodIndex = indexArr[Random.Range(0, indexArr.Count - 1)];
		targetFood = foodItemArr[foodIndex];
		indexArr.RemoveAt(foodIndex);
		lfoodAmount = (int)capacity;
		farmingInfos.Add(new FarmingInfo(targetFood.itemID, lfoodAmount ));
	}

	/// <summary>
	/// 의약품을 파밍합니다.
	/// </summary>
	/// <param name="amount">총 의약품 파밍 양</param>
	private void MedFarming(float amount)
	{
		int pillAmount = Random.Range(0, 1);
		farmingInfos.Add(new FarmingInfo(medItemArr[1].itemID, pillAmount)); // 알약은 medIDArr에서 [1]번 인덱스로 예상
		float capacity = (amount - pillAmount*medItemArr[1].capacity);
		if (capacity < 3) { return; }

		int bandageAmount = (int)Random.Range(0, capacity);
		farmingInfos.Add(new FarmingInfo(medItemArr[0].itemID, bandageAmount));
	}

	/// <summary>
	/// 재료를 파밍합니다.
	/// </summary>
	/// <param name="amount">총 재료 파밍 양</param>
	private void MaterialFarming(float amount)
	{
		float capacity = amount;

		List<int> indexArr = new List<int> { 0, 1, 2, 3 };
		
		int mat1Index = indexArr[Random.Range(0, indexArr.Count - 1)];
		ImpliedItem Mat1 = matItemArr[mat1Index];
		indexArr.RemoveAt(mat1Index);
		int mat2Index = indexArr[Random.Range(0, indexArr.Count - 1)];
		ImpliedItem Mat2 = matItemArr[mat2Index];
		indexArr.RemoveAt(mat2Index);
		int mat3Index = indexArr[Random.Range(0, indexArr.Count - 1)];
		ImpliedItem Mat3 = matItemArr[mat3Index];

		int mat1Amount = (int)(Random.Range(capacity * 0.3f, capacity * 0.5f) / 5);
		capacity -= mat1Amount * Mat1.capacity;
		int mat2Amount = (int)(Random.Range(capacity * 0.2f, capacity * 0.8f) / 5);
		capacity -= mat2Amount* Mat2.capacity;
		int mat3Amount = (int)(Random.Range(0, capacity) / 5);

		farmingInfos.Add(new FarmingInfo(Mat1.itemID, mat1Amount));
		farmingInfos.Add(new FarmingInfo(Mat2.itemID, mat2Amount));
		farmingInfos.Add(new FarmingInfo(Mat3.itemID, mat3Amount));
	}

	/// <summary>
	/// 파밍한 내역을 초기화합니다.
	/// </summary>
	private void ResetFarmingInfo()
	{
		farmingInfos.Clear();
	}

	/// <summary>
	///	파밍을 시작합니다. 
	/// </summary>
	public void GoFarming()
	{
		// 파밍 관련 정보 세팅
		ResetFarmingInfo();
		SetFarmingAmount();
		
		// 파밍 수행
		if (foodAmount / 5 >= 1) { FoodFarming(foodAmount); }
		if (medAmount > 1) { MedFarming(medAmount); }
		MaterialFarming(matAmount);
		
		// 플레이어 수치 변경
		survivalGauge.HungerMinus(-20);
		survivalGauge.ThirstMinus(-25);
		survivalGauge.FatiguePlus(20);
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