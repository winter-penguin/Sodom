using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemImage
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public Sprite itemImage;
}
public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    public ItemImage[] itemImage;
    private Image CurrentImage;

    private void Start()
    {
        CurrentImage = this.gameObject.GetComponent<Image>();
    }
    public void SetImage(int ID)
    {
        int index = ID - 14;
        CurrentImage.sprite = itemImage[index].itemImage;
        SetColor(CurrentImage, 1);
    }

    public void DeleteImage()
    {
        SetColor(CurrentImage, 0);
    }
    private void SetColor(Image image, float _alpha) //이미지 알파 조정
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }
}
