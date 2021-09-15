using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductItem : MonoBehaviour
{
    public Sprite ItemImage;
    public Sprite NoneImage;
    public GameObject ProduceButton;
    public bool produce = false;
    public bool UseItem = false;
    private Item item;
    private ItemCraft Water;
    private GameObject box;
    private GameObject bonfire;
    private float distance;
    private GameObject Player;
    private void Start()
    {
        item = this.gameObject.GetComponent<Item>();
        Water = GameObject.Find("Water_Purifier").GetComponent<ItemCraft>();
        box = GameObject.Find("MainUI").transform.Find("Box").gameObject;
        bonfire = GameObject.Find("MainUI").transform.Find("Bonfire").gameObject;
        Player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if(UseItem)
        {
            StartCoroutine(CheckItem());
        }
        distance = Vector2.Distance(Player.transform.position, transform.position);
        if (distance < 200)
        {
            ProduceButton.SetActive(true);
        }
        else
        {
            ProduceButton.SetActive(false);
        }
    }
    public bool Product
    {
        get { return produce; }
        set {
            produce = value;
            if (produce)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = ItemImage;
                ProduceButton.GetComponent<ItemButtonClick>().SetImage();
                if(item.ID == 20)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
    IEnumerator CheckItem()
    {
        while(UseItem)
        {
            switch(item.ID)
            {
                case 19:
                    Water.CraftItem = true;
                    break;
                case 20:
                    bonfire.SetActive(true);
                    break;
                case 21:
                    box.SetActive(true);
                    break;
                case 22:
                    break;
            }
            UseItem = false;
            yield return null;
        }
    }
}
