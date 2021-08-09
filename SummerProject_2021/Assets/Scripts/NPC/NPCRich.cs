using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRich : DBNPC
{
    public bool dead = false;

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

}
