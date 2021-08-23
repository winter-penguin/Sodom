/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-20
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private GameObject trader;
    private GameManager GM;

    private void Init()
    {
        trader = FindObjectOfType<Trader>().gameObject;
        GM = FindObjectOfType<GameManager>();
    }
    
    private void Awake()
    {
        Init();
    }

    private void TraderAppear()
    {
        if (!trader.activeSelf)
        {
            trader.transform.position = new Vector3(-600, 0, 0);
            trader.SetActive(true);
            StartCoroutine(TraderMoving());
        }
    }

    private IEnumerator TraderMoving()
    {
        float TotalTime = 3f;
        float elapsedTime = 0;
        while (elapsedTime < TotalTime)
        {
            
            
            elapsedTime += Time.deltaTime;   
            
            yield return null;
        }
    }
    
    private void Trading()
    {
        
    }
}
