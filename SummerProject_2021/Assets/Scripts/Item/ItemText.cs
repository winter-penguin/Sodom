using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    private DBManagerItem ItemData;
    private Item item;
    private Text itemText;
    string[] text;
    private int num;
    // Start is called before the first frame update
    void Awake()
    {
        ItemData = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
        item = this.gameObject.GetComponent<Item>();
        itemText = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        text = itemText.text.Split('X');
    }
    private void OnEnable()
    {
        SetText();
    }
    public void SetText()
    {
        if(item.ID != 0)
        {
            itemText.text = text[0] + "X " + ItemData.itemDB[item.ID - 1].ItemCount;
        }
    }
}
