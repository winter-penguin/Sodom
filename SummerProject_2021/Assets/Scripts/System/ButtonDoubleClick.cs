using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDoubleClick : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameProfileSystem profileSystem;
	[SerializeField] private int sceneIndex;
	private const float CLICK_DELAY = 0.5f;
	private Coroutine ResetClickStatusCoroutine;
	private Button btn;
	private UnityAction profileAction;
	private GameObject GM;

	private void Init()
	{
		if (profileSystem == null) { profileSystem = transform.parent.gameObject.GetComponent<GameProfileSystem>(); }

		btn = GetComponent<Button>();
		GM = GameObject.FindGameObjectWithTag("GameController");

		profileSystem.isClicked = false;
		profileSystem.isDoubleClicked = false;
		profileAction = new UnityAction(() => GM.GetComponent<SceneLoader>().LoadLevel(sceneIndex));
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
			if (profileSystem.selectedObject != null)
			{
				/*profileSystem.selectedObject.GetComponent<Button>().onClick
					.RemoveListener(profileAction);*/
				profileSystem.selectedObject.GetComponent<Button>().onClick.RemoveAllListeners();
			}

			profileSystem.selectedObject = eventData.selectedObject;
			profileSystem.selectedObject.GetComponent<Button>().onClick
				.AddListener(profileAction);
			profileSystem.isClicked = true;
			ResetClickStatusCoroutine = StartCoroutine(ResetClickStatus());
		}
	}
}