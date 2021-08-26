using System;
using System.Collections;
using UnityEngine;

public class NPCRich : MonoBehaviour
{
    DB_Character DBdata;
    public enum Guilty
    {
        Gangster, Killer, Prisoner
    };
    public enum State { Idle, Move, Attack, Dead };
    public Guilty CurrentGuilty = Guilty.Prisoner;
    public State CurrentState = State.Idle;
    public float id, health, attack_power, attack_speed, attack_range, move_speed;
    private bool dataloading;
    //DB

    public bool dead = false;
    public bool isAttack = false;
    protected bool canAtk = true;
    private bool canAtk2 = true;
    protected float AttackCoolTimeCacl;
    private GameObject Player;
    private ClickMovement playermove;
    private CharacterValue playerValue;

    #region Move
    [System.Serializable]
    public class Floor
    {
        public Transform up;
        public Transform down;
    }
    public Floor[] floor;

    private Vector2 npcPosition;
    private Vector2 playerPosition;
    private Vector2 destination;

    private Transform stairStart;
    private Transform stairEnd;
    private float distance;
    private float direction;
    private float StairDirection;

    private bool isNormalMoving = false;
    private bool isFirst_ing = false;
    private bool isSecond_ing = false;

    private bool isFirst = false;
    private bool isSecond = false;
    private bool StairTrigger = false;

    public int MovingCase;
    private int npcFloor;
    private int Wheretogo;
    #endregion

