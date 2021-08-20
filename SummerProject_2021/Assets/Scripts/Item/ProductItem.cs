using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductItem : MonoBehaviour
{
    public bool Product = false;
    public bool UseItem = false;
    private Item item;
    private ItemCraft Water;
    private GameObject box;
    private GameObject bonfire;
    private void Start()
    {
        item = this.gameObject.GetComponent<Item>();
        Water = GameObject.Find("Water").GetComponent<ItemCraft>();
        box = GameObject.Find("MainUI").transform.Find("Box").gameObject;
        bonfire = GameObject.Find("MainUI").transform.Find("Bonfire").gameObject;
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
                    Water.CraftItem = true;
                    break;
                case 20:
                    bonfire.SetActive(true);
                    break;
                case 21:
                    box.SetActive(true);
                    break;
                case 22:
                    break;
            }
            UseItem = false;
            yield return null;
        }
    }
}
