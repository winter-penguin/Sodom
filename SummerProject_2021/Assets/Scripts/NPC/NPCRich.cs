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
    //DB

    public bool dead = false;
    public bool isAttack = false;
    protected bool canAtk = true;
    protected float AttackCoolTimeCacl;
    private GameObject Player;
    private CharacterValue playerValue;

    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private Collider2D PlayerCollider;



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

    public int MovingCase;
    public int npcFloor;
    public int Wheretogo;
    #endregion

    private Animator animator;
    void Start()
    {
        DBdata = GameObject.Find("DBManager").GetComponent<DB_Character>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerValue = Player.GetComponent<CharacterValue>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = this.gameObject.GetComponent<Collider2D>();
        PlayerCollider = Player.GetComponent<Collider2D>();
        AttackCoolTimeCacl = attack_speed;
        animator = GetComponent<Animator>();
        StartCoroutine(CheckStateForActon());
        StartCoroutine(CalcCoolTime());
        StartCoroutine(FSM());
        Physics2D.IgnoreCollision(collider, PlayerCollider, true);
    }
    void Update()
    {
        npcPosition = transform.position;
        playerPosition = Player.transform.position;
        direction = playerPosition.x - npcPosition.x;
        direction = (direction < 0) ? 180 : 0;
        if (DBdata.isCharacterDB == true)
        {
            DataSet();
        }


        if (isSecond_ing == false)
        {
            WhereToGo();
            CharacterPosition();
            CaseSetting();
            isSecond_ing = true;
        }
        if (isSecond_ing)
        {
            rigidbody.isKinematic = true;
            WhereToGo();
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
                        //rb.isKinematic = true;
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
    protected bool CanAtkStateFun()
    {
        distance = Vector2.Distance(Player.transform.position, transform.position);

        if (distance <= attack_range)
        {
            return true;
        }
        else
        {
            return false;
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
    protected virtual IEnumerator Dead()
    {
        yield return null;
    }


    protected virtual IEnumerator Move()
    {
        yield return null;
        if (CanAtkStateFun() && canAtk)
        {
            CurrentState = State.Attack;
        }
        else
        {
            CaseSetting();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            {
                animator.SetTrigger("move");
            }

        }
    }

    void Firstmove()
    {
        StairDirection = stairStart.position.x - npcPosition.x;
        StairDirection = (StairDirection < 0) ? 180 : 0; //1F direction
        transform.rotation = Quaternion.Euler(0, StairDirection, 0);

        transform.position = Vector2.MoveTowards(transform.position, stairStart.position, Time.deltaTime * move_speed);

        if (Math.Abs(transform.position.x - stairStart.position.x) < 1f)
        {
            isFirst = true;
            isFirst_ing = false;

        }
    }

    private void Secondmove()
    {
        isSecond_ing = true;
        StairDirection = stairEnd.position.x - npcPosition.x;
        StairDirection = (StairDirection < 0) ? 180 : 0; //direction
        transform.rotation = Quaternion.Euler(0, StairDirection, 0);
        rigidbody.isKinematic = true;
        //transform.position = Vector2.MoveTowards(stairStart.position, stairEnd.position, Time.deltaTime * move_speed);
        transform.position = Vector2.MoveTowards(transform.position, stairEnd.position, Time.deltaTime * move_speed);
        if (Math.Abs(transform.position.y - stairEnd.position.y) < 0.1f)
        {
            rigidbody.isKinematic = false;
            isSecond = true;
            isSecond_ing = false;
            isNormalMoving = true;
        }
    }

    public void NormalMove()
    {
        destination = new Vector2(playerPosition.x, transform.position.y);
        transform.rotation = Quaternion.Euler(0, direction, 0);
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * move_speed);
        if (Math.Abs(transform.position.x - destination.x) < 0.01f)
        {
            isNormalMoving = false;
            rigidbody.isKinematic = false;
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
        if (transform.position.y > -291 && transform.position.y < -25)
        {
            npcFloor = 1;//캐릭터가 1층에 있음
        }
        if (transform.position.y > 24 && transform.position.y < 290)
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
            isFirst = false;
            isSecond = false;
            MovingCase = 2;
            isFirst_ing = true;
            stairStart = floor[0].down;
            stairEnd = floor[0].up;
        }
        if (Wheretogo < npcFloor)//내려가기(3층이상 되면 달라져야함)
        {
            isFirst = false;
            isSecond = false;
            MovingCase = 3;
            stairStart = floor[0].up;
            stairEnd = floor[0].down;
        }
    }

}
