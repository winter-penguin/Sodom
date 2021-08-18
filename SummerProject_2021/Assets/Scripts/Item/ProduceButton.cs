using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceButton : MonoBehaviour
{
    public ProductItem product;
    public void OnClickProduceButton()
    {
        product.Product = true;
    }

    private GameObject ProduceUI;
    public void OnClickCancelButton()
    {
        ProduceUI = GameObject.Find("ProduceUI");
        ProduceUI.SetActive(false);
    }


}
