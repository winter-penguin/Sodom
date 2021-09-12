using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonClick : MonoBehaviour
{
    Button button;
    private ItemCraft itemCraft;
    private GameObject CurrentItem;
    private ProductItem product;
    public Image ButtonImage;
    public Sprite ProduceButtonImage;
    public Sprite UseButtonImage;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        CurrentItem = this.gameObject.transform.parent.parent.gameObject;
        itemCraft = CurrentItem.GetComponent<ItemCraft>();
        product = CurrentItem.GetComponent<ProductItem>();
    }
    public void OnClickButton()
    {
        if (product.Product == false)
        {
            itemCraft.CraftItem = true;
        }
        else
        {
            product.UseItem = true;
        }
    }
    public void SetImage()
    {
        ButtonImage.sprite = UseButtonImage;
    }
}
