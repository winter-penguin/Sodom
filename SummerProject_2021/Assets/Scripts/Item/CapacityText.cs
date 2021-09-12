using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityText : MonoBehaviour
{
    private Text capacityText;
    private DBManagerItem ItemData;
    private void Awake()
    {
        capacityText = this.gameObject.GetComponent<Text>();
        ItemData = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
    }
    private void OnEnable()
    {
        SetText();
    }

    private void Update()
    {
        if(ItemData.capacity)
        {
            SetText();
        }
    }
    private void SetText()
    {
        capacityText.text = ItemData.currentCapacity  + " / " + ItemData.itemDB[20].Capacity;
        ItemData.capacity = false;
    }
}
