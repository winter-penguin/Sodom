using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item_Craft
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public int Necessary_Material_ID1, Amount_Of_Material1, Necessary_Material_ID2, Amount_Of_Material2,
                Necessary_Material_ID3, Amount_Of_Material3, Necessary_Object_ID;
}
public class Item_Base : MonoBehaviour
{
    [SerializeField]
    public Item_Craft[] item_Craft;
    public enum ItemType { Food, Tool, Weapon, Ingredient, Medicine, Product };
    public enum ItemName
    {
        Brown_Water, Water, Raw_Meat, Vegetable, Vegetable_Soup, Fried_Meat, Bandage, Pill,
        Crowbar, Shovel, Dagger, Sword,
        Cloth, Wood, Stone, Iron, Bonfire, Bed, Bag, Water_Purifier, Box
    };
    public ItemName CurrentItem = ItemName.Cloth;
    public ItemType CurrentItemType = ItemType.Food;
    [SerializeField] protected float ID, Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
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
                        ID = itemData.itemDB_Food_Medicine[i].ID;
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
                        ID = itemData.itemDB_Material[i].ID;
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
                        ID = itemData.itemDB_Object[i].ID;
                        Fatigue = itemData.itemDB_Object[i].Fatigue;
                        Capacity = itemData.itemDB_Object[i].Capacity;
                        Charge_Space = itemData.itemDB_Object[i].Charge_Space;
                        DataLoading = false;
                    }
                }
                yield return null;
            }
            for(int i = 0; i < itemData.itemDB_Craft.Length; i++)
            {
                if (itemData.itemDB_Craft[i].ID == ID)
                {
                    item_Craft[0].ID = itemData.itemDB_Craft[i].ID;
                    item_Craft[0].name = itemData.itemDB_Craft[i].name;
                    item_Craft[0].Necessary_Material_ID1 = itemData.itemDB_Craft[i].Necessary_Material_ID1;
                    item_Craft[0].Amount_Of_Material1 = itemData.itemDB_Craft[i].Amount_Of_Material1;
                    item_Craft[0].Necessary_Material_ID2 = itemData.itemDB_Craft[i].Necessary_Material_ID2;
                    item_Craft[0].Amount_Of_Material2 = itemData.itemDB_Craft[i].Amount_Of_Material2;
                    item_Craft[0].Necessary_Material_ID3 = itemData.itemDB_Craft[i].Necessary_Material_ID3;
                    item_Craft[0].Amount_Of_Material3 = itemData.itemDB_Craft[i].Amount_Of_Material3;
                    item_Craft[0].Necessary_Object_ID = itemData.itemDB_Craft[i].Necessary_Object_ID;
                    DataLoading = false;
                }
            }
            yield return null;
        }
        yield return null;
    }

   
}
