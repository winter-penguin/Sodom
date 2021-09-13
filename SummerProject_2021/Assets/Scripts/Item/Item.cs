using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    #region DB
    [SerializeField]
    public Craft[] itemCraft;
    public enum ItemName
    {
        Wood, Stone, Cloth, Iron,
        Brown_Water, Water, Raw_Meat, Fried_Meat, Vegetable, Vegetable_Soup, Rotten_Food, Bandage, Pill,
        Shovel, Crowbar, Dagger, Sword,
        Bag, Water_Purifier, Bonfire, Box, Bed
    };
    public ItemName CurrentItem = ItemName.Wood;
    public int ID, ItemType, CurrentCapacity;
    private int index;
    private DBManagerItem itemData;
    public bool useItem = false;
    public bool ItemEquip = false;
    #endregion
    private SurvivalGauge survivalGauge;
    private CharacterValue playerValue;
    private ItemSlot itemslot;

    private bool itemDBloading = false;
    private bool craftloading = false;

    void Start()
    {
        survivalGauge = GameObject.FindGameObjectWithTag("Player").GetComponent<SurvivalGauge>();
        playerValue = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterValue>();
        itemData = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
        itemslot = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();

    }
    void Update()
    {
        if (useItem)
        {
            CheckUseItem();
        }
        if (itemData.DataLoading && !itemDBloading && !craftloading)
        {
            DataSet();
        }
    }
    protected void DataSet()
    {
        for (int i = 0; i < itemData.itemDB.Length; i++)
        {
            if (CurrentItem.ToString() == itemData.itemDB[i].name)
            {
                ID = itemData.itemDB[i].ID;
                ItemType = itemData.itemDB[i].Item_Type;
                index = ID - 1;
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
        SetImage(index);
        if(this.gameObject.GetComponent<ItemText>() != null)
        {
            this.gameObject.GetComponent<ItemText>().SetText();
        }
    }
    public void SetImage(int num)
    {
        if(this.gameObject.GetComponent<Image>() != null)
        {
            if(itemData.itemDB[index].ItemCount == 0)
            {
                SetColor(this.gameObject.GetComponent<Image>(), 0.3f, 0.3f);
            }
            else
            {
                SetColor(this.gameObject.GetComponent<Image>(), 1f ,1f);
            }
        }
    }
    private void SetColor(Image image, float _alpha, float _color) //이미지 알파 조정
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
        image.color = new Color(_color, _color, _color);
    }
    private void CheckUseItem()
    {
        if (ItemType == 1 || ItemType == 2 || ItemType == 3)
        {
            playerValue.HpChanged(itemData.itemDB[index].Heal);
            survivalGauge.HungerMinus(itemData.itemDB[index].Hunger);
            survivalGauge.ThirstMinus(itemData.itemDB[index].Thirst);
            survivalGauge.FatiguePlus(itemData.itemDB[index].Fatigue);
        }
        else if (ItemType == 4 || ItemType == 5)
        {
            if (ItemEquip)
            {
                playerValue.Atk_PowChanged(-itemData.itemDB[index].AD);
                playerValue.Atk_RangeChanged(-itemData.itemDB[index].Attack_Range);
                ItemEquip = false;
                itemslot.DeleteImage();
            }
            else if (ItemEquip == false)
            {
                playerValue.Atk_PowChanged(itemData.itemDB[index].AD);
                playerValue.Atk_RangeChanged(itemData.itemDB[index].Attack_Range);
                ItemEquip = true;
                itemslot.SetImage(ID);
            }
        }
        useItem = false;
    }
}
