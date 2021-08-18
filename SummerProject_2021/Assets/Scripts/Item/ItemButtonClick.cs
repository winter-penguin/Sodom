using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonClick : MonoBehaviour
{
    Button button;
    private ItemCraft item;
    private ProductItem product;
    private Image Button;
    public Sprite ProduceButtonImage;
    public Sprite UseButtonImage;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        item = this.gameObject.transform.parent.parent.GetComponent<ItemCraft>();
        product = this.gameObject.transform.parent.parent.GetComponent<ProductItem>();
    }
    
    private void ButtonImage()
    {

    }

    public void OnClickButton()
    {
        if (product.Product == false)
        {
            item.CraftItem = true;
        }
        else
        {
            product.UseItem = true;
        }
    }

}
