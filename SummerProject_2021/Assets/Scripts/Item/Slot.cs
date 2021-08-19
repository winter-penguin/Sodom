using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int ProduceItemCount;
    public int ItemCount;
    public Image itemImage;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;
    private Slot[] slots;
    private int num;
    private void Start()
    {
        slots = GameObject.Find("ProduceGridSetting").GetComponentsInChildren<Slot>();
    }
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
        ProduceItemCount = _count;
        num = _count;
        ItemCount = item.ItemCount;
        itemImage.sprite = item.itemImage;
        go_CountImage.SetActive(true);
        text_Count.text = ProduceItemCount.ToString();
        SetColor(1);
    }

    public void UPDownCount(int _count)
    {
        ProduceItemCount += _count;
        text_Count.text = ProduceItemCount.ToString();
        for(int i = 0 ; i < slots.Length ; i++)
        {
            if(slots[i].item != null)
            {
                slots[i].ProduceItemCount = slots[i].num * ProduceItemCount;
                slots[i].text_Count.text = slots[i].ProduceItemCount.ToString();
            }
        }
    }

    private void ClearSlot()
    {
        item = null;
        ProduceItemCount = 0;
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}