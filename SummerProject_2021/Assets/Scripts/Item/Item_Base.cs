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
    private ItemName rayItemName;
    private ItemType rayItemType;
    [SerializeField] protected float Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
    public Sprite itemImage;
    public GameObject itemPrefab;
    private DBManager_Item itemData;

    private bool useItem = false;
    // Start is called before the first frame update
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
                rayItemType = hit.transform.gameObject.GetComponent<Item_Base>().CurrentItemType;
                rayItemName = hit.transform.gameObject.GetComponent<Item_Base>().CurrentItem;
            }

            if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
            {
                m_IsOneClick = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (rayItemType != ItemType.Ingredient)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (!m_IsOneClick)
                        {
                            m_Timer = Time.time;
                            m_IsOneClick = true;
                        }
                    }

                    else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                    {
                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log("Double Click");
                            m_IsOneClick = false;
                            useItem = true;
                        }
                    }
                }
            }
        }
        if (useItem)
        {
            UseItem();
        }
    }


    void UseItem()
    {

    }
    
}
