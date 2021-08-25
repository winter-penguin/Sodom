/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-24
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingPoint : MonoBehaviour
{
    private EventHandler eventHandler;

    private void Init()
    {
        eventHandler = FindObjectOfType<EventHandler>();
    }

    private void Awake()
    {
        Init();
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.TryGetComponent(out CharacterValue charValue))
        {
            if(!eventHandler.IsFarming){eventHandler.IsFarming = true;}
        }
    }
}
