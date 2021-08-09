/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-07
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProfileSystem : MonoBehaviour
{
	public GameObject selectedObject
	{
		get { return currentObject; }
		set
		{
			currentObject = value;
			if (lastSelectObject != currentObject)
			{
				if(lastSelectObject != null){ lastSelectObject.transform.GetChild(0).gameObject.SetActive(false);}
				lastSelectObject = currentObject;
				currentObject.transform.GetChild(0).gameObject.SetActive(true);
			}
		}
	}
	
	public bool isClicked, isDoubleClicked;

	private GameObject currentObject = null;

	private GameObject lastSelectObject = null;

}