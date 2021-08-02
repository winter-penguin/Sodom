using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemClick : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    private GameObject produceUI;
    private GameObject boxGridSetting;
    private GameObject ParentObject;
    void Start()
    {
        item = this.gameObject.GetComponent<Item>();
        produceUI = GameObject.Find("Canvas").transform.Find("ProduceUI").gameObject;
        boxGridSetting = GameObject.Find("BoxGridSetting");
        ParentObject = this.gameObject.transform.parent.gameObject;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (clickCount == 2 && item.CurrentItemType != Item_Base.ItemType.Ingredient && ParentObject == boxGridSetting)
            {
                OnDoubleClick();
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("InputButton.Right");
            produceUI.SetActive(true);
        }

    }

    void OnDoubleClick()
    {
        Debug.Log("Double Clicked");
        Debug.Log(item.CurrentItemType);
        Debug.Log(item.CurrentItem);
        item.useItem = true;
    }
}
