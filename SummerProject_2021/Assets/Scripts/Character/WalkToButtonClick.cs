using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToButtonClick : MonoBehaviour
{
    public bool Button_ing;
    
    private DoorOpenClose _doorOpenClose;
    
    public GameObject ParentObject;
    private WallCollision _wallCollision;
    
    private ClickMovement _clickMovement;
    public bool isClick;
    Coroutine co_my_coroutine;
    void Start()
    {
        _clickMovement = GameObject.FindWithTag("Player").GetComponent<ClickMovement>();
        
        _doorOpenClose = gameObject.GetComponent<DoorOpenClose>();
        _wallCollision = ParentObject.GetComponent<WallCollision>();
    }

    public void ButtonClick()
    {
        if (_wallCollision.isCollideButton)
        {
            Debug.Log("바꾼다앗");
            _doorOpenClose.BeforAfterChange();
        }
        else
        {
            if (!Button_ing)
            {
                co_my_coroutine = StartCoroutine(WaitUntilButton());
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        isClick = _clickMovement.isButtonClick;
    }
    IEnumerator WaitUntilButton()//버튼이랑 가까워지고, 콜라이더랑 부딪혀서 멈출까지 

    {
        Button_ing = true;
        while (!_wallCollision.isCollideButton)//문과 닿지 않았을때 아래 반복
        {
            
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Wait for touching button...");
            if (!_clickMovement.isButtonClick)//문과 닿지 않았는데 버튼말고 다른곳을 눌렀을 때 이 코루틴 중지
            {
                Debug.Log("야진짜끝!");
                StopCoroutine(co_my_coroutine);
                Button_ing = false;
            }
        }
        

        if (_clickMovement.isButtonClick)
        {
            Debug.Log("시작!");////게이지바 오브젝트 시작1
            _doorOpenClose.BeforAfterChange();
        }

        Button_ing = false;
    }

    
}
