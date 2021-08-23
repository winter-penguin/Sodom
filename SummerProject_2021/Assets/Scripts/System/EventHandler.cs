
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private GameManager GM;
    private ClockSystem clockSystem;
    private Trader trader;
    
    private static bool isEventing;

    private static readonly int[] TRADER_VISIT_DAYS = new int[4] { 4, 10, 17, 24 };

    private void Init()
    {
        GM = FindObjectOfType<GameManager>();
        clockSystem = FindObjectOfType<ClockSystem>();
        trader = FindObjectOfType<Trader>();
        isEventing = false;
    }
    
    private void Awake()
    {
        Init();
    }

    private IEnumerator TimeChecking()
    {
        while (!GM.isEnd)
        {
            if (TRADER_VISIT_DAYS.Contains(clockSystem.Day) && !isEventing)
            {
                isEventing = true;
                
                
            }
            
            yield return null;
        }
    }
}
