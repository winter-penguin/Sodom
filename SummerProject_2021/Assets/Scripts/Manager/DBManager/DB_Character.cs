using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct CharacterDB
{
    public enum CharacterType { Gangster, Killer, Prisoner };

    [SerializeField]
    public CharacterType characterType;

    [SerializeField]
    public int id;
    [SerializeField]
    public string crime_name, guilty, character_type;
    [SerializeField]
    public float health, attack_power, attack_speed, attack_range, move_speed, farming_amount, hunger, thirst, fatigue;
}
public class DB_Character : DBManager
{
    [SerializeField]
    public CharacterDB[] characterDB; //스크립트 내에서 character.Length-1의 크기만큼 선언해보려고 했는데 실패함
    
    [SerializeField]
    public string[] character;
    public string sql_string;
    public bool isCharacterDB = false;

    void Start()
    {
        StartCoroutine(GetCharacter());
    }
    

    void Update()
    {
        
    }
    IEnumerator GetCharacter()
    {
        Init("Character_CHG.php");
        yield return StartCoroutine(ConnectDB());
        
        character = queryResult.Split('|'); //| 가 있는 갯수만큼 나눈 문자열들을 배열로 넣은것
        
        for (int i =0; i < character.Length - 1; i++)
        {
            characterDB[i].id = Convert.ToInt32(GetDataValue(character[i], "id:",";"));
            characterDB[i].crime_name = (GetDataValue(character[i], "crime_name:",";"));
            characterDB[i].guilty =(GetDataValue(character[i], "guilty:",";"));
            characterDB[i].character_type = (GetDataValue(character[i], "character_type:",";"));
            characterDB[i].health = Convert.ToSingle(GetDataValue(character[i], "health:",";"));
            characterDB[i].attack_power = Convert.ToSingle(GetDataValue(character[i], "attack_power:",";"));
            characterDB[i].attack_speed = Convert.ToSingle(GetDataValue(character[i], "attack_speed:",";"));
            characterDB[i].attack_range = Convert.ToSingle(GetDataValue(character[i], "attack_range:",";"));
            characterDB[i].move_speed = Convert.ToSingle(GetDataValue(character[i], "move_speed:",";"));
            characterDB[i].farming_amount = Convert.ToSingle(GetDataValue(character[i], "farming_amount:",";"));
            characterDB[i].hunger = Convert.ToSingle(GetDataValue(character[i], "hunger:", ";"));
            characterDB[i].thirst = Convert.ToSingle(GetDataValue(character[i], "thirst:", ";"));
            characterDB[i].fatigue = Convert.ToSingle(GetDataValue(character[i], "fatigue:", ";"));
        }
        isCharacterDB = true;
    }
}
