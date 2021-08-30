/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-29
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
	/// 파밍 완료한 정보
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

	/// <summary>
	/// DB에서 가져오는 아이템 정보
	/// </summary>
	private struct ImpliedItem
	{
		public int itemID;
		public int itemType;
		public int capacity;

		public ImpliedItem(int _itemID, int _itemType, int _capacity)
		{
			itemID = _itemID;
			itemType = _itemType;
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
		float totalFarmingAmount = Random.Range(farmingBagAmount * 0.8f, farmingBagAmount);
/*#if UNITY_EDITOR
		Debug.Log("예상 최종 파밍 양 : " + totalFarmingAmount);	
#endif*/
		foodAmount = Random.Range(0, totalFarmingAmount * 0.2f);
		medAmount = Random.Range(0, totalFarmingAmount * 0.1f);
		matAmount = totalFarmingAmount - (foodAmount + medAmount);
	}

	private void SetItemIDLists()
	{
		List<ImpliedItem> foodItemList = new List<ImpliedItem>();
		List<ImpliedItem> medItemList = new List<ImpliedItem>();
		List<ImpliedItem> matItemList = new List<ImpliedItem>();

		for (var i = 0; i < itemList.Length; i++)
		{
			switch (itemList[i].Item_Type)
			{
				case 1:
					foodItemList.Add(new ImpliedItem(itemList[i].ID, itemList[i].Item_Type, itemList[i].Charge_Space));
					break;
				case 0:
					matItemList.Add(new ImpliedItem(itemList[i].ID, itemList[i].Item_Type, itemList[i].Charge_Space));
					break;
				case 3:
					medItemList.Add(new ImpliedItem(itemList[i].ID, itemList[i].Item_Type, itemList[i].Charge_Space));
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


		int tempIndex = Random.Range(0, indexArr.Count - 1);
		int foodIndex = indexArr[tempIndex];
		ImpliedItem targetFood = foodItemArr[foodIndex];
		indexArr.RemoveAt(tempIndex);
		int lfoodAmount = (int)(Random.Range(capacity * 0.3f, capacity * 0.5f));
		if (lfoodAmount >= targetFood.capacity)
		{
			farmingInfos.Add(new FarmingInfo(targetFood.itemID, lfoodAmount / targetFood.capacity));
			capacity -= lfoodAmount / targetFood.capacity * targetFood.capacity;
		}

#if UNITY_EDITOR
		Debug.LogFormat("음식 1(ID : {0}) 파밍양 : {1} \n할당된 공간 : {2}",foodItemArr[foodIndex].itemID, lfoodAmount / targetFood.capacity, lfoodAmount);
#endif

		if (capacity < 5) { return; } // (예상 음식 파밍양 - 음식 1 파밍양) / 5 < 1 일 경우 파밍 종료


		tempIndex = Random.Range(0, indexArr.Count - 1);
		foodIndex = indexArr[tempIndex];
		targetFood = foodItemArr[foodIndex];
		indexArr.RemoveAt(tempIndex);
		lfoodAmount = (int)(Random.Range(capacity * 0.3f, capacity * 0.7f));
		if (lfoodAmount >= targetFood.capacity)
		{
			farmingInfos.Add(new FarmingInfo(targetFood.itemID, lfoodAmount / targetFood.capacity));
			capacity -= lfoodAmount / targetFood.capacity * targetFood.capacity;
		}

#if UNITY_EDITOR
		Debug.LogFormat("음식 2(ID : {0}) 파밍양 : {1} \n할당된 공간 : {2}",foodItemArr[foodIndex].itemID, lfoodAmount / targetFood.capacity, lfoodAmount);
#endif

		if (capacity < 5) { return; }


		tempIndex = Random.Range(0, indexArr.Count - 1);
		foodIndex = indexArr[tempIndex];
		targetFood = foodItemArr[foodIndex];
		indexArr.RemoveAt(tempIndex);
		lfoodAmount = (int)capacity;
		if (lfoodAmount >= targetFood.capacity)
		{
			farmingInfos.Add(new FarmingInfo(targetFood.itemID, lfoodAmount / targetFood.capacity));
		}

#if UNITY_EDITOR
		Debug.LogFormat("음식 3(ID:{0}) 파밍양 : {1} \n할당된 공간 : {2}",foodItemArr[foodIndex].itemID, lfoodAmount / targetFood.capacity, lfoodAmount);
#endif
	}

	/// <summary>
	/// 의약품을 파밍합니다.
	/// </summary>
	/// <param name="amount">총 의약품 파밍 양</param>
	private void MedFarming(float amount)
	{
		float capacity = amount;
		int pillAmount = Random.Range(0, 1);
		if (pillAmount > medItemArr[1].capacity)
		{
			farmingInfos.Add(new FarmingInfo(medItemArr[1].itemID,
				pillAmount / medItemArr[1].capacity)); // 알약은 medIDArr에서 [1]번 인덱스로 예상
			capacity -= pillAmount / medItemArr[1].capacity * medItemArr[1].capacity;
/*#if UNITY_EDITOR
			Debug.LogFormat("알약 파밍양 : {0} \n할당된 공간 : {1]", pillAmount / medItemArr[1].capacity, pillAmount);
#endif*/
		}


		if (capacity < 3) { return; }

		int bandageAmount = (int)Random.Range(0, capacity);
		if (capacity > matItemArr[0].capacity)
		{
			farmingInfos.Add(new FarmingInfo(medItemArr[0].itemID, bandageAmount / matItemArr[0].capacity));
/*#if UNITY_EDITOR
			Debug.LogFormat("붕대 파밍양 : {0} \n할당된 공간 : {1]", pillAmount / medItemArr[1].capacity, pillAmount);
#endif*/
		}
	}

	/// <summary>
	/// 재료를 파밍합니다.
	/// </summary>
	/// <param name="amount">총 재료 파밍 양</param>
	private void MaterialFarming(float amount)
	{
		float capacity = amount;
		int tempIndex;

		List<int> indexArr = new List<int> { 0, 1, 2, 3 };

		tempIndex = Random.Range(0, indexArr.Count - 1);
		int mat1Index = indexArr[tempIndex];
		ImpliedItem Mat1 = matItemArr[mat1Index];
		indexArr.RemoveAt(tempIndex);

		tempIndex = Random.Range(0, indexArr.Count - 1);
		int mat2Index = indexArr[tempIndex];
		ImpliedItem Mat2 = matItemArr[mat2Index];
		indexArr.RemoveAt(tempIndex);

		tempIndex = Random.Range(0, indexArr.Count - 1);
		int mat3Index = indexArr[tempIndex];
		ImpliedItem Mat3 = matItemArr[mat3Index];

		int mat1Amount = (int)Random.Range(capacity * 0.3f, capacity * 0.5f);
		if (mat1Amount >= Mat1.capacity)
		{
			farmingInfos.Add(new FarmingInfo(Mat1.itemID, mat1Amount / Mat1.capacity));
			capacity -= mat1Amount / Mat1.capacity * Mat1.capacity;
		}


		int mat2Amount = (int)Random.Range(capacity * 0.2f, capacity * 0.8f);
		if (mat2Amount >= Mat2.capacity)
		{
			farmingInfos.Add(new FarmingInfo(Mat2.itemID, mat2Amount / Mat2.capacity));
			capacity -= mat2Amount / Mat2.capacity * Mat1.capacity;
		}


		int mat3Amount = (int)Random.Range(0, capacity);
		if (mat3Amount >= Mat3.capacity) { farmingInfos.Add(new FarmingInfo(Mat3.itemID, mat3Amount / Mat3.capacity)); }
/*#if UNITY_EDITOR
		Debug.LogFormat(
			"재료(ID : {0}), 파밍양 : {1}, 할당된 공간 : {2}\n재료(ID : {3}) 파밍양 : {4}, 할당된 공간 : {5}\n재료(ID : {6}) 파밍양 : {7}, 할당된 공간 : {8}",
			Mat1.itemID, mat1Amount / Mat1.capacity, mat1Amount,
			Mat2.itemID, mat2Amount / Mat2.capacity, mat2Amount,
			Mat3.itemID, mat3Amount / Mat3.capacity, mat3Amount);
#endif*/
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

/*#if UNITY_EDITOR
		Debug.LogFormat("음식 총 파밍양 : {0}, 의약품 총 파밍양 : {1}, 재료 총 파밍양 : {2}", foodAmount, medAmount, matAmount);
#endif*/


		// 파밍 수행
		if (foodAmount >= 5) { FoodFarming(foodAmount); }

		if (medAmount >= 1) { MedFarming(medAmount); }

		MaterialFarming(matAmount);

		// 플레이어 수치 변경
		survivalGauge.HungerMinus(-20);
		survivalGauge.ThirstMinus(-25);
		survivalGauge.FatiguePlus(20);

#if UNITY_EDITOR
		for (var i = 0; i < farmingInfos.Count; i++)
		{
			Debug.LogFormat("아이템 ID : {0}, 아이템 파밍양 : {1}", farmingInfos[i].itemID, farmingInfos[i].amount);
		}
#endif
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