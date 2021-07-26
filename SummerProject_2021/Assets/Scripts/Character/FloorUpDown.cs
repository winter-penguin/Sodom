using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorUpDown : MonoBehaviour
{

    ClickMovement clickmovement;
    // Start is called before the first frame update
    void Awake()
    {
        clickmovement = GameObject.Find("Character").GetComponent<ClickMovement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            clickmovement.Move();
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
