using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValue : MonoBehaviour
{
    DB_Character db_character;
    private bool isDB_done;
    public float health, attack_power, attack_speed, attack_range, move_speed, farming_amount, hunger, thirst, fatigue;
    // Start is called before the first frame update
    void Start()
    {
        db_character = GameObject.Find("DBManager").GetComponent<DB_Character>();
    }
    void Update()
    {
        if(isDB_done == false)
        {
            if (db_character.isCharacterDB == true)
            {
                health = db_character.characterDB[0].health;
                attack_power = db_character.characterDB[0].attack_power;
                attack_speed = db_character.characterDB[0].attack_speed;
                attack_range = db_character.characterDB[0].attack_range;
                move_speed = db_character.characterDB[0].move_speed;
                farming_amount = db_character.characterDB[0].farming_amount;
                hunger = db_character.characterDB[0].hunger;
                thirst = db_character.characterDB[0].thirst;
                fatigue = db_character.characterDB[0].fatigue;
                isDB_done = true;
            }
        }
        
    }
    public float HpChanged(float damage)
    {
        health += damage;
        return health;
    }
    public float Atk_PowChanged(float damage)
    {
        attack_power += damage;
        return attack_power;
    }
    public float Atk_SpeedChanged(float damage)
    {
        attack_speed += damage;
        return attack_speed;
    }
    public float Atk_RangeChanged(float damage)
    {
        attack_range += damage;
        return attack_range;
    }
    public float Move_SpeedChanged(float damage)
    {
        move_speed += damage;
        return move_speed;
    }
    public float Farm_AmtChanged(float damage)
    {
        attack_range += damage;
        return attack_range;
    }
    public float HungerChanged(float damage)
    {
        hunger += damage;
        return hunger;
    }
    public float ThirstChanged(float damage)
    {
        thirst += damage;
        return thirst;
    }
    public float FatigueChanged(float damage)
    {
        fatigue += damage;
        return fatigue;
    }
    // Update is called once per frame
    
}
