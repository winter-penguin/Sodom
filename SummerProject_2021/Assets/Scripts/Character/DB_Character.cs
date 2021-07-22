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
    public float health, attack_power, attack_speed, attack_range, move_speed, farming_amount;
}
public class DB_Character : MonoBehaviour
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
        using UnityWebRequest www = UnityWebRequest.Get("http://220.127.167.244:8080/SummerProject_2021/Character_CHG.php");
        //using UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/test/Character_CHG_test.php");

        yield return www.SendWebRequest();
        string sql_string = www.downloadHandler.text;
        character = sql_string.Split('|'); //| 가 있는 갯수만큼 나눈 문자열들을 배열로 넣은것
        
        for (int i =0; i < character.Length - 1; i++)
        {
            characterDB[i].id = Convert.ToInt32(GetDataValue(character[i], "id:"));
            characterDB[i].crime_name = (GetDataValue(character[i], "crime_name:"));
            characterDB[i].guilty =(GetDataValue(character[i], "guilty:"));
            characterDB[i].character_type = (GetDataValue(character[i], "character_type:"));
            characterDB[i].health = Convert.ToSingle(GetDataValue(character[i], "health:"));
            characterDB[i].attack_power = Convert.ToSingle(GetDataValue(character[i], "attack_power:"));
            characterDB[i].attack_speed = Convert.ToSingle(GetDataValue(character[i], "attack_speed:"));
            characterDB[i].attack_range = Convert.ToSingle(GetDataValue(character[i], "attack_range:"));
            characterDB[i].move_speed = Convert.ToSingle(GetDataValue(character[i], "move_speed:"));
            characterDB[i].farming_amount = Convert.ToSingle(GetDataValue(character[i], "farming_amount:"));
        }

        isCharacterDB = true;

        string GetDataValue(string data, string index)//index 뒤에있는 값을 가져오는 함수, ;는 없앤다.
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);
            if (value.Contains(";")) value = value.Remove(value.IndexOf(";"));
            return value;

        }
    }
}
