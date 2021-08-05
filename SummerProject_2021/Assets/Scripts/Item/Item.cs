using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region DB
    [SerializeField]
    public Craft[] itemCraft;

    public enum ItemType { Food, Tool, Weapon, Ingredient, Medicine, Product };
    public enum ItemName
    {
        Brown_Water, Water, Raw_Meat, Vegetable, Vegetable_Soup, Fried_Meat, Bandage, Pill,
        Crowbar, Shovel, Dagger, Sword,
        Cloth, Wood, Stone, Iron, Bonfire, Bed, Bag, Water_Purifier, Box
    };

    public ItemName CurrentItem = ItemName.Cloth;
    public ItemType CurrentItemType = ItemType.Food;
    public float ID, Item_Type, Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
    public Sprite itemImage;
    public GameObject itemPrefab;
    private DBManagerItem itemData;
    // Start is called before the first frame update
    public bool useItem = false;
    public bool ItemEquip = false;
    #endregion

    private DB_Character db_character;
    private CharacterValue playerValue;
    private float maxHealth, maxHunger, maxThirst, maxFatigue;
    void Start()
    {
        playerValue = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterValue>();
        db_character = GameObject.Find("DBManager").GetComponent<DB_Character>();
        itemData = GameObject.Find("DBManager").GetComponent<DBManagerItem>();
    }
    void Update()
    {
        if (useItem)
        {
            StartCoroutine(CheckUseItem());
        }
        if (itemData.DataLoading)
        {
            StartCoroutine(DataSet());
        }
    }
    protected IEnumerator DataSet()
    {
        while (itemData.DataLoading)
        {
            for (int i = 0; i < itemData.itemDB.Length; i++)
            {
                if (CurrentItem.ToString() == itemData.itemDB[i].name)
                {
                    ID = itemData.itemDB[i].ID;
                    Item_Type = itemData.itemDB[i].Item_Type;
                    Hunger = itemData.itemDB[i].Hunger;
                    Thirst = itemData.itemDB[i].Thirst;
                    Heal = itemData.itemDB[i].Heal;
                    Fatigue = itemData.itemDB[i].Fatigue;
                    AD = itemData.itemDB[i].AD;
                    Attack_Range = itemData.itemDB[i].Attack_Range;
                    Capacity = itemData.itemDB[i].Capacity;
                    Charge_Space = itemData.itemDB[i].Charge_Space;
                }
            }

            for (int i = 0; i < itemData.itemDBCraft.Length; i++)
            {
                if (itemData.itemDBCraft[i].ID == ID)
                {
                    itemCraft[0] = itemData.itemDBCraft[i];
                    break;
                }
            }
            itemData.DataLoading = false;
            yield return null;

        }
        yield return null;
    }
    IEnumerator CheckUseItem()
    {
        maxHealth = db_character.characterDB[0].health;
        maxHunger = db_character.characterDB[0].hunger;
        maxThirst = db_character.characterDB[0].thirst;
        maxFatigue = db_character.characterDB[0].fatigue;
        if (CurrentItemType == ItemType.Food || CurrentItemType == ItemType.Medicine)
        {
            playerValue.HpChanged(Heal);
            playerValue.HungerChanged(Hunger);
            playerValue.ThirstChanged(Thirst);
            playerValue.FatigueChanged(Fatigue);
        }
        else if (CurrentItemType == ItemType.Weapon || CurrentItemType == ItemType.Tool)
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
        yield return null;
    }
}
