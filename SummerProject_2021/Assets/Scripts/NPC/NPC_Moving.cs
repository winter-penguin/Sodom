/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-25
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Moving : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject Door;
    public bool isDead;

    public bool activeOver = false; // NPC 고유 기능이 완료 되었는가?
    
    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// NPC 고유의 기능을 수행합니다. 
    /// </summary>
    /// <param name="pos">목적지 포지션</param>
    /// <param name="time">목적지까지 가는데 걸리는 시간</param>
    public virtual void Operate(Vector3 pos, float time)
    {
        activeOver = false;
        StartCoroutine(NPCMoving(pos, time));
    }
    
    /// <summary>
    /// NPC가 지정된 장소로 이동합니다.
    /// </summary>
    /// <param name="targetPos">목적지 포지션</param>
    /// <param name="movingTime">목적지까지 가는데 걸리는 시간</param>
    /// <returns></returns>
    protected virtual IEnumerator NPCMoving(Vector3 targetPos, float movingTime)
    {
        float elapsedTime = 0;
        Vector3 currentPos = transform.position;
        while (movingTime - elapsedTime > Mathf.Epsilon)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / movingTime) * speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        transform.position = targetPos;
        
    }

    public virtual bool NpcTerminate()
    {
        return activeOver = true;
    }
}
