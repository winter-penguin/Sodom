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
    private Item.ItemType itemType;
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
            Item mItem = other.transform.GetComponent<ItemType>().item;
            Debug.Log(mItem.itemName);
            itemType = mItem.itemType;
            
            if (itemType == Item.ItemType.Food)
            {
                Food.AcquireItem(mItem);
            }
            else if (itemType == Item.ItemType.Weapon || itemType == Item.ItemType.Tool)
            {
                ToolWeapon.AcquireItem(mItem);
            }
            else if (itemType == Item.ItemType.Ingredient)
            {
                Ingredient.AcquireItem(mItem);
            }
            else if (itemType == Item.ItemType.Medicine)
            {
                Medicine.AcquireItem(mItem);
            }
            else if (itemType == Item.ItemType.Product)
            {
                Product.AcquireItem(mItem);
            }
            other.gameObject.SetActive(false);
        }
    }
}
