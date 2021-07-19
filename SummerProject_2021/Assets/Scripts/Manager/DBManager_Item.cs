using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

[System.Serializable]
public struct ItemDB
{
    public enum ItemType { Food, Tool, Weapon, Ingredient, Medicine, Product};

    [SerializeField]
    public ItemType itemType;

    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float Item_Type, AD, Attack_Range, Hunger, Thirst, Heal, Fatigue;
}

public class DBManager_Item : MonoBehaviour
{
    [ArrayElementTitle("ItemType")]
    [SerializeField]
    public ItemDB[] itemDB;
    public string[] Item;

    IEnumerator Start()
    {
        WWW itemData = new WWW("http://220.127.167.244:8080/summerproject_2021/ItemData.php");
        yield return itemData;
        string itemDataString = itemData.text;
        Item = itemDataString.Split(';');

        for (int i = 0; i < itemDB.Length; i++)
        {
            itemDB[i].ID = Convert.ToInt32(GetDataValue(Item[i], "ID:"));
            itemDB[i].name = GetDataValue(Item[i], "Name:");
            itemDB[i].Item_Type = Convert.ToSingle(GetDataValue(Item[i], "Item_Type:"));
            itemDB[i].AD = Convert.ToSingle(GetDataValue(Item[i], "AD:"));
            itemDB[i].Attack_Range = Convert.ToSingle(GetDataValue(Item[i], "Attack_Range:"));
            itemDB[i].Hunger = Convert.ToSingle(GetDataValue(Item[i], "Hunger:"));
            itemDB[i].Thirst = Convert.ToSingle(GetDataValue(Item[i], "Thirst:"));
            itemDB[i].Heal = Convert.ToSingle(GetDataValue(Item[i], "Heal:"));
            itemDB[i].Fatigue = Convert.ToSingle(GetDataValue(Item[i], "Fatigue:"));
        }

    }

    // Start is called before the first frame update

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

}