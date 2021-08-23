/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-15
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using UnityEngine;

public interface IManageWindow
{
	public void OpenSpecificWindow();
	public void CloseSpecificWindow();
	
	/// <summary>
	/// 창을 열 준비가 될 때까지 기다립니다.
	/// </summary>
	public IEnumerator WaitUntillReady();
}
