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
    private Vector2 npcPosition;
    private Vector2 playerPosition;

    private float distance;
    private float direction;
    private float StairDirection;
    private Transform[] UpDown;
    private GameObject UPFlatform;

    public bool GoDownStair = false;
    public bool GoUpStair = false;
    private Animator animator;
    void Start()
    {
        DBdata = GameObject.Find("DBManager").GetComponent<DB_Character>();
        Player = GameObject.FindGameObjectWithTag("Player");
        UpDown = GameObject.Find("Points").transform.GetComponentsInChildren<Transform>();
        UPFlatform = GameObject.Find("2ndFlatform");
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
                    rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    break;
                case State.Move:
                    rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    break;
                case State.Attack:
                    isAttack = true;
                    rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
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
        playerValue.HpChanged(-attack_power);
        canAtk = false;
        CurrentState = State.Idle;
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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            {
                animator.SetTrigger("move");
            }

            if (GoDownStair == false && GoUpStair == false)
            {
                Physics2D.IgnoreCollision(collider, UPFlatform.GetComponent<Collider2D>(), false);
                transform.Translate(Vector2.right * move_speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, direction, 0);
            }
            else if(GoDownStair || GoUpStair)
            {
                Stairs();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Up")
        {
            GoDownStair = (playerPosition.y < npcPosition.y) ? true : false;
            if(GoUpStair)
            {
                GoUpStair = false;
            }
        }
        else if (other.gameObject.name == "Down")
        {
            GoUpStair = (playerPosition.y > npcPosition.y) ? true : false;
            if (GoDownStair)
            {
                GoDownStair = false;
            }
        }
    }
    private void Stairs()
    {
        Physics2D.IgnoreCollision(collider, UPFlatform.GetComponent<Collider2D>(), true);
        if (GoDownStair)
        {
            StairDirection = UpDown[2].position.x - npcPosition.x;
            StairDirection = (StairDirection < 0) ? 180 : 0; //1F direction
            transform.rotation = Quaternion.Euler(0, StairDirection, 0);
            transform.position = Vector3.Lerp(transform.position, UpDown[2].position, Time.deltaTime * 0.008f);
        }
        else if (GoUpStair)
        {
            StairDirection = UpDown[1].position.x - npcPosition.x;
            StairDirection = (StairDirection < 0) ? 180 : 0; //2F direction
            transform.rotation = Quaternion.Euler(0, StairDirection, 0);
            transform.position = Vector3.Lerp(transform.position, UpDown[1].position, Time.deltaTime * 0.008f);
        }

        StartCoroutine(Move());
    }

    protected virtual IEnumerator Dead()
    {
        yield return null;
    }


}
