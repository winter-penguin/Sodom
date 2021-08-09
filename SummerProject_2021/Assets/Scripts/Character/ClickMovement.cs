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
    EnemyNPC enemynpc;
    [System.Serializable] // classÎ•º Ïù∏Ïä§ÌéôÌÑ∞ÏóêÏÑú Î≥¥Ïó¨Ï§å
    //[SerializeField] // private Î≥ÄÏàòÎ•º Ïù∏Ïä§ÌéôÌÑ∞ÏóêÏÑú Î≥¥Ïó¨Ï§å
    public class Floor
    {
        public Transform up;
        public Transform down;
    }
    public Floor[] floor;
    public Transform pos;
    public Vector2 boxSize;
    public LayerMask EnemyNPCMask;
    public GameObject AttackRange;

    private Vector2 destination;
    private Vector2 first_destination;
    private Vector2 MousePosition;
    private Camera camera1;
    public Rigidbody2D rb;
    private Transform stairstart;
    private Transform stairend;
    private RaycastHit2D hit;
    public Animator anim;


    public bool isNormalMoving = false;
    public bool isFirst_ing = false;
    public bool isSecond_ing = false;
    public bool isClickEnemy;
    public bool isCollideEnemy = false;
    public bool attack =true;
    public bool isFirstChanged = false;
    public bool isSecondChanged = false;
    public bool isNormalChanged = false;

    private bool isFirst = false;
    private bool isSecond = false;
    private bool isNormalMove = false;
    //private bool isClick;
    private bool isSecond_ing_click = false;
    

    public int LeftRight;
    public int FinalDirection = 1;

    public int MovingCase;
    public int Wherecharacteris;
    public int Wheretogo;
    public int HP;
    public float speed = 5;
    public float stairspeed;


    



    void Start()
    {
        enemynpc = GameObject.Find("EnemyNPC_CHG").GetComponent<EnemyNPC>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AttackRange.SetActive(false);

    }

    void Update()
    {
        if (isSecond_ing == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("isPunching", false);
                rb.isKinematic = true;
                EnemyClick();
                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Ìï≠ÏÉÅ Ï†úÏùº Î®ºÏ†Ä
                WhereToGo();
                Debug.Log("Wheretogo: " + Wheretogo);

                if (Wheretogo != 0)
                {
                    WhereCharacter();
                    CaseSetting();
                }
            }
            else if (isSecond_ing_click == true)
            {
                WhereCharacter();
                CaseSetting();
                isSecond_ing_click = false;
            }
        }
        if (isSecond_ing == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                rb.isKinematic = true;
                EnemyClick();
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
    //Í≥ÑÎã® Ïò¨ÎùºÍ∞ÄÎäîÏ§ë => ÌÅ¥Î¶≠ => ÏùºÎã® ÏúÑÏπò Ï†ÄÏû• => Í≥ÑÎã® Ïù¥ÎèôÏù¥ ÎÅùÎÇòÎ©¥ Î∞îÎ°ú Ïù¥ Ï†ÄÏû•Ìïú ÏúÑÏπòÎ°ú Ïù¥ÎèôÌï¥ÏïºÌï®
    
    private void FixedUpdate()
    {
        switch (MovingCase)
        {
            case 1:
                if(isNormalMoving == true) NormalMove();
                break;
            case 2:
                if (isFirst == false)
                {
                    firstmove();
                    
                }
                else if (isFirst == true && isSecond == false)
                {
                    //rb.isKinematic = true;
                    secondmove();
                    if (isSecond_ing_click == true)
                    {
                        break;
                    }
                }
                else if(isSecond == true)
                {
                    if (isNormalMoving == true) NormalMove();
                }
                break;
            case 3:
                if (isFirst == false && isSecond_ing == false)
                {
                    firstmove();
                }
                else if (isFirst == true && isSecond == false)
                {
                    //rb.isKinematic = true;
                    secondmove();
                }
                else if (isSecond == true)
                {
                    if (isNormalMoving == true) NormalMove();
                }
                break;
        }
        
        if (isClickEnemy)
        {
            if (enemynpc.iscollide)
            {
                if (attack == true)
                {
                    enemynpc.EnemyHP -= 5;
                    attack = false;
                    StartCoroutine(WaitForAttack());
                }
            }
        }
    }
    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(1.0f);
        attack = true;
    }
    void firstmove()
    {
        isFirst_ing = true;
        if (this.transform.localScale.x > 0)
        {
            FinalDirection = 1;
        }
        else FinalDirection = -1;

        LeftOrRight(isFirstChanged);

        anim.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, stairstart.position, Time.deltaTime * speed);
        
        if (Math.Abs(transform.position.x - stairstart.position.x) < 1f)
        {
            isFirst = true;
            isFirst_ing = false;
            
        }
    }
    public void secondmove()
    {
        isSecond_ing = true;
        if (this.transform.localScale.x > 0)//∏∂¡ˆ∏∑ πŸ∂Û∫∏∞Ì ¿÷¥¯ πÊ«‚¿Ã ø¿∏•¬ ¿Ã∏È 1, øﬁ¬ ¿Ã∏È -1
        {
            FinalDirection = 1;
        }
        else FinalDirection = -1;
        if (transform.position.x > stairend.position.x)//∏∂¡ˆ∏∑ πŸ∂Û∫∏∞Ì ¿÷¥¯ πÊ«‚∞˙ æ’¿∏∑Œ ∞• ∏Ò¿˚¡ˆ¿« πÊ«‚¿Ã ¥Ÿ∏¶∂ß Ω∫ƒ…¿œ¿ª ª©∞≈≥™ ¥ı«ÿ¡‹
        {
            LeftRight = -1;

            if (isSecondChanged == false)
            {
                if (FinalDirection != LeftRight)
                {
                    this.transform.localScale += new Vector3(-2f, 0f, 0f);
                }
                isSecondChanged = true;
            }
        }
        else
        {
            LeftRight = 1;
            if (isSecondChanged == false)
            {
                if (FinalDirection != LeftRight)
                {
                    this.transform.localScale += new Vector3(2f, 0f, 0f);
                }
                isSecondChanged = true;
            }
        }
        //transform.position = Vector2.MoveTowards(stairstart.position, stairend.position, Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(transform.position, stairend.position, Time.deltaTime * stairspeed);
        if (Math.Abs(transform.position.y - stairend.position.y) < 0.1f)
        {
            //rb.isKinematic = false;
            isSecond = true;
            isSecond_ing = false;
            isNormalMoving = true;
        }
    }
    public void NormalMove()
    {
        if (isClickEnemy == true)
        {
            AttackRange.SetActive(true);
        }
        destination = new Vector2(MousePosition.x, transform.position.y);
        if (this.transform.localScale.x > 0)
        {
            FinalDirection = 1;
        }
        else FinalDirection = -1;

        if (transform.position.x > destination.x)
        {
            LeftRight = -1;

            if (isNormalChanged == false)
            {
                if (FinalDirection != LeftRight)
                {
                    this.transform.localScale += new Vector3(-2f, 0f, 0f);
                }
                isNormalChanged = true;
            }
        }
        else
        {
            LeftRight = 1;
            if (isNormalChanged == false)
            {
                if (FinalDirection != LeftRight)
                {
                    this.transform.localScale += new Vector3(2f, 0f, 0f);
                }
                isNormalChanged = true;
            }
        }
        anim.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        if (Math.Abs(transform.position.x - destination.x) < 0.01f)
        {
            isNormalMoving = false;
            anim.SetBool("isWalking", false);
            //isClick = false;
        }
    }
    void LeftOrRight(bool isChanged)//isChanged¥¬ ∏≈∞≥∫Øºˆ∑Œ ∫“∑Øø‘¡ˆ∏∏ ∆˜¡ˆº«¿ª ∏≈∞≥∫Øºˆ∑Œ ∫“∑Øø¿¡ˆ ∏¯«ÿº≠ «‘ºˆ»≠∏¶∏¯«ﬂ¥Ÿ ±◊∑°º≠ firstmove∏∏ ¿Ã «‘ºˆ∏¶ ªÁøÎ«‘
    {
        if (transform.position.x > stairstart.position.x)
        {
            LeftRight = -1;

            if (isChanged == false)
            {
                if (FinalDirection != LeftRight)
                {
                    this.transform.localScale += new Vector3(-2f, 0f, 0f);
                }
                isChanged = true;
            }
        }
        else
        {
            LeftRight = 1;
            if (isChanged == false)
            {
                if (FinalDirection != LeftRight)
                {
                    this.transform.localScale += new Vector3(2f, 0f, 0f);
                }
                isChanged = true;
            }
        }
    }
    void EnemyClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
        if (hit)
        {
            //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "EnemyNPC")
            {
                //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                isClickEnemy = true;
                rb.isKinematic = false;
            }
            else
            {
                isClickEnemy = false;
                AttackRange.SetActive(false);
            }
        }
        else
        {
            isClickEnemy = false;
            AttackRange.SetActive(false);
        }
    }
    void WhereToGo()
    {
        if (MousePosition.y > -390 && MousePosition.y < -125)
        {
            Wheretogo = 1;//1Ï∏µÏóê Í∞àÍ≤É
        }
        else if (MousePosition.y > -75 && MousePosition.y < 390)
        {
            Wheretogo = 2;//2Ï∏µÏóê Í∞àÍ≤É
        }
        else
            Wheretogo = 0; //Ïßë Ïô∏Ïóê Îã§Î•∏Í≥≥ ÏÑ†ÌÉù
    }
    private void WhereCharacter()
    {
        if (transform.position.y > -290 && transform.position.y < -25)
        {
            Wherecharacteris = 1;//Ï∫êÎ¶≠ÌÑ∞Í∞Ä 1Ï∏µÏóê ÏûàÏùå
        }
        if (transform.position.y > 24 && transform.position.y < 290)
        {
            Wherecharacteris = 2;//Ï∫êÎ¶≠ÌÑ∞Í∞Ä 2Ï∏µÏóê ÏûàÏùå
        }
        
    }
    private void CaseSetting()
    {
        if(Wheretogo == Wherecharacteris)//Í∞ôÏùÄ Ï∏µÎÇ¥ÏóêÏÑú ÏõÄÏßÅÏù¥Í∏∞
        {
            MovingCase = 1;
            isNormalMoving = true;
            isNormalChanged = false;
        }
        if(Wheretogo > Wherecharacteris)//Ïò¨ÎùºÍ∞ÄÍ∏∞
        {
            isFirst = false;
            isSecond = false;
            isNormalMove = false;
            isFirstChanged = false;
            isSecondChanged = false;
            isNormalChanged = false;
            MovingCase = 2; 
            stairstart = floor[Wherecharacteris - 1].down;
            stairend = floor[Wherecharacteris - 1].up;
        }
        if (Wheretogo < Wherecharacteris)//ÎÇ¥Î†§Í∞ÄÍ∏∞(3Ï∏µÏù¥ÏÉÅ ÎêòÎ©¥ Îã¨ÎùºÏ†∏ÏïºÌï®)
        {
            isFirst = false;
            isSecond = false;
            isFirstChanged = false;
            isSecondChanged = false;
            isNormalChanged = false;
            MovingCase = 3;
            stairstart = floor[Wherecharacteris - 2].up;
            stairend = floor[Wherecharacteris - 2].down;
        }
        /*
        if(isSecond_ing_click == true)//Í≥ÑÎã® Ïò¨ÎùºÍ∞ÄÍ±∞ÎÇò ÎÇ¥Î†§Í∞ÄÎçò Ï§ëÏóê ÌÅ¥Î¶≠ÎêêÏùÑÎïå
        {
            MovingCase = 4;
        }
        */
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private void GoUp()
    {

    }
    private void GoDown()
    {

    }

}
