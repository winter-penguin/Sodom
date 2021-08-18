using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemCraft : MonoBehaviour
{
    [SerializeField]
    private GameObject ProduceUI;
    [SerializeField]
    public GameObject itemInformation;
    private Item[] item;

    [SerializeField]
    private GameObject ProduceGridSetting;

    public bool CraftItem = false;
    private Slot produceItem;
    private Slot[] slots;
    private ProduceButton produce;
    void Start()
    {
        itemInformation = GameObject.Find("ItemInformation");
        ProduceUI = GameObject.Find("MainUI").transform.Find("ProduceUI").gameObject;
        ProduceGridSetting = ProduceUI.transform.Find("ProduceGridSetting").gameObject;
    }
    void Update()
    {
        if(CraftItem)
        {
            ProduceUI.SetActive(true);
            ItemInformation();
        }
    }

    void ItemInformation()
    {
        produce = GameObject.Find("ProduceButton").GetComponent<ProduceButton>();
        produce.product = this.gameObject.GetComponent<ProductItem>();
        item = itemInformation.GetComponentsInChildren<Item>();
        Item mItem = this.gameObject.GetComponent<Item>();
        Debug.Log(mItem.CurrentItem);
        CraftItem = false;
        ProduceItem(mItem);
        for(int i = 0; i < item.Length; i++)
        {
            if(item[i].ID == mItem.itemCraft[0].Necessary_Material_ID1)
            {
                AcquireItem(item[i], mItem.itemCraft[0].Amount_Of_Material1);
                if(mItem.itemCraft[0].Necessary_Material_ID2 == 0)
                {
                    break;
                }
            }
            else if(item[i].ID == mItem.itemCraft[0].Necessary_Material_ID2)
            {
                AcquireItem(item[i], mItem.itemCraft[0].Amount_Of_Material2);
                if (mItem.itemCraft[0].Necessary_Material_ID3 == 0)
                {
                    break;
                }
            }
            else if (item[i].ID == mItem.itemCraft[0].Necessary_Material_ID3)
            {
                AcquireItem(item[i], mItem.itemCraft[0].Amount_Of_Material3);
            }
        }
    }

    public void ProduceItem(Item _item, int _count = 1)
    {
        produceItem = ProduceGridSetting.transform.parent.GetChild(0).GetComponent<Slot>();
        produceItem.AddItem(_item, _count);
    }

    public void AcquireItem(Item _item, int _count)
    {
        slots = ProduceGridSetting.GetComponentsInChildren<Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
