using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

[System.Serializable]
public struct ItemDB_Food_Medicine
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float Item_Type, Hunger, Thirst, Heal, Fatigue, Charge_Space;
}
[System.Serializable]
public struct ItemDB_Material
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float Item_Type, AD, Attack_Range, Charge_Space;
}
[System.Serializable]
public struct ItemDB_Obejct
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float Item_Type, Fatigue, Capacity, Charge_Space;
}

public class DBManager_Item: DBManager
{
    [SerializeField]
    public ItemDB_Food_Medicine[] itemDB_Food_Medicine;
    public string[] Item_Food_Medicine;

    [SerializeField]
    public ItemDB_Material[] itemDB_Material;
    public string[] Item_Material;

    [SerializeField]
    public ItemDB_Obejct[] itemDB_Object;
    public string[] Item_Object;
    void Start()
    {
        StartCoroutine(ConnectItemDB());
    }
    private IEnumerator ConnectItemDB()
    {
        Init(_phpFile : "ItemData_FoodMedicine.php");
        yield return StartCoroutine(routine: ConnectDB());
        Item_Food_Medicine = queryResult.Split(';');

        for (int i = 0; i < itemDB_Food_Medicine.Length; i++)
        {
            itemDB_Food_Medicine[i].ID = Convert.ToInt32(GetDataValue(Item_Food_Medicine[i], "ID:", "|"));
            itemDB_Food_Medicine[i].name = GetDataValue(Item_Food_Medicine[i], "Name:", "|");
            itemDB_Food_Medicine[i].Item_Type = Convert.ToSingle(GetDataValue(Item_Food_Medicine[i], "Item_Type:", "|"));
            itemDB_Food_Medicine[i].Hunger = Convert.ToSingle(GetDataValue(Item_Food_Medicine[i], "Hunger:", "|"));
            itemDB_Food_Medicine[i].Thirst = Convert.ToSingle(GetDataValue(Item_Food_Medicine[i], "Thirst:", "|"));
            itemDB_Food_Medicine[i].Heal = Convert.ToSingle(GetDataValue(Item_Food_Medicine[i], "Heal:", "|"));
            itemDB_Food_Medicine[i].Fatigue = Convert.ToSingle(GetDataValue(Item_Food_Medicine[i], "Fatigue:", "|"));
            itemDB_Food_Medicine[i].Charge_Space = Convert.ToSingle(GetDataValue(Item_Food_Medicine[i], "Charge_Space:", "|"));
        }

        Init(_phpFile: "ItemData_Material.php");
        yield return StartCoroutine(routine: ConnectDB());
        Item_Material = queryResult.Split(';');

        for (int i = 0; i < itemDB_Material.Length; i++)
        {
            itemDB_Material[i].ID = Convert.ToInt32(GetDataValue(Item_Material[i], "ID:", "|"));
            itemDB_Material[i].name = GetDataValue(Item_Material[i], "Name:", "|");
            itemDB_Material[i].Item_Type = Convert.ToSingle(GetDataValue(Item_Material[i], "Item_Type:", "|"));
            itemDB_Material[i].AD = Convert.ToSingle(GetDataValue(Item_Material[i], "AD:", "|"));
            itemDB_Material[i].Attack_Range = Convert.ToSingle(GetDataValue(Item_Material[i], "Attack_Range:", "|"));
            itemDB_Material[i].Charge_Space = Convert.ToSingle(GetDataValue(Item_Material[i], "Charge_Space:", "|"));
        }

        Init(_phpFile: "ItemData_Object.php");
        yield return StartCoroutine(routine: ConnectDB());
        Item_Object = queryResult.Split(';');

        for (int i = 0; i < itemDB_Object.Length; i++)
        {
            itemDB_Object[i].ID = Convert.ToInt32(GetDataValue(Item_Object[i], "ID:", "|"));
            itemDB_Object[i].name = GetDataValue(Item_Object[i], "Name:", "|");
            itemDB_Object[i].Item_Type = Convert.ToSingle(GetDataValue(Item_Object[i], "Item_Type:", "|"));
            if (GetDataValue(Item_Object[i], "Fatigue:", "|") != "")
            {
                itemDB_Object[i].Fatigue = Convert.ToSingle(GetDataValue(Item_Object[i], "Fatigue:", "|"));
            }
            if (GetDataValue(Item_Object[i], "Capacity:", "|") != "")
            {
                itemDB_Object[i].Capacity = Convert.ToSingle(GetDataValue(Item_Object[i], "Capacity:", "|"));
            }
            itemDB_Object[i].Charge_Space = Convert.ToSingle(GetDataValue(Item_Object[i], "Charge_Space:", "|"));

        }
    }

}