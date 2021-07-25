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
    private float movingTime;
    public Vector3 targetPos;

    protected float CurrentTime
    {
        get { return CurrentTime;}
        set
        {
            CurrentTime = value;
            if (CurrentTime % 3 == 0 && CurrentTime != 0) // 
            {
                StartCoroutine(NPCMoving());
            }
        }
    }

    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(NPC_Clock());
        StartCoroutine(NPCMoving());
    }

    protected virtual IEnumerator NPC_Clock()
    {
        while (isDead)
        {
            CurrentTime = GM.Day;
            yield return null;
        }
    }
    
    protected virtual IEnumerator NPCMoving()
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
}
