using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    ClickMovement clickmovement;
    public int Case;
    private bool iscollide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Case == 1)
        {
            clickmovement = collision.gameObject.GetComponent<ClickMovement>();
            clickmovement.isNormalMoving = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Case == 2)
        {
            iscollide = true;
            clickmovement = collision.gameObject.GetComponent<ClickMovement>();
            clickmovement.speed = 100;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Case == 2)
        {
            iscollide = false;
            clickmovement = collision.gameObject.GetComponent<ClickMovement>();
            clickmovement.speed = 300;
        }
    }
}
