using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductItem : MonoBehaviour
{
    public bool Product = false;
    public bool UseItem = false;
    private Item item;
    private ItemCraft Water;
    private void Start()
    {
        item = this.gameObject.GetComponent<Item>();
        Water = GameObject.Find("Water").GetComponent<ItemCraft>();
    }
    private void Update()
    {
        if(UseItem)
        {
            StartCoroutine(CheckItem());
        }
    }
    IEnumerator CheckItem()
    {
        while(UseItem)
        {
            switch(item.ID)
            {
                case 19:
                    WaterPurifier();
                    break;
                case 20:
                case 21:
                case 22:
                    break;
            }
            UseItem = false;
            yield return null;
        }
    }
    private void WaterPurifier()
    {
        Water.CraftItem = true;
    }

}
