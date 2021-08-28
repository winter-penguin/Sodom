using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int ProduceItemCount;
    public int ItemCount;
    private int num;
    public Image itemImage;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;
    public GameObject ProduceButton;
    private Slot[] slots;
    public bool isProduce = false;
    private void Start()
    {
        slots = GameObject.Find("ProduceGridSetting").GetComponentsInChildren<Slot>();
    }
    private void OnDisable()
    {
        ClearSlot();
    }
    private void SetColor(Image image, float _alpha) //이미지 알파 조정
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }

    public void AddItem(Item _item, int _count)
    {
        item = _item;
        ProduceItemCount = _count;
        num = _count;
        ItemCount = item.ItemCount;
        itemImage.sprite = item.itemImage;
        go_CountImage.SetActive(true);
        if (this.gameObject.name != "CraftSlot")
        {
            if(ItemCount < ProduceItemCount)
            {
                text_Count.text = "<color=#FF0000>" + ItemCount + "</color>"  + " / " + ProduceItemCount;
                ProduceButton.GetComponent<Button>().enabled = false;
                SetColor(ProduceButton.GetComponent<Image>(), 0.2f);
            }    
            else
            {
                text_Count.text = ItemCount + " / " + ProduceItemCount;
                ProduceButton.GetComponent<Button>().enabled = true;
                SetColor(ProduceButton.GetComponent<Image>(), 1f);
            }
        }
        else
        {
            text_Count.text = ProduceItemCount.ToString();
        }
        SetColor(itemImage,1);
    }

    public void UPDownCount(int _count)
    {
        ProduceItemCount += _count;
        text_Count.text = ProduceItemCount.ToString();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].ProduceItemCount = slots[i].num * ProduceItemCount;
                if (slots[i].ItemCount < slots[i].ProduceItemCount)
                {
                    slots[i].text_Count.text = "<color=#FF0000>" + slots[i].ItemCount + "</color>" + " / " + slots[i].ProduceItemCount;
                    ProduceButton.GetComponent<Button>().enabled = false;
                    SetColor(ProduceButton.GetComponent<Image>(), 0.2f);
                }
                else
                {
                    slots[i].text_Count.text = slots[i].ItemCount + " / " + slots[i].ProduceItemCount;
                    ProduceButton.GetComponent<Button>().enabled = true;
                    SetColor(ProduceButton.GetComponent<Image>(), 1f);
                }

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