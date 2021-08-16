using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalGauge : MonoBehaviour
{
    public int Hunger;
    public int Thirst;
    public int Fatigue;
    public bool isHunger;
    public bool isThirst;
    public bool isFatigue;
    private ClockSystem _clockSystem;
    private GameManager GM;

    public int Minute;
    void Start()
    {
        _clockSystem = GameObject.Find("Clock").GetComponent<ClockSystem>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Hunger = 75;
        Thirst = 75;
        Fatigue = 0;
        StartCoroutine(ThirstMinus());
        StartCoroutine(HungerMinus());
    }

    void Update()
    {
        Minute = _clockSystem.DayTime.min;
    }

    IEnumerator ThirstMinus()
    {
        while (!GM.isEnd)
        {
            if (isThirst == false)
            {
                if (_clockSystem.DayTime.min >= 10)
                {
                    Thirst--;
                    isThirst = true;
                    yield return new WaitForSeconds(10f);
                    Debug.Log("woooow");
                }
            }
            yield return new WaitForSeconds(1f);
            isThirst = false;
        }
        
    }
    IEnumerator HungerMinus()
    {
        while (!GM.isEnd)
        {
            if (isHunger == false)
            {
                if (_clockSystem.DayTime.min >= 15)
                {
                    Hunger--;
                    isHunger = true;
                    yield return new WaitForSeconds(10f);
                    Debug.Log("woooow");
                }
            }
            yield return new WaitForSeconds(1f);
            
            isHunger = false;
        }
        
    }
    
}
