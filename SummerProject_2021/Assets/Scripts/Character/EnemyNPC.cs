using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{
    ClickMovement clickmovement;
    public bool iscollide = false;
    public int EnemyHP = 100;
    // Start is called before the first frame update
    void Start()
    {
        clickmovement = GameObject.Find("CHG_Character").GetComponent<ClickMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (clickmovement.isClickEnemy == true)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        */
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        clickmovement = collision.gameObject.GetComponent<ClickMovement>();
        if (clickmovement.isClickEnemy)
        {
            clickmovement.isNormalMoving = false;
            clickmovement.anim.SetBool("isWalking", false);
            clickmovement.anim.SetBool("isPunching", true);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        clickmovement = collision.gameObject.GetComponent<ClickMovement>();
        if(clickmovement.isClickEnemy == true)
        {
            iscollide = true;
            clickmovement.anim.SetBool("isPunching", true);
            if(EnemyHP == 80)
            {
                clickmovement.anim.SetBool("isPunching", false);
            }
        }
        
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        iscollide = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            iscollide = true;
            clickmovement = other.GetComponent<ClickMovement>();
            if (clickmovement.isClickEnemy)
            {
                clickmovement.isNormalMoving = false;
                clickmovement.anim.SetBool("isWalking", false);
                clickmovement.anim.SetBool("isPunching", true);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (clickmovement.isClickEnemy)
            {
                clickmovement.isNormalMoving = false;
                clickmovement.anim.SetBool("isWalking", false);
                clickmovement.anim.SetBool("isPunching", true);
            }
        }
        
    }
}
