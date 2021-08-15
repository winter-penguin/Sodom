using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalGauge : MonoBehaviour
{
    public int Hunger;
    public int Thirst;
    public int Fatigue;
    
    void Start()
    {
        Hunger = 75;
        Thirst = 75;
        Fatigue = 0;
    }

    void Update()
    {
        
    }
}
