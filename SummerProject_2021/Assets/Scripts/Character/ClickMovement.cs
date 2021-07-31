//*******************************
// Editor:CHG
// LAST EDITED DATE : 2021.07.19
// Script Purpose : Character_ClickMove
//*******************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [System.Serializable] // class를 인스펙터에서 보여줌
    //[SerializeField] // private 변수를 인스펙터에서 보여줌
    public class Floor
    {
        public Transform up;
        public Transform down;
        public GameObject floor;
    }
    public Floor[] floor;
    public float speed = 5;
    public float stairspeed;

    private Vector2 destination;
    private Vector2 first_destination;
    private Camera camera1;
    private Rigidbody2D rb;
    private Transform stairstart;
    private Transform stairend;

    private bool isClick;
    private bool isMoving = false;
    private bool isFirst = false;
    private bool isSecond = false;
    private bool isFirst_ing = false;
    private bool isSecond_ing = false;
    public bool isSecond_ing_click = false;

    public int MovingCase;
    public int Wherecharacteris;
    public int Wheretogo;


    Vector2 MousePosition;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isSecond_ing == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//항상 제일 먼저
                WhereToGo();
                Debug.Log("Wheretogo: " + Wheretogo);

                if (Wheretogo != 0)
                {
                    WhereCharacter();
                    //Debug.Log("Whereis: " + Wherecharacteris);
                    CaseSetting();
                }
                //Debug.Log(MousePosition);
                //isClick = true; //애니메이션 할때 필요함
            }
            else if (isSecond_ing_click == true)
            {
                WhereCharacter();
                //Debug.Log("Whereis: " + Wherecharacteris);
                CaseSetting();
                isSecond_ing_click = false;
            }
        }
        if (isSecond_ing == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                WhereToGo();
                //Debug.Log("Wheretogo: " + Wheretogo);
                if (Wheretogo != 0)
                {
                    isSecond_ing_click = true;
                    
                    
                }
            }
        }
    }
    //계단 올라가는중 => 클릭 => 일단 위치 저장 => 계단 이동이 끝나면 바로 이 저장한 위치로 이동해야함
    
    private void FixedUpdate()
    {
        if(isSecond_ing == false)
        {

        }
        switch (MovingCase)
        {
            case 1:
                NormalMove();
                break;
            case 2:
                if (isFirst == false)
                {
                    firstmove();
                    if (Math.Abs(transform.position.x - stairstart.position.x) < 1f)
                    {
                        isFirst = true;
                        isFirst_ing = false;
                    }
                }
                else if (isFirst == true && isSecond == false)
                {
                    rb.isKinematic = true;
                    secondmove();
                    if (Math.Abs(transform.position.y - stairend.position.y) < 0.01f)
                    {
                        rb.isKinematic = false;
                        isSecond = true;
                        isSecond_ing = false;
                        if(Wheretogo == 2)
                            floor[Wheretogo - 1].floor.gameObject.SetActive(true);
                        if(isSecond_ing_click == true)
                        {
                            break;
                        }
                    }
                }
                else if(isSecond == true)
                {
                    NormalMove();
                }
                break;
            case 3:
                if (isFirst == false && isSecond_ing == false)
                {
                    firstmove();
                    if (Math.Abs(transform.position.x - stairstart.position.x) < 1f)
                    {
                        isFirst = true;
                        isFirst_ing = false;
                    }
                }
                else if (isFirst == true && isSecond == false)
                {
                    rb.isKinematic = true;
                    secondmove();
                    if (Math.Abs(transform.position.y - stairend.position.y) < 0.01f)
                    {
                        rb.isKinematic = false;
                        isSecond = true;
                        isSecond_ing = false;
                        if (Wheretogo == 2)
                            floor[Wherecharacteris - 1].floor.gameObject.SetActive(true);
                        if (isSecond_ing_click == true)
                        {
                            break;
                        }
                    }
                }
                else if (isSecond == true)
                {
                    NormalMove();
                }
                break;


                
        }
        if (Wheretogo != 0)
        {
            
        }
        
        if (isClick == true)
        {
            
        }
    }
    void firstmove()
    {
        isMoving = true;
        isFirst_ing = true;
        //first_destination = new Vector2(stairstart.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, stairstart.position, Time.deltaTime * speed);
    }
    public void secondmove()
    {
        isSecond_ing = true;
        //transform.position = Vector2.MoveTowards(stairstart.position, stairend.position, Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(transform.position, stairend.position, Time.deltaTime * stairspeed);
        
    }
    public void NormalMove()
    {
        destination = new Vector2(MousePosition.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        isMoving = true;
        if (Math.Abs(transform.position.x - destination.x) < 0.01f)
        {
            isMoving = false;
            isClick = false;
        }
    }
    public void Move()
    {
        switch (MovingCase)
        {
            case 1:
                transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                if (Math.Abs(transform.position.x - destination.x) < 0.01f)
                {
                    isMoving = false;
                    isClick = false;
                }
                break;
            case 2:
                destination = new Vector2(MousePosition.x, transform.position.y); //계단 다 올라간뒤의 y좌표로 다시 목표설정
                transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * speed);
                if (Math.Abs(transform.position.x - destination.x) < 0.01f)
                {
                    isMoving = false;
                    isClick = false;
                }
                break;


        }
    }
    
    void StairMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, stairend.transform.position, Time.deltaTime * stairspeed);
    }
    void WhereToGo()
    {
        if (MousePosition.y > -390 && MousePosition.y < -25)
        {
            Wheretogo = 1;//1층에 갈것
        }
        else if (MousePosition.y > 25 && MousePosition.y < 390)
        {
            Wheretogo = 2;//2층에 갈것
        }
        else
            Wheretogo = 0; //집 외에 다른곳 선택
    }
    private void WhereCharacter()
    {
        if (transform.position.y > -290 && transform.position.y < -125)
        {
            Wherecharacteris = 1;//캐릭터가 1층에 있음
        }
        if (transform.position.y > 124 && transform.position.y < 290)
        {
            Wherecharacteris = 2;//캐릭터가 2층에 있음
        }
        
    }
    private void CaseSetting()
    {
        if(Wheretogo == Wherecharacteris)//같은 층내에서 움직이기
        {
            MovingCase = 1; 
        }
        if(Wheretogo > Wherecharacteris)//올라가기
        {
            isFirst = false;
            isSecond = false;
            MovingCase = 2; 
            stairstart = floor[Wherecharacteris - 1].down;
            stairend = floor[Wherecharacteris - 1].up;
            floor[Wheretogo - 1].floor.gameObject.SetActive(false);
        }
        if (Wheretogo < Wherecharacteris)//내려가기(3층이상 되면 달라져야함)
        {
            isFirst = false;
            isSecond = false;
            MovingCase = 3;
            stairstart = floor[Wherecharacteris - 2].up;
            stairend = floor[Wherecharacteris - 2].down;
            floor[Wherecharacteris - 1].floor.gameObject.SetActive(false);
        }
        /*
        if(isSecond_ing_click == true)//계단 올라가거나 내려가던 중에 클릭됐을때
        {
            MovingCase = 4;
        }
        */
    }
    private void GoUp()
    {

    }
    private void GoDown()
    {

    }

}
