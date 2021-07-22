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
    public ItemType CurrentitemType = ItemType.Food;

    [SerializeField] protected float Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
    public Sprite itemImage;
    public GameObject itemPrefab;
    private DBManager_Item_food_medicien itemData_Food_Medicien;
    private DBManager_Item_Material itemData_Material;
    private DBManager_Item_Object itemData_Object;

    private bool useItem = false;
    // Start is called before the first frame update
    void Start()
    {
        itemData_Food_Medicien = GameObject.Find("ItemDB").GetComponent<DBManager_Item_food_medicien>();
        itemData_Material = GameObject.Find("ItemDB").GetComponent<DBManager_Item_Material>();
        itemData_Object = GameObject.Find("ItemDB").GetComponent<DBManager_Item_Object>();
        StartCoroutine(DataSet());
    }
    protected IEnumerator DataSet()
    {
        bool DataLoading = true;
        while (DataLoading)
        {
            if (CurrentitemType == ItemType.Food || CurrentitemType == ItemType.Medicine)
            {
                for (int i = 0; i < itemData_Food_Medicien.itemDB.Length; i++)
                {

                    if (itemData_Food_Medicien.itemDB[i].name == CurrentItem.ToString())
                    {
                        Hunger = itemData_Food_Medicien.itemDB[i].Hunger;
                        Thirst = itemData_Food_Medicien.itemDB[i].Thirst;
                        Heal = itemData_Food_Medicien.itemDB[i].Heal;
                        Fatigue = itemData_Food_Medicien.itemDB[i].Fatigue;
                        Charge_Space = itemData_Food_Medicien.itemDB[i].Charge_Space;
                        DataLoading = false;
                    }
                }
                yield return null;
            }
            else if(CurrentitemType == ItemType.Weapon || CurrentitemType == ItemType.Tool)
            {
                for (int i = 0; i < itemData_Material.itemDB.Length; i++)
                {
                    if (itemData_Material.itemDB[i].name == CurrentItem.ToString())
                    {
                        AD = itemData_Material.itemDB[i].AD;
                        Attack_Range = itemData_Material.itemDB[i].Attack_Range;
                        Charge_Space = itemData_Material.itemDB[i].Charge_Space;
                        DataLoading = false;
                    }
                }
                yield return null;
            }
            else if (CurrentitemType == ItemType.Ingredient || CurrentitemType == ItemType.Product)
            {
                for (int i = 0; i < itemData_Object.itemDB.Length; i++)
                {
                    if (itemData_Object.itemDB[i].name == CurrentItem.ToString())
                    {
                        Fatigue = itemData_Object.itemDB[i].Fatigue;
                        Capacity = itemData_Object.itemDB[i].Capacity;
                        Charge_Space = itemData_Object.itemDB[i].Charge_Space;
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
            UseItem();
        }
    }

    void UseItem()
    {

    }
}
