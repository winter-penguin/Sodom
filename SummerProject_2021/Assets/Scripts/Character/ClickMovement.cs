//*******************************
// Editor:CHG
// LAST EDITED DATE : 2021.07.19
// Script Purpose : Character_ClickMove
//*******************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickMovement : MonoBehaviour
{
    #region Variables
    NPCRich enemynpc;
    private WallCollision _wallCollision;
    private SoundManager _soundManager;
    private GameManager _gameManager;
    [System.Serializable] // class를 인스펙터에서 보여줌
    //[SerializeField] // private 변수를 인스펙터에서 보여줌
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
    public GameObject Stair;
    public Button button;
    public LayerMask layerMask;

    private Vector2 destination;
    private Vector2 first_destination;
    private Vector2 MousePosition;
    private Camera camera1;
    [HideInInspector]
    public Rigidbody2D rb;
    private Transform stairstart;
    private Transform stairend;
    private RaycastHit2D hit;
    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public bool isNormalMoving = false;
    [HideInInspector]
    public bool isFirst_ing = false;
    
    public bool isSecond_ing = false;
    //[HideInInspector]
    public bool isClickEnemy;
    public bool isCollideEnemy = false;
    public bool attack =true;
    [HideInInspector]
    public bool isFirstChanged = false;
    [HideInInspector]
    public bool isSecondChanged = false;
    [HideInInspector]
    public bool isNormalChanged = false;

    public bool isBackHouse = false;

    public bool isWall = false;

    public bool isButtonClick;
    
    private bool isFirst = false;
    private bool isSecond = false;
    private bool isNormalMove = false;
    private bool isSecond_ing_click = false;
    

    public int LeftRight;
    public int FinalDirection = 1;

    public int MovingCase;
    public int Wherecharacteris;
    public int Wheretogo;
    
    public float speed = 5;
    public float stairspeed;
    #endregion

    void Start()
    {
        
        //_wallCollision = GameObject.FindGameObjectWithTag("Wall").GetComponent<WallCollision>();
        rb = gameObject.GetComponent<Rigidbody2D>();//이 캐릭터가 왜 NPC하고 부딪혔을때 안멈추는지 모르겠음
        anim = GetComponent<Animator>();
        AttackRange.SetActive(false);
        Stair.SetActive(false);
        _gameManager = GameManager._instance;
        _soundManager = GameObject.FindWithTag("GameController").GetComponent<SoundManager>();

    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            #region MouseClick

            if (isSecond_ing == false)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //항상 제일 먼저
                    if (transform.position.x > MousePosition.x) // 가야할 방향이 왼쪽이면
                    {
                        LeftRight = -1;
                    }
                    else LeftRight = 1;
                    //Debug.Log(LeftRight);

                    if (isWall)//벽이랑 닿은 상태일때
                    {
                        if (_wallCollision.iscollide)
                        {
                            //Debug.Log(_wallCollision.LeftOrRight);
                            Debug.Log("벽과 닿은상태입니다");
                            if (_wallCollision.LeftOrRight == LeftRight)
                            {
                                WhereToGo();
                                ButtonClick();
                                EnemyClick();
                                Debug.Log("Wheretogo: " + Wheretogo);

                                if (Wheretogo != 0) //집밖을 선택하지 않았을때만 캐릭터의 위치를 구하고, 케이스를 정해줌
                                {
                                    WhereCharacter();
                                    CaseSetting();
                                }
                            }
                        }
                    }
                    else
                    {
                        WhereToGo();
                        ButtonClick();
                        EnemyClick();

                        if (Wheretogo != 0) //집밖을 선택하지 않았을때만 캐릭터의 위치를 구하고, 케이스를 정해줌
                        {
                            WhereCharacter();
                            CaseSetting();
                        }
                    }
                }
                else if (isSecond_ing_click == true) //isSecond_ing일때 클릭했다면 isSecond_ing이 false일때 다시 움직임 시작
                {
                    WhereCharacter();
                    CaseSetting();
                    EnemyClick();
                    ButtonClick();
                    isSecond_ing_click = false;

                }
            }

            if (isSecond_ing)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    rb.isKinematic = true;

                    MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    WhereToGo();
                    //Debug.Log("Wheretogo: " + Wheretogo);
                    if (Wheretogo != 0)
                    {
                        isSecond_ing_click = true;
                    }
                }
            }

            #endregion
        }
        else //UI를 클릭했을때
        {
            #region MouseClick_Button

            if (isSecond_ing == false)
            {
                if (Input.GetMouseButtonUp(0))//클릭 고려첫번째
                {
                    ButtonClick();
                    if (isButtonClick)
                    {
                        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //항상 제일 먼저
                        if (transform.position.x > MousePosition.x) // 가야할 방향이 왼쪽이면
                        {
                            LeftRight = -1;
                        }
                        else LeftRight = 1;
                        //Debug.Log(LeftRight);

                        if (isWall)
                        {
                            if (_wallCollision.iscollide)
                            {
                                //Debug.Log(_wallCollision.LeftOrRight);
                                Debug.Log("벽과 닿은상태입니다");
                                if (_wallCollision.LeftOrRight == LeftRight)
                                {
                                    WhereToGo();
                                    EnemyClick();
                                    Debug.Log("Wheretogo: " + Wheretogo);

                                    if (Wheretogo != 0) //집밖을 선택하지 않았을때만 캐릭터의 위치를 구하고, 케이스를 정해줌
                                    {
                                        WhereCharacter();
                                        CaseSetting();
                                    }
                                }
                            }
                        }
                        else
                        {
                            WhereToGo();
                            EnemyClick();

                            if (Wheretogo != 0) //집밖을 선택하지 않았을때만 캐릭터의 위치를 구하고, 케이스를 정해줌
                            {
                                WhereCharacter();
                                CaseSetting();
                            }
                        }
                    }


                }
                else if (isSecond_ing_click == true) //isSecond_ing일때 클릭했다면 isSecond_ing이 false일때 다시 움직임 시작
                {
                    WhereCharacter();
                    CaseSetting();
                    EnemyClick();
                    isSecond_ing_click = false;

                }
            }

            if (isSecond_ing)
            {
                if (Input.GetMouseButtonUp(0))//클릭 고려두번째
                {
                    ButtonClick();
                    if (isButtonClick)
                    {
                        rb.isKinematic = true;

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

            #endregion
        }

    }
    //계단 올라가는중 => 클릭 => 일단 위치 저장 => 계단 이동이 끝나면 바로 이 저장한 위치로 이동해야함
    private void OnCollisionEnter2D(Collision2D collision)//문이랑 부딪히면
    {
        if (collision.collider.CompareTag("Wall"))//태그가 벽인 오브젝트와 부딪혔을때만 그이상 안가지는 판정
        {
            isWall = true;
            _wallCollision = collision.gameObject.GetComponent<WallCollision>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            isWall = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        if (isBackHouse)
        {
            switch (MovingCase)
            {
                
                case 1:
                
                    if(isNormalMoving == true) NormalMove();
                    break;
                case 2:
                
                    
                    break;
                case 3:
                
                    break;
            }
        }
        else
        {
            switch (MovingCase)
            {
                
                case 1:
                
                    if(isNormalMoving == true) NormalMove();
                    break;
                case 2:
                
                    if (isFirst == false && isFirst_ing)
                    {
                        Firstmove();
                    
                    }
                    else if (isFirst == true && isSecond == false)
                    {
                        //rb.isKinematic = true;
                        Secondmove();
                        if (isSecond_ing_click)
                        {
                            break;
                        }
                    }
                    else if(isSecond)
                    {
                        if (isNormalMoving) NormalMove();
                    }
                    break;
                case 3:
                
                    if (isFirst == false && isSecond_ing == false)
                    {
                        Firstmove();
                    }
                    else if (isFirst == true && isSecond == false)
                    {
                        //rb.isKinematic = true;
                        Secondmove();
                    }
                    else if (isSecond == true)
                    {
                        if (isNormalMoving == true) NormalMove();
                    }
                    break;
            }
        }
        
        
        if (isClickEnemy == true)
        {
            if (enemynpc.iscollide == true)
            {
                if (attack == true)
                {
                    enemynpc.health -= 5;
                    attack = false;
                    StartCoroutine(WaitForAttack());
                }
            }
        }
    }

    public void BackHouse()
    {
        if (isBackHouse)
        {
            isBackHouse = false;
        }
        else
        {
            isBackHouse = true;
        }
    }
    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(1.0f);
        attack = true;
    }
    void Firstmove()
    {
        if (transform.localScale.x > 0)
        {
            FinalDirection = 1;
        }
        else FinalDirection = -1;

        LeftOrRight(isFirstChanged, stairstart.position);

        anim.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, stairstart.position, Time.deltaTime * speed);
        
        if (Math.Abs(transform.position.x - stairstart.position.x) < 1f)
        {
            isFirst = true;
            isFirst_ing = false;
            
        }
    }

    private void Secondmove()
    {
        Stair.SetActive(true); //계단 올라가거나 내려가는 중에만 플레이어 보다 SortingLayer높은 계단 오브젝트 활성화
        isSecond_ing = true;
        LeftOrRight(isSecondChanged, stairend.position);
        rb.isKinematic = true;//Why? 1.enemynpc 클릭하면 AttackPos Collision활성화되서 계단이랑 부딪혀서 밀려나서 2. 2층바닥에 머리 부딪혀서 밀려남
        //transform.position = Vector2.MoveTowards(stairstart.position, stairend.position, Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(transform.position, stairend.position, Time.deltaTime * stairspeed);
        if (Math.Abs(transform.position.y - stairend.position.y) < 0.1f)
        {
            rb.isKinematic = false;
            isSecond = true;
            isSecond_ing = false;
            isNormalMoving = true;
            Stair.SetActive(false);
        }
    }
    public void NormalMove()
    {
        if (isClickEnemy)//계단을 움직이는중에 적을 클릭해도 노말무빙중에 콜라이더 키기때문에 계단에서 버벅임 없음
        {
            AttackRange.SetActive(true);
        }
        destination = new Vector2(MousePosition.x, transform.position.y);
        LeftOrRight(isNormalChanged, destination);
        anim.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        if (Math.Abs(transform.position.x - destination.x) < 0.01f)
        {
            isNormalMoving = false;
            anim.SetBool("isWalking", false);
            rb.isKinematic = false;
            
            //isClick = false;
        }
    }
    void LeftOrRight(bool isChanged, Vector3 vector3)//isChanged 
    {
        if (!isChanged)//방향을 바꾸려는걸 시도하지 않았을때만
        {
            if (transform.localScale.x > 0) // 현재 방향이 오른쪽이면
            {
                FinalDirection = 1;
            }
            else FinalDirection = -1;
            
            if (transform.position.x > vector3.x) // 가야할 방향이 왼쪽이면
            {
                LeftRight = -1;
                if (FinalDirection != LeftRight) // 현재 방향과 가야할 방향이 다를때만 변화주기
                {
                    transform.localScale += new Vector3(-2f, 0f, 0f);
                }
            }
            else // 가야할 방향이 오른쪽이면
            {
                LeftRight = 1; 
                if (FinalDirection != LeftRight)
                {
                    transform.localScale += new Vector3(2f, 0f, 0f);
                }
                
            }

            isChanged = true;
        }
    }

    void ButtonClick()//layermask가 Button인 콜라이더만 인식(레이어마스크 인스펙터에서 설정 가능)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 50f,layerMask);
        if (hit)
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Button"))
            {
                isButtonClick = true;
            }
            else
            {
                isButtonClick = false;
            }
        }
        else
        {
            isButtonClick = false;
        }
    }
    void EnemyClick()//이함수를 적대적 엔피시가 스폰됐을때만 실행시켜야 Good! 이라고 생각했지만 이함수로 버튼을 구분할거임
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
        if (hit)
        {
            //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
            //Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("EnemyNPC"))
            {
                enemynpc = hit.collider.GetComponent<NPCRich>();
                //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                isClickEnemy = true;
                rb.isKinematic = false;
            }
            else if(hit.collider.CompareTag("Button"))
            {
                isButtonClick = true;
            }
            else
            {
                
                anim.SetBool("isPunching", false);
                isClickEnemy = false;
                //isButtonClick = false;
                AttackRange.SetActive(false);
            }
        }
        else
        {
            anim.SetBool("isPunching", false);
            isClickEnemy = false;
            //isButtonClick = false;
            AttackRange.SetActive(false);
        }
    }
    void WhereToGo()
    {
        if (MousePosition.y > -385 && MousePosition.y < -81)
        {
            Wheretogo = 1;//1층에 갈것
        }
        else if (MousePosition.y > -55 && MousePosition.y < 250)
        {
            Wheretogo = 2;//2층에 갈것
        }
        else
            Wheretogo = 0; //집 외에 다른곳 선택
    }
    private void WhereCharacter()
    {
        if (transform.position.y > -291 && transform.position.y < -25)
        {
            Wherecharacteris = 1;//캐릭터가 1층에 있음
        }
        if (transform.position.y > 24 && transform.position.y < 290)
        {
            Wherecharacteris = 2;//캐릭터가 2층에 있음
        }
        
    }
    private void CaseSetting()
    {
        if(Wheretogo == Wherecharacteris)//같은 층내에서 움직이기
        {
            MovingCase = 1;
            isNormalMoving = true;
            isNormalChanged = false;
        }
        if(Wheretogo > Wherecharacteris)//올라가기
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
            isFirst_ing = true;
        }
        if (Wheretogo < Wherecharacteris)//내려가기(3층이상 되면 달라져야함)
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
        if(isSecond_ing_click == true)//계단 올라가거나 내려가던 중에 클릭됐을때
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
    public void isButtonTrue()
    {
        isButtonClick = true;

    }
    private void GoDown()
    {

    }

}
