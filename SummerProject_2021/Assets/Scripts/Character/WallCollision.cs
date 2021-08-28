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

    public GameObject DoorPoint;
    void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)//얘는 가만히있는거고 캐릭터는 앞으로 부딪힐 콜라이더가 많으니깐 가만히있는 물체가 캐릭터 움직임을 멈추는게 좋지 않을까?
    {
        if (collision.transform.position.x > transform.position.x)
        {
            LeftOrRight = 1;
        }
        else
        {
            LeftOrRight = -1;
        }
        iscollide = true;
        clickmovement = collision.gameObject.GetComponent<ClickMovement>();
        clickmovement.isNormalMoving = false;
        clickmovement.isFirst_ing = false;
        clickmovement.anim.SetBool("isWalking", false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        iscollide = true;
        clickmovement = collision.gameObject.GetComponent<ClickMovement>(); //물어볼거
        if (LeftOrRight == -1)
        {
            
        }
        clickmovement.anim.SetBool("isWalking", false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        iscollide = false;
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
