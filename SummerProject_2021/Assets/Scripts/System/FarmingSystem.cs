/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-20
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingSystem : MonoBehaviour
{
	#region Variables

	private GameObject dbInstance; // 캐릭터 DB 가져오기
	private float charBagAmount; // 캐릭터 하루 파밍 양
	private float farmingAmount; // 최종 파밍 양
	private float foodAmount; // 음식 파밍 양
	private float medAmount; // 의약품 파밍 양
	private float matAmount; // 재료 파밍 양

	#endregion

	/// <summary>
	/// [cloth, wood, rock, steel]
	/// </summary>
	private enum Materials
	{
		cloth,
		wood,
		rock,
		steel
	};

	struct MaterialPack
	{
		public Materials mat;
		public float amount;

		public MaterialPack(Materials _mat, float _amount)
		{
			mat = _mat;
			amount = _amount;
		}
	}

	private MaterialPack[] matPack; // 재료는 총 4개 수집

	private void Start()
	{
		Init();
	}

	public void Farming()
	{
		float totalAmount, foodAmount, medAmount;
		MaterialPack[] tempPack;
		totalAmount = CalculateFarmingAmout(40); // 나중에 수를 charBagAmount로 변경할것
		foodAmount = CalculateFoodAmount(totalAmount);
		medAmount = CalculateMedAmount(totalAmount);
		tempPack = CalculateMatAmount(totalAmount, foodAmount, medAmount);

#if UNITY_EDITOR
		Debug.Log("totalAmount : " + totalAmount);
		Debug.Log("foodAmount : " + foodAmount);
		Debug.Log("medAmount : " + medAmount);
		for (var i = 0; i < matPack.Length-1; i++)
		{
			Debug.Log("재료"+i+" : " + matPack[i].mat + " 파밍양"+i+" : " + matPack[i].amount);
		}
#endif
	}

	private void Init()
	{
		dbInstance = GameObject.Find("" /*DB 정보를 가져오는 게임 오브젝트*/);
		//TODO: dbInstance로부터 캐릭터 정보를 받아와 저장 
	}

	/// <summary>
	/// 캐릭터의 최종 파밍 양을 계산합니다.
	/// </summary>
	/// <param name="charAmount">캐릭터의 파밍 양</param>
	/// <returns>최종 파밍 양</returns>
	private float CalculateFarmingAmout(float charAmount)
	{
		return UnityEngine.Random.Range((float)charAmount * 0.7f, (float)charAmount); // 파밍 양 계산
	}

	/// <summary>
	/// 음식 파밍 양을 계산합니다.
	/// </summary>
	/// <param name="totalAmount">캐릭터의 최종 파밍 양</param>
	/// <returns>음식 파밍 양</returns>
	private float CalculateFoodAmount(float totalAmount)
	{
		return UnityEngine.Random.Range(0, (float)totalAmount * 0.1f);
	}

	/// <summary>
	/// 의약품 파밍 양을 계산합니다.
	/// </summary>
	/// <param name="totalAmount">캐릭터의 최종 파밍 양</param>
	/// <returns>의약품 파밍 양</returns>
	private float CalculateMedAmount(float totalAmount)
	{
		return UnityEngine.Random.Range(0, (float)totalAmount * 0.02f);
	}

	/// <summary>
	/// 재료 파밍 양을 계산합니다.
	/// </summary>
	/// <param name="totalAmount">캐릭터의 최종 파밍 양</param>
	/// <param name="foodAmount">음식 파밍 양</param>
	/// <param name="medAmount">의약품 최종 파밍 양</param>
	private MaterialPack[] CalculateMatAmount(float totalAmount, float foodAmount, float medAmount)
	{
		Materials mat1, mat2, mat3;
		matPack = new MaterialPack[4]; // 재료는 총 4개 수집하므로 크기는 4

		float matAmount = totalAmount - (foodAmount + medAmount);
		mat1 = (Materials) UnityEngine.Random.Range(0, 3);
		mat2 = MatValueController(mat1);
		mat3 = MatValueController(mat1, mat2);

		float mat1Amount = UnityEngine.Random.Range(matAmount * 0.3f, matAmount * 0.6f) / 5;
		float mat2Amount = UnityEngine.Random.Range((matAmount - mat1Amount) * 0.2f,
			(matAmount - mat1Amount) * 0.8f) / 5;
		float mat3Amount = UnityEngine.Random.Range(0, matAmount - (mat1Amount + mat2Amount)) / 5;

		// TODO : 파밍 양 정해지면 마저 확률 및 획득하는 아이템 종류 지정해주기

		MaterialPack mat1Pack, mat2Pack, mat3Pack, mat4Pack;
		mat1Pack = new MaterialPack(mat1, mat1Amount);
		mat2Pack = new MaterialPack(mat2, mat2Amount);
		mat3Pack = new MaterialPack(mat3, mat3Amount);
		// mat4Pack = new MaterialPack(mat4, mat4Amount);

		matPack.SetValue(mat1Pack, 0);
		matPack.SetValue(mat2Pack, 1);
		matPack.SetValue(mat3Pack, 2);
		//matPack.SetValue(mat1Pack, 0);

		return matPack;
	}

	#region ValueController

	/// <summary>
	/// 머테리얼 종류를 이전 머테리얼과 다른 종류 중에서 랜덤하게 생성합니다.
	/// </summary>
	/// <param name="_mat">첫번째 재료</param>
	/// <returns>두번째 재료</returns>
	private Materials MatValueController(Materials _mat)
	{
		Materials mat = (Materials) UnityEngine.Random.Range(0, 3);
		if (_mat == mat) // 만약 같은 재료가 2번 나올 경우 반복
		{
			mat = MatValueController(_mat);
		}

		return mat;
	}

	/// <summary>
	/// 머테리얼 종류를 이전 머테리얼과 다른 종류 중에서 랜덤하게 생성합니다.
	/// </summary>
	/// <param name="_mat1">첫번째 재료</param>
	/// <param name="_mat2">두번째 재료</param>
	/// <returns>세번째 재료</returns>
	private Materials MatValueController(Materials _mat1, Materials _mat2)
	{
		Materials mat = (Materials) UnityEngine.Random.Range(0, 3);
		if (_mat1 == mat || _mat2 == mat)
		{
			mat = MatValueController(_mat1, _mat2);
		}

		return mat;
	}

	#endregion
}