//*******************************
// Editor:CHG
// LAST EDITED DATE : 2021.07.19
// Script Purpose : Character_ClickMove
//*******************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [System.Serializable] // class를 인스펙터에서 보여줌
    //[SerializeField] // private 변수를 인스펙터에서 보여줌
    public class Floor
    {
        public string name;
        public Transform enemy;
    }
    public Floor[] floor;
    private Camera camera1;

    private bool isClick;
    private bool isMoving;
    
    private Vector2 destination;
    public float speed = 5;

    private Rigidbody2D rb;

    
    Vector2 MousePosition;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    // whatever tag you are looking for on your game object
                    if (hit.collider.tag == "Trigger")
                    {
                        Debug.Log("---> Hit: ");
                    }
                }
            }

                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            destination = new Vector2(MousePosition.x, transform.position.y);
            //Debug.Log(destination);
            
            isClick = true; //애니메이션 할때 필요함
        }
        if (isClick && transform.position.x != destination.x)
        {
            isMoving = true;
            isClick = false;
            //transform.Translate(new Vector2(MousePosition.x - transform.position.x, 0)* Time.deltaTime * speed);
            
        }
        else
        {
            isClick = false;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            //transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            //transform.position += (Vector3.right)*speed*Time.deltaTime; // 여기선 무조건 Vector3를 써야한다고함, 이거하면 계속 한쪽으로만 감
            //rb.velocity = new Vector2(speed, 0); //질량을 고려하지 않고 움직임, y로 움직이지 않을 때 용이
            //rb.AddForce(new Vector2(-speed, 0)); //질량을 고려하여 움직임
            ///*
            if (isMoving == true && transform.position.x > destination.x)
            {
                rb.velocity = new Vector2(-speed, 0);//velocity를 사용하면 목적지에 다다랐을때 떨림
                if (transform.position.x < destination.x)
                {
                    isMoving = false;
                }
            }
            else if(isMoving == true && transform.position.x <= destination.x)
            {
                rb.velocity = new Vector2(speed, 0);
                if(transform.position.x > destination.x)
                {
                    isMoving = false;
                }
            }
            //*/
        }
        
        if(isMoving == false)
        {
            rb.AddForce(new Vector2(0, 0));
            
        }
        
            //transform.position = new Vector2(0, 0);
    }
}
