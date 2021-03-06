using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemClick : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    private Item ParentObject;
    private DBManagerItem itemData;
    private ItemCraft itemCraft;
    void Start()
    {
        item = this.gameObject.GetComponent<Item>();
        itemData = GameObject.Find("DBManager").GetComponent<DBManagerItem>();
        ParentObject = this.gameObject.transform.parent.parent.GetComponent<Item>();

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < itemData.itemDBCraft.Length; i++)
        {
            if (itemData.itemDBCraft[i].ID == item.ID)
            {
                itemCraft = this.gameObject.GetComponent<ItemCraft>();
            }
        }
        int clickCount = eventData.clickCount;
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (clickCount == 2 && item.ItemType != 0 && ParentObject.ID == 21)
            {
                OnDoubleClick();
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            for (int i = 0; i < itemData.itemDBCraft.Length; i++)
            {
                if (itemData.itemDBCraft[i].ID == item.ID && itemData.itemDBCraft[i].Necessary_Object_ID == ParentObject.ID)
                {
                    itemCraft.CraftItem = true;
                    break;
                }
            }
        }

    }

    void OnDoubleClick()
    {
        item.useItem = true;
    }
}
