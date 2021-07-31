/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-30
///  Contact : kjhcorgi99@gmail.com
///  Temp script for Item inventory system
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNameArranger : MonoBehaviour
{
    public string ItemName; // 임시로 작성하는 아이템 이름

    private void Start()
    {
        DebugItemName(ItemName);
    }

    private string DebugItemName(string item)
    {
        
        if(item.Length > 4)
        {
            modifiedItem = item.Insert(3,"\n");
        }

        return modifiedItem;
    }
}
