/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
	[SerializeField] private ClickMovement player;

	private void Init()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickMovement>();
#if UNITY_EDITOR
			if (player == null)
			{
				Debug.LogWarningFormat("메인 캐릭터의를 {0}속  {1}컴포넌트의 Player 에 참조해주세요.",
					this, GetType().Name 
				);
			}
#endif
		}
	}

	private void Awake()
	{
		Init();
	}
}