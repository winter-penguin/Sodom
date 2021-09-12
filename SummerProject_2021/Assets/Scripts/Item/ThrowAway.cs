using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAway : MonoBehaviour
{
    private GameObject[] CountButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*    public void ThrowAwayButton()
    {
        CountButton = GameObject.FindGameObjectsWithTag("Count");
        CountButton[].SetActive(true);
    }*/
    public void CancelButton()
    {
        //CountButton.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
