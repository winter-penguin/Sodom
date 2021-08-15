using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : NPC_Moving
{
    private Vector3 doorPos;    // 거래 상인이 도달하는 문 위치

    public override void Operate(Vector3 pos)
    {
        base.Operate(pos);
    }
}
