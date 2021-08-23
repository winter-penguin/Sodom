using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonClick : MonoBehaviour
{
    Button button;
    private ItemCraft itemCraft;
    private GameObject CurrentItem;
    private ProductItem product;
    private Image Button;
    public Sprite ProduceButtonImage;
    public Sprite UseButtonImage;
    private GameObject Player;
    private ClickMovement player;
    private float distance;
    private bool isProduct = false;
    private int floor;
    private int Playerfloor;
    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        CurrentItem = this.gameObject.transform.parent.parent.gameObject;
        itemCraft = CurrentItem.GetComponent<ItemCraft>();
        product = CurrentItem.GetComponent<ProductItem>();
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<ClickMovement>();
        if (CurrentItem.transform.position.y < -70)
        {
            floor = 1;
        }
        else if (CurrentItem.transform.position.y >= -70)
        {
            floor = 2;
        }
    }
    public void OnClickButton()
    {
        isProduct = false;
        StartCoroutine(CheckDistance());
    }
    IEnumerator CheckDistance()
    {
        while(!isProduct)
        {
            distance = Player.transform.position.x - CurrentItem.transform.position.x;
            if (Player.transform.position.y < -70)
            {
                Playerfloor = 1;
            }
            else if (Player.transform.position.y >= - 30)
            {
                Playerfloor = 2;
            }
            if (distance < 100 && distance > -100 && floor == player.Wherecharacteris)
            {
                if (product.Product == false)
                {
                    itemCraft.CraftItem = true;
                    isProduct = true;
                    break;
                }
                else
                {
                    product.UseItem = true;
                    isProduct = true;
                    break;
                }
            }
            yield return null;
        }
    }
}
