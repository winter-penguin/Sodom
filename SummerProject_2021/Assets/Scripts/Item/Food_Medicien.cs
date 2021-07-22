using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Medicien : MonoBehaviour
{
    public ItemName CurrentItem = ItemName.Brown_Water;
    public enum ItemName { Brown_Water, Water, Raw_Meat, Vegetable, Vegetable_Soup, Fried_Meat, Bandage, Pill };
    [SerializeField] protected float Hunger, Thirst, Heal, Fatigue, Charge_Space;
    private DBManager_Item_food_medicien itemData;


    private bool useItem = false;
    private void Start()
    {
        itemData = GameObject.Find("ItemDB").GetComponent<DBManager_Item_food_medicien>();
        StartCoroutine(DataSet());
    }
    void Update()
    {
        if(useItem)
        {
            UseItem();
        }
    }

    protected IEnumerator DataSet()
    {
        bool DataLoading = true;
        while(DataLoading)
        {
            for(int i = 0; i < itemData.itemDB.Length; i++)
            {
                if(itemData.itemDB[i].name == CurrentItem.ToString())
                {
                    Hunger = itemData.itemDB[i].Hunger;
                    Thirst = itemData.itemDB[i].Thirst;
                    Heal = itemData.itemDB[i].Heal;
                    Fatigue = itemData.itemDB[i].Fatigue;
                    Charge_Space = itemData.itemDB[i].Charge_Space;

                    DataLoading = false;
                }
            }
            yield return null;
        }
        yield return null;
    }

    void UseItem()
    {

    }
}
