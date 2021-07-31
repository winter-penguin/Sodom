/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-31
///  Contact : kjhcorgi99@gmail.com
///  Temp script for Item inventory system
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNameArranger : MonoBehaviour
{
    private Text ItemName; // 임시로 작성하는 아이템 이름
    private const int MAX_STRING_SIZE = 4;  // 한줄에 표시할 글자 수

    private void Init()
    {
        ItemName = GetComponent<Text>();
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        ItemName.text = ArrangeItemName(ItemName.text);
    }
    
    /// <summary>
    /// 한줄에 표시할 글자 수에 따라 문장을 넘깁니다.
    /// </summary>
    /// <param name="_itemName">아이템 이름</param>
    /// <returns></returns>
    private string ArrangeItemName(string _itemName)
    {
        string frontString;
        string backString;
        string completeString;
        if (_itemName.Length > MAX_STRING_SIZE)
        {
            frontString = _itemName.Substring(0, MAX_STRING_SIZE);
            backString = _itemName.Substring(MAX_STRING_SIZE, _itemName.Length - MAX_STRING_SIZE);
            completeString = frontString + "\n" + ArrangeItemName(backString);
        }
        else
        {
            return _itemName;
        }

        return completeString;
    }
}
