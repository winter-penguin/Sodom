using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemCraft : MonoBehaviour
{
    [SerializeField]
    private GameObject ProduceUI;
    //private Item[] item;
    private DBManagerItem ItemData;
    [SerializeField]
    private GameObject ProduceGridSetting;

    public bool CraftItem = false;
    private Slot produceItem;
    private Slot[] slots;
    private ProduceButton produce;
    void Start()
    {
        ProduceUI = GameObject.Find("MainUI").transform.Find("ProduceUI").gameObject;
        ProduceGridSetting = ProduceUI.transform.Find("ProduceGridSetting").gameObject;
        ItemData = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
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
        produce.item = this.gameObject.GetComponent<Item>();
        Item mItem = this.gameObject.GetComponent<Item>();

        CraftItem = false;
        ProduceItem(mItem.ID - 1);
        for (int i = 0; i < ItemData.itemDB.Length; i++)
        {
            if(ItemData.itemDB[i].ID == mItem.itemCraft[0].Necessary_Material_ID1)
            {
                produce.MaterialItem.Add(ItemData.itemDB[i]);
                AcquireItem(i, mItem.itemCraft[0].Amount_Of_Material1);
                if(mItem.itemCraft[0].Necessary_Material_ID2 == 0)
                {
                    break;
                }
            }
            else if(ItemData.itemDB[i].ID == mItem.itemCraft[0].Necessary_Material_ID2)
            {
                produce.MaterialItem.Add(ItemData.itemDB[i]);
                AcquireItem(i, mItem.itemCraft[0].Amount_Of_Material2);
                if (mItem.itemCraft[0].Necessary_Material_ID3 == 0)
                {
                    break;
                }
            }
            else if (ItemData.itemDB[i].ID == mItem.itemCraft[0].Necessary_Material_ID3)
            {
                produce.MaterialItem.Add(ItemData.itemDB[i]);
                AcquireItem(i, mItem.itemCraft[0].Amount_Of_Material3);
            }
        }
    }

    public void ProduceItem(int index, int _count = 1)
    {
        GameObject UpDownButton = GameObject.Find("CraftSlot").transform.Find("UpDownButton").gameObject;
        produceItem = ProduceGridSetting.transform.parent.GetChild(0).GetComponent<Slot>();
        produceItem.AddItem(index, _count);
        if (ItemData.itemDB[index].ID == 19 || ItemData.itemDB[index].ID == 20 || ItemData.itemDB[index].ID == 22)
        {
            UpDownButton.SetActive(false);
        }
        else
        {
            UpDownButton.SetActive(true);
        }
    }

    public void AcquireItem(int index, int _count)
    {
        slots = ProduceGridSetting.GetComponentsInChildren<Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ProduceItemCount == 0)
            {
                slots[i].AddItem(index, _count);
                return;
            }
        }
    }
}
