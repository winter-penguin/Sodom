using UnityEngine;

public class Item : MonoBehaviour
{
    #region DB
    [SerializeField]
    public Craft[] itemCraft;

    public enum ItemTypeEnum { Material, Food, Medicine, Tool, Weapon, Product };
    public enum ItemName
    {
        Wood, Stone, Cloth, Iron,
        Brown_Water, Water, Raw_Meat, Fried_Meat, Vegetable, Vegetable_Soup, Bandage, Pill,
        Crowbar, Shovel, Dagger, Sword,
        Bonfire, Bed, Bag, Water_Purifier, Box
    };

    public ItemName CurrentItem = ItemName.Wood;
    public ItemTypeEnum CurrentItemType = ItemTypeEnum.Material;
    public int ID, ItemType, Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space, Value;
    public Sprite itemImage;
    private DBManagerItem itemData;
    // Start is called before the first frame update
    public bool useItem = false;
    public bool ItemEquip = false;
    #endregion
    public int ItemCount;
    private SurvivalGauge survivalGauge;
    private CharacterValue playerValue;

    bool itemDBloading = false;
    bool craftloading = false;
    void Start()
    {
        survivalGauge = GameObject.FindGameObjectWithTag("Player").GetComponent<SurvivalGauge>();
        playerValue = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterValue>();
        itemData = GameObject.Find("DBManager").GetComponent<DBManagerItem>();
    }
    void Update()
    {
        if (useItem)
        {
            CheckUseItem();
        }
        if(itemData.DataLoading && !itemDBloading && !craftloading)
        {
            DataSet();
        }
    }
    private void DataSet()
    {
        for (int i = 0; i < itemData.itemDB.Length; i++)
        {
            if (CurrentItem.ToString() == itemData.itemDB[i].name)
            {
                ID = itemData.itemDB[i].ID;
                ItemType = itemData.itemDB[i].Item_Type;
                Hunger = itemData.itemDB[i].Hunger;
                Thirst = itemData.itemDB[i].Thirst;
                Heal = itemData.itemDB[i].Heal;
                Fatigue = itemData.itemDB[i].Fatigue;
                AD = itemData.itemDB[i].AD;
                Attack_Range = itemData.itemDB[i].Attack_Range;
                Capacity = itemData.itemDB[i].Capacity;
                Charge_Space = itemData.itemDB[i].Charge_Space;
                Value = itemData.itemDB[i].Value;
                itemDBloading = true;
                break;
            }
        }
        for (int i = 0; i < itemData.itemDBCraft.Length; i++)
        {
            if (itemData.itemDBCraft[i].ID == ID)
            {
                itemCraft[0] = itemData.itemDBCraft[i];
                craftloading = true;
                break;
            }
        }
    }
    private void CheckUseItem()
    {
        if (ItemType == 0 || ItemType == 4)
        {
            playerValue.HpChanged(Heal);
            survivalGauge.HungerMinus(Hunger);
            survivalGauge.ThirstMinus(Thirst);
            survivalGauge.FatiguePlus(Fatigue);
        }
        else if (ItemType == 1 || ItemType == 2)
        {
            if (ItemEquip)
            {
                playerValue.Atk_PowChanged(-AD);
                playerValue.Atk_RangeChanged(-Attack_Range);
                ItemEquip = false;
                Debug.Log("아이템 장착해제");
            }
            else if (ItemEquip == false)
            {
                playerValue.Atk_PowChanged(AD);
                playerValue.Atk_RangeChanged(Attack_Range);
                ItemEquip = true;
                Debug.Log("아이템 장착");
            }
        }
        useItem = false;
    }
}
