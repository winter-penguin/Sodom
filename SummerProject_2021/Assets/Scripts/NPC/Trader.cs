/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-20
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
	private GameManager GM;
	[SerializeField] private GameObject traderPoint;

	private Vector3 targetPos;

	private void Init()
	{
		GM = FindObjectOfType<GameManager>();
	}

	private void Awake()
	{
		Init();
	}

	public void TraderAppear()
	{
		Vector3 playerPos = new Vector3(-1300, -290, 0);
		targetPos = new Vector3(traderPoint.transform.position.x, playerPos.y, playerPos.z);
		gameObject.SetActive(true);
	}

	public IEnumerator TraderMoving()
	{
		float TotalTime = 5f;
		float elapsedTime = 0;
		Vector3 playerPos = gameObject.transform.position;

		while (elapsedTime < TotalTime)
		{
			gameObject.transform.position =
				Vector3.Lerp(playerPos, targetPos, elapsedTime / TotalTime);
			elapsedTime += Time.deltaTime;

			if (targetPos.x - gameObject.transform.position.x < Mathf.Epsilon)
			{
				var o = gameObject;
				Vector3 currentPos = o.transform.position;
				o.transform.position = new Vector3(targetPos.x, currentPos.y,
					currentPos.z);
			}

			yield return new WaitForEndOfFrame();
		}
	}

	private void Trading()
	{
	}
}