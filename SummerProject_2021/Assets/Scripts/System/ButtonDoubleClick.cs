using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDoubleClick : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameProfileSystem profileSystem;
	private const float CLICK_DELAY = 0.5f;
	private Coroutine ResetClickStatusCoroutine;

	private void Init()
	{
		if (profileSystem == null) { profileSystem = transform.parent.gameObject.GetComponent<GameProfileSystem>(); }
		
		profileSystem.isClicked = false;
		profileSystem.isDoubleClicked = false;

	}

	private void Awake()
	{
		Init();
	}

	private IEnumerator ResetClickStatus()
	{
		yield return new WaitForSeconds(CLICK_DELAY);
		profileSystem.isClicked = false;
		profileSystem.isDoubleClicked = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (profileSystem.isDoubleClicked)
		{
			StopCoroutine(ResetClickStatusCoroutine);
			profileSystem.isDoubleClicked = false;
		}

		if (profileSystem.isClicked && profileSystem.selectedObject == eventData.selectedObject)
		{
			Debug.Log("Double click detected!!");
			profileSystem.isDoubleClicked = true;
		}
		else
		{
			Debug.Log("Click detected!!");
			profileSystem.selectedObject = eventData.selectedObject;
			profileSystem.isClicked = true;
			ResetClickStatusCoroutine = StartCoroutine(ResetClickStatus());
		}
	}
}