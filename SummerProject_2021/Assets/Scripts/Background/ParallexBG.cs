/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-22
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum BackgroundKinds
{
	building,
	cloud
};

public enum BuildingKinds
{
	mainBuilding,
	frontBuilding,
	midBuilding,
	backBuilding,
	frontCloud,
	backCloud
};

public enum CloudKinds
{
	frontCloud,
	backCloud
};

public class ParallexBG : MonoBehaviour
{
	private MainCharacter player;
	private CameraController cameraInfo;
	private const float SCREEN_HALF_WIDTH = 1250;

	[HideInInspector] public BackgroundKinds sort;
	[HideInInspector] public BuildingKinds building;
	[HideInInspector] public CloudKinds cloud;

	private float parallexSpeed;

	private void Init()
	{
		player = GameObject.FindWithTag("Player").GetComponent<MainCharacter>();
		cameraInfo = Camera.main.gameObject.GetComponent<CameraController>();
	}

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		
		if (sort == BackgroundKinds.building)
		{
			parallexSpeed = CalBuildingSpeed();
			StartCoroutine(ParallexMove());
		}
		else if(sort == BackgroundKinds.cloud)
		{
			parallexSpeed = CalCloudSpeed();
			StartCoroutine(ParallexScrolling());
		}
		
		
	}

	private float CalBuildingSpeed()
	{
		float speed;
		switch (building)
		{
			case BuildingKinds.mainBuilding:
				speed = 50f;
				break;
			case BuildingKinds.frontBuilding:
				speed = 50f;
				break;
			case BuildingKinds.midBuilding:
				speed = 20f;
				break;
			case BuildingKinds.backBuilding:
				speed = 10f;
				break;
			default:
				Debug.Log("Building sort not existing");
				return 0;
		}
		return speed;
	}

	private float CalCloudSpeed()
	{
		float speed;
		switch (cloud)
		{
			case CloudKinds.frontCloud:
				speed = 50.0f;
				break;
			
			case CloudKinds.backCloud:
				speed = 10.0f;
				break;
			default:
				Debug.Log("Cloud sort not existing");
				return 0;
		}

		return speed;
	}

	private IEnumerator ParallexMove()
	{
		while ( /*player.isLive*/ true)
		{
			Vector3 dir = new Vector3(cameraInfo.playerDifferenceX, 0, 0).normalized;
			gameObject.transform.Translate(-dir * parallexSpeed * Time.deltaTime);

			yield return null;
		}
	}

	private IEnumerator ParallexScrolling()
	{
		Vector3 cameraPos;
		Vector3 dir = Vector3.left;	// 구름 이미지가 좌측으로 이동
		float spriteSize = GetComponent<SpriteRenderer>().bounds.size.x;
		while (true)
		{
			cameraPos = Camera.main.transform.position;
			if (transform.position.x + spriteSize/2 < -SCREEN_HALF_WIDTH)
			{
				transform.position = new Vector3(transform.position.x+ 3*spriteSize, transform.position.y, transform.position.z);
			}
			transform.Translate(dir*parallexSpeed*Time.deltaTime);
			yield return null;
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(ParallexBG))]
public class ParallexBGEditor : Editor
{
	private SerializedProperty sort;
	private SerializedProperty building;
	private SerializedProperty cloud;
	
	
	private void OnEnable()
	{
		sort = serializedObject.FindProperty("sort");
		building = serializedObject.FindProperty("building");
		cloud = serializedObject.FindProperty("cloud");
	}
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField(sort);

		ParallexBG script = (ParallexBG) target;

		switch (script.sort)
		{
			case BackgroundKinds.building:
				EditorGUILayout.PropertyField(building);
				/*script.building = (BuildingKinds)EditorGUILayout.EnumPopup("Building kinds", script.building);*/
				break;
			
			case BackgroundKinds.cloud:
				EditorGUILayout.PropertyField(cloud);
				/*script.cloud = (CloudKinds) EditorGUILayout.EnumPopup("Cloud Kinds", script.cloud);*/
				break;
		}

		serializedObject.ApplyModifiedProperties();
	}
}
#endif