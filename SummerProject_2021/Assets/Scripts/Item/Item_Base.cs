using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Base : MonoBehaviour
{
    public enum ItemType { Food, Tool, Weapon, Ingredient, Medicine, Product };
    public enum ItemName
    {
        Brown_Water, Water, Raw_Meat, Vegetable, Vegetable_Soup, Fried_Meat, Bandage, Pill,
        Crowbar, Shovel, Dagger, Sword,
        Cloth, Wood, Stone, Iron, Bonfire, Bed, Bag, Water_Purifier, Box
    };
    public ItemName CurrentItem = ItemName.Cloth;
    public ItemType CurrentItemType = ItemType.Food;
    [SerializeField] protected float Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
    public Sprite itemImage;
    public GameObject itemPrefab;
    private DBManager_Item itemData;
    // Start is called before the first frame update
    public bool useItem = false;
    public bool ItemEquip = false;
    void Start()
    {
        itemData = GameObject.Find("DBManager").GetComponent<DBManager_Item>();
        StartCoroutine(DataSet());
    }
    protected IEnumerator DataSet()
    {
        bool DataLoading = true;
        while (DataLoading)
        {
            if (CurrentItemType == ItemType.Food || CurrentItemType == ItemType.Medicine)
            {
                for (int i = 0; i < itemData.itemDB_Food_Medicine.Length; i++)
                {

                    if (itemData.itemDB_Food_Medicine[i].name == CurrentItem.ToString())
                    {
                        Hunger = itemData.itemDB_Food_Medicine[i].Hunger;
                        Thirst = itemData.itemDB_Food_Medicine[i].Thirst;
                        Heal = itemData.itemDB_Food_Medicine[i].Heal;
                        Fatigue = itemData.itemDB_Food_Medicine[i].Fatigue;
                        Charge_Space = itemData.itemDB_Food_Medicine[i].Charge_Space;
                        DataLoading = false;
                    }
                }
                yield return null;
            }
            else if(CurrentItemType == ItemType.Weapon || CurrentItemType == ItemType.Tool)
            {
                for (int i = 0; i < itemData.itemDB_Material.Length; i++)
                {
                    if (itemData.itemDB_Material[i].name == CurrentItem.ToString())
                    {
                        AD = itemData.itemDB_Material[i].AD;
                        Attack_Range = itemData.itemDB_Material[i].Attack_Range;
                        Charge_Space = itemData.itemDB_Material[i].Charge_Space;
                        DataLoading = false;
                    }
                }
                yield return null;
            }
            else if (CurrentItemType == ItemType.Ingredient || CurrentItemType == ItemType.Product)
            {
                for (int i = 0; i < itemData.itemDB_Object.Length; i++)
                {
                    if (itemData.itemDB_Object[i].name == CurrentItem.ToString())
                    {
                        Fatigue = itemData.itemDB_Object[i].Fatigue;
                        Capacity = itemData.itemDB_Object[i].Capacity;
                        Charge_Space = itemData.itemDB_Object[i].Charge_Space;
                        DataLoading = false;
                    }
                }
                yield return null;
            }
        }
        yield return null;
    }

    void Update()
    {
        if (useItem)
        {
            StartCoroutine(CheckUseItem());
        }

    }

    IEnumerator CheckUseItem()
    {
        switch (CurrentItemType)
        {
            case ItemType.Ingredient:

            case ItemType.Food:
                break;
            case ItemType.Medicine:
                break;
            case ItemType.Weapon:
                if (ItemEquip)
                {
                    ItemEquip = false;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂøÇØÁ¦");
                }
                else if (ItemEquip == false)
                {
                    ItemEquip = true;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂø");
                }
                break;
            case ItemType.Tool:
                if (ItemEquip)
                {
                    ItemEquip = false;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂøÇØÁ¦");
                }
                else if (ItemEquip == false)
                {
                    ItemEquip = true;
                    Debug.Log("¾ÆÀÌÅÛ ÀåÂø");
                }
                break;
        }
        useItem = false;
        yield return null;
    }
}
