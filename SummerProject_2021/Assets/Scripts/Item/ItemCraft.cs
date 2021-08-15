using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemCraft : MonoBehaviour
{
    [SerializeField]
    private GameObject ProduceUI;
    private GameObject BoxGridSetting;
    private Item[] item;
    private Produce produce;
    public bool CraftItem = false;
    void Start()
    {
        produce = ProduceUI.transform.Find("ProduceGridSetting").GetComponent<Produce>();
        BoxGridSetting = GameObject.Find("BoxGridSetting");
    }
    void Update()
    {
        if(CraftItem)
        {
            ProduceItem();
        }
    }

    void ProduceItem()
    {
        item = BoxGridSetting.GetComponentsInChildren<Item>();
        Item mItem = this.gameObject.GetComponent<Item>();
        Debug.Log(mItem.CurrentItem);
        CraftItem = false;
        produce.ProduceItem(mItem);
        for(int i = 0; i < item.Length; i++)
        {
            if(item[i].ID == mItem.itemCraft[0].Necessary_Material_ID1)
            {
                produce.AcquireItem(item[i], mItem.itemCraft[0].Amount_Of_Material1);
                if(mItem.itemCraft[0].Necessary_Material_ID2 == 0)
                {
                    break;
                }
            }
            else if(item[i].ID == mItem.itemCraft[0].Necessary_Material_ID2)
            {
                produce.AcquireItem(item[i], mItem.itemCraft[0].Amount_Of_Material2);
                if (mItem.itemCraft[0].Necessary_Material_ID3 == 0)
                {
                    break;
                }
            }
            else if (item[i].ID == mItem.itemCraft[0].Necessary_Material_ID3)
            {
                produce.AcquireItem(item[i], mItem.itemCraft[0].Amount_Of_Material3);
            }
        }

    }
}
