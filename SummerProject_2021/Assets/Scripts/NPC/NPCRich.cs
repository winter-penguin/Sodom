using System.Collections;
using System.Collections.Generic;
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
    private float distance;
    private float direction;
    private float StairDirectionUP;
    private float StairDirectionDown;

    private Rigidbody2D rigidbody;

    private Vector2 npcPosition;
    private Vector2 playerPosition;

    private GameObject WoodenStair;
    private Transform[] UpDown;
    private bool GoStair = false; 
    void Start()
    {
        DBdata = GameObject.Find("DBManager").GetComponent<DB_Character>();
        Player = GameObject.FindGameObjectWithTag("Player");
        WoodenStair = GameObject.Find("WoodenStair_sample");
        UpDown = WoodenStair.transform.GetComponentsInChildren<Transform>();
        playerValue = Player.GetComponent<CharacterValue>();
        rigidbody = GetComponent<Rigidbody2D>();
        AttackCoolTimeCacl = attack_speed;
        StartCoroutine(CheckStateForActon());
        StartCoroutine(CalcCoolTime());
        StartCoroutine(FSM());
    }
    void Update()
    {
        npcPosition = transform.position;
        playerPosition = Player.transform.position;
        direction = playerPosition.x - npcPosition.x;
        direction = (direction < 0) ? -1 : 1;
        if (DBdata.isCharacterDB == true)
        {
            StartCoroutine(DataSet());
        }

    }

    protected IEnumerator DataSet()
    {
        bool DataLoding = true;
        while (DataLoding)
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
                    DataLoding = false;
                    break;
                }
            }
            yield return null;
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

        if (Player.transform.position.y == transform.position.y && distance <= attack_range)
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
            if (playerPosition.y == npcPosition.y && GoStair == false)
            {
                transform.Translate(new Vector2(direction, 0) * move_speed * Time.deltaTime);
                transform.localScale = new Vector3(direction, 1, 1);
            }
            else
            {
                GoStair = true;
                Stairs();
            }
        }
    }
    private void Stairs()
    {
        if (playerPosition.y < 25 && playerPosition.y > -290) 
        {
            StairDirectionUP = UpDown[0].position.x - npcPosition.x;
            StairDirectionUP = (StairDirectionUP < 0) ? -1 : 1; //2F direction
            transform.localScale = new Vector3(StairDirectionUP, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, UpDown[0].position, 0.005f);
            if(npcPosition.x == UpDown[0].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, UpDown[1].position, 0.005f);
                GoStair = false;
            }
        }
        else if (playerPosition.y >= 25)
        {
            StairDirectionDown = UpDown[1].position.x - npcPosition.x;
            StairDirectionDown = (StairDirectionDown < 0) ? -1 : 1; //1F direction
            transform.localScale = new Vector3(StairDirectionDown, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, UpDown[1].position, 0.005f);
            if (npcPosition.x == UpDown[1].position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, UpDown[0].position, 0.005f);
                GoStair = false;
            }
        }
        StartCoroutine(Move());
    }
    protected virtual IEnumerator Dead()
    {
        yield return null;
    }


}
