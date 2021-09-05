using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int ProduceItemCount;
    [SerializeField]
    private int ItemCount;
    [SerializeField]
    private Text text_Count;
    private int num;
    public Image itemImage;
    [SerializeField]
    private GameObject go_CountImage;
    public GameObject ProduceButton;
    private Slot[] slots;
    private DBManagerItem ItemData;
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

    public void AddItem(int index, int _count)
    {
        ItemData = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
        ProduceItemCount = _count;
        num = _count;
        ItemCount = ItemData.itemDB[index].ItemCount;
        itemImage.sprite = ItemData.itemDB[index].ItemImage;
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
            if (slots[i].ProduceItemCount != 0)
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
        ProduceItemCount = 0;
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}