using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public int LeftOrRight;
    public Image _image;
    ClickMovement clickmovement;
    public bool iscollide;
    void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        iscollide = true;
        clickmovement = collision.gameObject.GetComponent<ClickMovement>();
        clickmovement.isNormalMoving = false;
        clickmovement.isFirst_ing = false;
        clickmovement.anim.SetBool("isWalking", false);
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        clickmovement = collision.gameObject.GetComponent<ClickMovement>(); //물어볼거
        if (LeftOrRight == -1)
        {
            
        }
        clickmovement.anim.SetBool("isWalking", false);
    }

    public void RemoveDoor()
    {
        //this.gameObject.SetActive(false);
        //_image.sprite = Resources.Load<Sprite>("Sprites/House/door_side_open_sample") as Sprite;
    }
}
