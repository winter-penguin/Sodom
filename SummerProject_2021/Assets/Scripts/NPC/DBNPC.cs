using System.Collections;
using UnityEngine;

public class DBNPC : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        DBdata = GameObject.Find("DBManager").GetComponent<DB_Character>();

    }
    void Update()
    {
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
            for(int i = 0; i < DBdata.characterDB.Length; i++)
            {
                if(DBdata.characterDB[i].guilty == CurrentGuilty.ToString())
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
}
