using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private void OnDisable()
    {
        ClearSlot();
    }
    private void SetColor(float _alpha) //이미지 알파 조정
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        go_CountImage.SetActive(true);
        text_Count.text = itemCount.ToString();
        SetColor(1);
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}