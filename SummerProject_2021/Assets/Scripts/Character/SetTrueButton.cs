using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrueButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Button;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Button.SetActive(false);
        }
    }
}
