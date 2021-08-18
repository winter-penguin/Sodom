/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
	[SerializeField] private CharacterValue player;

	[SerializeField] private GameObject healthProgress;
	private Image healthMat;
	private Material newHealthMat;
	[SerializeField] private GameObject hungryProgress;
	private Image hungryMat;
	private Material newHungryMat;
	[SerializeField] private GameObject fatigueProgress;
	private Image fatigueMat;
	private Material newFatigueMat;
	[SerializeField] private GameObject thirstProgress;
	private Image thirstMat;
	private Material newThirstMat;
	private static readonly int Progress = Shader.PropertyToID("_Progress");
	
	
	private GameManager GM;

	private void Init()
	{
		if (player == null)
		{
			player = FindObjectOfType<CharacterValue>();
#if UNITY_EDITOR
			if (player == null)
			{
				Debug.LogWarningFormat("메인 캐릭터의를 {0}속  {1}컴포넌트의 Player 에 참조해주세요.",
					this, GetType().Name 
				);
			}
#endif
		}

		GM = FindObjectOfType<GameManager>();

		healthMat = healthProgress.GetComponent<Image>();
		newHealthMat = Instantiate(healthMat.material);
		hungryMat = hungryProgress.GetComponent<Image>();
		newHungryMat = Instantiate(hungryMat.material);
		fatigueMat = fatigueProgress.GetComponent<Image>();
		newFatigueMat = Instantiate(fatigueMat.material);
		thirstMat = thirstProgress.GetComponent<Image>();
		newThirstMat = Instantiate(thirstMat.material);
	}

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		StartCoroutine(CheckStatusValue());
	}

	private IEnumerator CheckStatusValue()
	{
		while (!GM.isEnd)
		{
			newHealthMat.SetFloat(Progress, player.health/150);
			newHungryMat.SetFloat(Progress, player.hunger/100);
			newFatigueMat.SetFloat(Progress, player.fatigue/100);
			newThirstMat.SetFloat(Progress, player.thirst/100);

			healthMat.material = newHealthMat;
			hungryMat.material = newHungryMat;
			fatigueMat.material = newFatigueMat;
			thirstMat.material = newThirstMat;
			
			yield return null;
		}
	}
}