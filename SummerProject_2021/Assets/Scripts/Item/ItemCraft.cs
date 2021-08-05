using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemCraft : MonoBehaviour
{
    [SerializeField]
    private GameObject ProduceUI;
    private Produce produce;
    public bool CraftItem = false;
    void Start()
    {
        produce = ProduceUI.transform.Find("ProduceGridSetting").GetComponent< Produce>();
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
        Item mItem = this.gameObject.GetComponent<Item>();
        Debug.Log(mItem.CurrentItem);
        CraftItem = false;
        produce.AcquireItem(mItem);
    }
}
