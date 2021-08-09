using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce : MonoBehaviour
{
    [SerializeField]
    private GameObject ProduceGridSetting;

    private Slot produceItem;
    private Slot[] slots;

    void Start()
    {

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
