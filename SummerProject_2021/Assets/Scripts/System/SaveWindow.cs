/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using UnityEngine;

public class SaveWindow : MonoBehaviour, IManageWindow
{
	[SerializeField] private GameObject mainWindow;

	private void Init()
	{
		if (mainWindow == null)
		{
			mainWindow = GameObject.Find("MainWindow").gameObject;
		}
	}

	private void Awake()
	{
		Init();
	}

	public void OpenSpecificWindow()
	{
		mainWindow.SetActive(false);
		gameObject.SetActive(true);
	}

	public void CloseSpecificWindow()
	{
		mainWindow.SetActive(true);
		gameObject.SetActive(false);
	}
}