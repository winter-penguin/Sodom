using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceButton : MonoBehaviour
{
    private GameObject ProduceUI;
    public ProductItem product;
    public Item item;
    private DBManagerItem itemdata;
    public List<ItemDB> MaterialItem = new List<ItemDB>();
    private Slot CraftSlot;
    private Slot[] slots;
    private void Start()
    {
        CraftSlot = GameObject.Find("CraftSlot").GetComponent<Slot>();
        slots = GameObject.Find("ProduceGridSetting").GetComponentsInChildren<Slot>();
        ProduceUI = GameObject.Find("ProduceUI");
        itemdata = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
    }
    private void OnDisable()
    {
        MaterialItem.RemoveAll(x => true);
    }
    public void OnClickProduceButton()
    {
        int index = item.ID - 1;
        itemdata.itemDB[index].ItemCount += CraftSlot.ProduceItemCount;
        for (int i = 0; i < MaterialItem.Count; i++)
        {
            if(MaterialItem[i].ID != 0)
            {
                itemdata.SetCount(MaterialItem[i].ID - 1, -slots[i].ProduceItemCount);
                //itemdata.itemDB[MaterialItem[i].ID - 1].ItemCount -= slots[i].ProduceItemCount;
            }
        }
        if (item.ItemType == 6)
        {
            product.Product = true;
        }
        ProduceUI.SetActive(false);
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
