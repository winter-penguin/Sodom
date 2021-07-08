using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class MainCharcter : MonoBehaviour
{
	private Animator m_animator;
	private Coroutine KeyInputCoroutine;

	public bool isLive = true;
	private bool run = false;
	private Ray mouseRay;
	private RaycastHit mouseHit;
	private Camera MainCamera;


	void Awake()
	{
		KeyInputCoroutine = StartCoroutine(KeyInput());
		m_animator = gameObject.GetComponent<Animator>();
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	private IEnumerator KeyInput()
	{
		while (isLive)
		{
			if (run)
			{
				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					m_animator.SetBool("Run", false);
					run = false;
				}
			}

			if (!run)
			{
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					m_animator.SetBool("Run", true);
					run = true;
					Move();
				}
			}

			yield return null;
		}

		yield return null;
	}

	private bool Move()
	{
		Vector3 targetPos;
		Vector3 position = transform.position;

		mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(mouseRay, out mouseHit, 1000f))
		{
			targetPos = mouseHit.point;
			transform.position = Vector3.Lerp(position,
				new Vector3(targetPos.x, position.y, position.z), 0.2f);
			return true;
		}

		return false;
	}
}