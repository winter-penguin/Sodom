using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

[System.Serializable]
public struct ItemDB
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float Item_Type, Hunger, Thirst, Heal, Fatigue, Charge_Space;
}

public class DBManager_Item_food_medicien: MonoBehaviour
{
    [SerializeField]
    public ItemDB[] itemDB;
    public string[] Item;

    IEnumerator Start()
    {
        string url = "http://220.127.167.244:8080/summerproject_2021/ItemData_FoodMedicien.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if(www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }
        string itemDataString = www.downloadHandler.text;
        Item = itemDataString.Split(';');

        for (int i = 0; i < itemDB.Length; i++)
        {
            itemDB[i].ID = Convert.ToInt32(GetDataValue(Item[i], "ID:"));
            itemDB[i].name = GetDataValue(Item[i], "Name:");
            itemDB[i].Item_Type = Convert.ToSingle(GetDataValue(Item[i], "Item_Type:"));
            itemDB[i].Hunger = Convert.ToSingle(GetDataValue(Item[i], "Hunger:"));
            itemDB[i].Thirst = Convert.ToSingle(GetDataValue(Item[i], "Thirst:"));
            itemDB[i].Heal = Convert.ToSingle(GetDataValue(Item[i], "Heal:"));
            itemDB[i].Fatigue = Convert.ToSingle(GetDataValue(Item[i], "Fatigue:"));
            itemDB[i].Charge_Space = Convert.ToSingle(GetDataValue(Item[i], "Charge_Space:"));
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