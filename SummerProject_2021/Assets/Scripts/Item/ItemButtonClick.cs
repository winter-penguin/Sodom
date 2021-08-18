using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonClick : MonoBehaviour
{
    Button button;
    private ItemCraft item;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        item = this.gameObject.transform.parent.parent.GetComponent<ItemCraft>();
    }
    
    public void OnClickButton()
    {
        if(item.CraftItem == false)
        {
            item.CraftItem = true;
        }

    }
}
