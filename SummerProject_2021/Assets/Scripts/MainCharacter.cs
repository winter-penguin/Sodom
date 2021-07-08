using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MainCharacter : MonoBehaviour
{
	private Animator m_animator;
	private Coroutine KeyInputCoroutine;
	private Coroutine MoveCoroutine;

	public bool isLive = true;
	public float playerSpeed = 10;


	void Awake()
	{
		KeyInputCoroutine = StartCoroutine(KeyInput());
		m_animator = gameObject.GetComponent<Animator>();
	}

	private IEnumerator KeyInput()
	{
		while (isLive)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				m_animator.SetBool("Run", true);

				if (MoveCoroutine != null)
				{
					StopCoroutine(MoveCoroutine);
					MoveCoroutine = StartCoroutine(Move(playerSpeed));
				}
				else
				{
					MoveCoroutine = StartCoroutine(Move(playerSpeed));
				}

				
			}
			yield return null;
		}
	}

	private IEnumerator Move(float speed)
	{
		Vector3 position = transform.position;
		Vector3 lastPosition = position;

		if (lastPosition != Input.mousePosition)
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float walkedTime = 0;
			float totalTime = 0;

			if (mousePosition.x < position.x)
			{
				GetComponent<SpriteRenderer>().flipX = true;
				totalTime = position.x-mousePosition.x; // 플레이어 이동 시간을 넣을 것
			}
			else if (mousePosition.x > position.x)
			{
				GetComponent<SpriteRenderer>().flipX = false;
				totalTime = mousePosition.x - position.x; // 플레이어 이동 시간을 넣을 것
			}

			while (walkedTime < totalTime)
			{
				transform.position = Vector2.Lerp(position,
					new Vector2(mousePosition.x, position.y), walkedTime / totalTime);
				walkedTime += speed * Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}

			transform.position = new Vector3(mousePosition.x, position.y, position.z);
			m_animator.SetBool("Run",false);
		}
	}
}