    private Animator animator;
    void Start()
    {
        DBdata = GameObject.Find("DBManager").GetComponent<DB_Character>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playermove = Player.GetComponent<ClickMovement>();
        playerValue = Player.GetComponent<CharacterValue>();
        AttackCoolTimeCacl = attack_speed;
        animator = GetComponent<Animator>();
        StartCoroutine(CheckStateForActon());
        StartCoroutine(CalcCoolTime());
        StartCoroutine(FSM());
    }
    void Update()
    {
        npcPosition = transform.position;
        playerPosition = Player.transform.position;
        direction = playerPosition.x - npcPosition.x;
        direction = (direction < 0) ? 180 : 0;
        if (DBdata.isCharacterDB == true && !dataloading)
        {
            DataSet();
        }

        if (isSecond_ing == false)
        {
            WhereToGo();
            CharacterPosition();
            CaseSetting();
            if (playermove.isSecond_ing) //플레이어쫓아가 계단오르기
            {
                canAtk2 = false;
                if (playermove.Wheretogo == 2)
                {
                    UP();
                }
                else if (playermove.Wheretogo == 1)
                {
                    Down();
                }
            }
            else
            {
                canAtk2 = true;
            }
        }
        if (isSecond_ing)
        {
            WhereToGo();
            if (!playermove.isSecond_ing)
            {
                canAtk2 = false;
            }
            else
            {
                canAtk2 = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (CurrentState == State.Move)
        {
            switch (MovingCase)
            {
                case 1:
                    if (isNormalMoving == true) NormalMove();
                    break;
                case 2:
                    if (isFirst == false && isFirst_ing)
                    {
                        Firstmove();

                    }
                    else if (isFirst == true && isSecond == false)
                    {
                        Secondmove();
                    }
                    else if (isSecond)
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
                case 4:
                    if(StairTrigger)//계단에서 마주쳤을때
                    {
                        Secondmove();
                    }
                    break;
            }
        }

    }
    private void DataSet()
    {
        for (int i = 0; i < DBdata.characterDB.Length; i++)
        {
            if (DBdata.characterDB[i].guilty == CurrentGuilty.ToString())
            {
                id = DBdata.characterDB[i].id;
                health = DBdata.characterDB[i].health;
                attack_power = DBdata.characterDB[i].attack_power;
                attack_speed = DBdata.characterDB[i].attack_speed;
                attack_range = DBdata.characterDB[i].attack_range;
                move_speed = DBdata.characterDB[i].move_speed;
                dataloading = true;
                break;
            }
        }
    }
    #region HP
    public float HP
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                dead = true;
                CurrentState = State.Dead;
                this.gameObject.SetActive(false);
            }
        }
    }

    public float HpChanged(float damage)
    {
        HP += damage;
        if (HP > 0)
        {
            //knockBack = true;
            //StartCoroutine(KnockBack());
        }
        return HP;
    }
    #endregion
    IEnumerator CheckStateForActon()
    {
        while (!dead)
        {
            switch (CurrentState)
            {
                case State.Idle:
                    break;
                case State.Move:
                    break;
                case State.Attack:
                    isAttack = true;
                    break;

            }
            yield return null;
        }
    }
    protected virtual IEnumerator FSM()
    {
        yield return null;

        while (!dead)
        {
            yield return StartCoroutine(CurrentState.ToString());
        }
    }
    protected virtual IEnumerator Idle()
    {
        yield return null;

        if (CanAtkStateFun())
        {
            if (canAtk)
            {
                CurrentState = State.Attack;
            }
            else
            {
                CurrentState = State.Idle;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator.SetTrigger("Idle");
                }
            }
        }
        else
        {
            CurrentState = State.Move;
        }
    }
    protected virtual IEnumerator Dead()
    {
        yield return null;
    }

    #region Attack
    protected virtual IEnumerator Attack()
    {
        yield return null;
        //Atk
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            animator.SetTrigger("punch");
        }
        transform.rotation = Quaternion.Euler(0, direction, 0);
        playerValue.HpChanged(-attack_power);
        canAtk = false;
        CurrentState = State.Idle;
    }
    protected bool CanAtkStateFun()
    {
        distance = Vector2.Distance(playerPosition, npcPosition);

        if (distance <= attack_range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected virtual IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                AttackCoolTimeCacl -= Time.deltaTime;
                if (AttackCoolTimeCacl <= 0 && !dead)
                {
                    AttackCoolTimeCacl = attack_speed;
                    canAtk = true;
                }
            }
        }
    }
    #endregion
    #region Move
    protected virtual IEnumerator Move()
    {
        yield return null;
        if (CanAtkStateFun() && canAtk && canAtk2)
        {
            CurrentState = State.Attack;
        }
        else
        {
            //CaseSetting();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            {
                animator.SetTrigger("move");
            }

        }
    }

    void Firstmove() //계단앞으로이동
    {
        StairDirection = stairStart.position.x - npcPosition.x;
        StairDirection = (StairDirection < 0) ? 180 : 0; //1F direction
        transform.rotation = Quaternion.Euler(0, StairDirection, 0);
        transform.position = Vector2.MoveTowards(npcPosition, stairStart.position, Time.deltaTime * move_speed);

        if (Math.Abs(npcPosition.x - stairStart.position.x) < 1f)
        {
            isFirst = true;
            isFirst_ing = false;
            npcPosition.x = stairStart.position.x;
        }
    }

    private void Secondmove() //계단이용
    {
        isSecond_ing = true;
        StairDirection = stairEnd.position.x - npcPosition.x;
        StairDirection = (StairDirection < 0) ? 180 : 0; //direction
        transform.rotation = Quaternion.Euler(0, StairDirection, 0);
        transform.position = Vector2.MoveTowards(npcPosition, stairEnd.position, Time.deltaTime * move_speed);

        if (Math.Abs(npcPosition.y - stairEnd.position.y) < 0.5f)
        {
            isSecond = true;
            isSecond_ing = false;
            isNormalMoving = true;
            npcPosition.y = stairEnd.position.y;
        }
    }

    public void NormalMove()
    {
        destination = new Vector2(playerPosition.x, npcPosition.y);
        transform.rotation = Quaternion.Euler(0, direction, 0);
        transform.position = Vector2.MoveTowards(npcPosition, destination, Time.deltaTime * move_speed);
        if (Math.Abs(npcPosition.x - destination.x) < 0.01f)
        {
            isNormalMoving = false;
        }
    }

    private void WhereToGo()
    {
        if (playerPosition.y > -291 && playerPosition.y < -25)
        {
            Wheretogo = 1;//1층에 갈것
        }
        if (playerPosition.y > 24 && playerPosition.y < 290)
        {
            Wheretogo = 2;//2층에 갈것
        }
    }
    private void CharacterPosition()
    {
        if (npcPosition.y > -291 && npcPosition.y < -25)
        {
            npcFloor = 1;//캐릭터가 1층에 있음
        }
        if (npcPosition.y > 24 && npcPosition.y < 290)
        {
            npcFloor = 2;//캐릭터가 2층에 있음
        }

    }

    private void CaseSetting()
    {
        if (Wheretogo == npcFloor)//같은 층내에서 움직이기
        {
            MovingCase = 1;
            isNormalMoving = true;
        }
        if (Wheretogo > npcFloor)//올라가기
        {
            UP();
        }
        if (Wheretogo < npcFloor)//내려가기(3층이상 되면 달라져야함)
        {
            Down();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isSecond_ing && playermove.isSecond_ing && Wheretogo != playermove.Wheretogo) //계단에서 마주쳤을때 따라내려가기
            {
                StairTrigger = true;
                switch (playermove.MovingCase)
                {
                    case 2:
                        stairStart = floor[0].down;
                        stairEnd = floor[0].up;
                        break;
                    case 3:
                        stairStart = floor[0].up;
                        stairEnd = floor[0].down;
                        break;
                }
            }
        }
    }
    private void UP()
    {
        isFirst = false;
        isSecond = false;
        MovingCase = 2;
        isFirst_ing = true;
        stairStart = floor[0].down;
        stairEnd = floor[0].up;
    }
    private void Down()
    {
        isFirst = false;
        isSecond = false;
        MovingCase = 3;
        stairStart = floor[0].up;
        stairEnd = floor[0].down;
    }
    #endregion
}
