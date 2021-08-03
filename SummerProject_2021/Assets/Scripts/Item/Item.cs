using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Item_Base
{
    void Update()
    {
        if (useItem)
        {
            StartCoroutine(CheckUseItem());
        }

    }
    IEnumerator CheckUseItem()
    {
        switch (CurrentItemType)
        {
            case ItemType.Ingredient:

            case ItemType.Food:
                break;
            case ItemType.Medicine:
                break;
            case ItemType.Weapon:
                if (ItemEquip)
                {
                    ItemEquip = false;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂøÇØÁ¦");
                }
                else if (ItemEquip == false)
                {
                    ItemEquip = true;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂø");
                }
                break;
            case ItemType.Tool:
                if (ItemEquip)
                {
                    ItemEquip = false;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂøÇØÁ¦");
                }
                else if (ItemEquip == false)
                {
                    ItemEquip = true;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂø");
                }
                break;
        }
        useItem = false;
        yield return null;
    }
}
