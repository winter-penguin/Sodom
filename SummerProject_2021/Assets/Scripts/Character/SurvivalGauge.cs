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

    private float hunger;

    private float thirst;

    private float fatigue;
    //private ClockSystem _clockSystem;
    private GameManager GM;

    public int Minute;
    void Start()
    {
        //_clockSystem = GameObject.Find("Clock").GetComponent<ClockSystem>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Hunger = 75;
        Thirst = 75;
        Fatigue = 0;
        StartCoroutine(ThirstCoroutine());
        StartCoroutine(HungerCoroutine());
    }

    void Update()
    {
        //Minute = _clockSystem.DayTime.min;
    }
/// <summary>
/// 
/// </summary>
/// <param name="value">변동시킬 수분 값</param>
    public void ThirstMinus(int value)
    {
        Thirst += value;
    }
/// <summary>
/// 
/// </summary>
/// <param name="value">변동시킬 허기 값</param>
    public void HungerMinus(int value)
    {
        Hunger += value;
    }
/// <summary>
/// 
/// </summary>
/// <param name="value">변동시킬 피로 값</param>
    public void FatiguePlus(int value)
    {
        Fatigue += value;
    }

    IEnumerator Thirst_Coroutine()
    {
        while (!GM.isEnd)
        {
            Thirst--;
            
            hunger += Time.timeScale;
            if (Thirst == 0)
            {
                //게임 종료
            }

            yield return new Time();
        }
        
    }
    IEnumerator ThirstCoroutine()
    {
        while (!GM.isEnd)
        {
            Thirst--;
            if (Thirst == 0)
            {
                //게임 종료
            }
            yield return new WaitForSeconds(18.765f);
            Debug.Log("한번");
        }
    }
    IEnumerator HungerCoroutine()
    {
        while (!GM.isEnd)
        {
            Hunger--;
            if (Hunger == 0)
            {
                //게임 종료
            }
            yield return new WaitForSeconds(25f);
        }
    }
    IEnumerator FatigueCoroutine()
    {
        while (!GM.isEnd)//아무행동도 하지 않을 시, 어떤 행동을 하면 이 코루틴 일시정지
        {
            Fatigue--;
            if (Hunger == 0)
            {
                //게임 종료
            }
            yield return new WaitForSeconds(12.51f);
        }
    }
    
}
