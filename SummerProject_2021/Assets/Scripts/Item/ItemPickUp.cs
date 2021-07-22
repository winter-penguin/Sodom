using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemPickUp : MonoBehaviour
{
    private Inventory Food;
    private Inventory ToolWeapon;
    private Inventory Ingredient;
    private Inventory Medicine;
    private Inventory Product;
    private Item_Base.ItemType itemType;
    void Update()
    {
        Food = GameObject.Find("Food Grid Setting").GetComponent<Inventory>();
        ToolWeapon = GameObject.Find("Tool Weapon Grid Setting").GetComponent<Inventory>();
        Ingredient = GameObject.Find("Ingredient Grid Setting").GetComponent<Inventory>();
        Medicine = GameObject.Find("Medicine Grid Setting").GetComponent<Inventory>();
        Product = GameObject.Find("Product Grid Setting").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Item_Base mItem = other.transform.GetComponent<Item_Base>();
            Debug.Log(mItem.CurrentItem);
            itemType = mItem.CurrentitemType;
            
            if (itemType == Item_Base.ItemType.Food)
            {
                Food.AcquireItem(mItem);
            }
            else if (itemType == Item_Base.ItemType.Weapon || itemType == Item_Base.ItemType.Tool)
            {
                ToolWeapon.AcquireItem(mItem);
            }
            else if (itemType == Item_Base.ItemType.Ingredient)
            {
                Ingredient.AcquireItem(mItem);
            }
            else if (itemType == Item_Base.ItemType.Medicine)
            {
                Medicine.AcquireItem(mItem);
            }
            else if (itemType == Item_Base.ItemType.Product)
            {
                Product.AcquireItem(mItem);
            }
            other.gameObject.SetActive(false);
        }
    }
}
