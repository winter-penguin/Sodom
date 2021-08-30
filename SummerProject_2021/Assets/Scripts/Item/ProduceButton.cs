using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceButton : MonoBehaviour
{
    private GameObject ProduceUI;
    public ProductItem product;
    public Item item;
    public List<Item> MaterialItem = new List<Item>();
    private Slot CraftSlot;
    private Slot[] slots;
    private void Start()
    {
        CraftSlot = GameObject.Find("CraftSlot").GetComponent<Slot>();
        slots = GameObject.Find("ProduceGridSetting").GetComponentsInChildren<Slot>();
        ProduceUI = GameObject.Find("ProduceUI");
    }
    
    public void OnClickProduceButton()
    {
        MaterialItem.RemoveAll(x => true);
        ProduceUI.SetActive(false);
        item.ItemCount += CraftSlot.ProduceItemCount;
        for (int i = 0; i < MaterialItem.Count; i++)
        {
            if(MaterialItem[i] != null)
            {
                MaterialItem[i].ItemCount -= slots[i].ProduceItemCount;
            }
        }
        if (item.ItemType == 6)
        {
            product.Product = true;
        }
    }

    public void OnClickCancelButton()
    {

        ProduceUI.SetActive(false);
    }
    public void DownButton()
    {
        if(CraftSlot.ProduceItemCount > 1)
        {
            CraftSlot.UPDownCount(-1);
        }

    }

    public void UpButton()
    {
        CraftSlot.UPDownCount(1);
    }
}
