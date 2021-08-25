using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    public Text ProgressIndicator;
    public Image LoadingBar;

    public GameObject BeforeDoor;
    public GameObject AfterDoor;
    public GameObject HandButton;
    
    public GameObject ButtonRemove;
    private WallCollision _wallCollision;
    private ButtonRemover _buttonRemover;
    
    Coroutine co_my_coroutine;
    Coroutine co_my_coroutine_1;
    
    /* GaugeBar에서도 Door의 WallCollision이 필요하고, ButtonRemover에서도 겟컴포넌트 하는데   
    이렇게 여러번 같은 변수를 위해 같은 오브젝트를 겟컴포넌트 하는것이 효율적으로 괜찮은가? */
    
    float currentValue;
    public float speed;
    public float Second = 15;

    void Start()
    {
        _wallCollision = BeforeDoor.GetComponent<WallCollision>();
        _buttonRemover = ButtonRemove.GetComponent<ButtonRemover>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPercentGauge()
    {
        this.gameObject.SetActive(true);
        co_my_coroutine = StartCoroutine(PercentGauge());
        co_my_coroutine_1 = StartCoroutine(GaugeText());
    }
    

    IEnumerator PercentGauge()
    {
        while (currentValue < 100)
        {
            currentValue += speed * Time.deltaTime;
            LoadingBar.fillAmount = currentValue / 100;
            yield return null;
            if (!_wallCollision.iscollide)
            {
                StopCoroutine(co_my_coroutine);
            }
        }
        _buttonRemover.StartButtonFade();
        BeforeDoor.SetActive(false);
        AfterDoor.SetActive(true);
        HandButton.SetActive(true);
        this.gameObject.SetActive(false);
        
        
    }

    IEnumerator GaugeText()
    {
        while (Second >= 0)
        {
            ProgressIndicator.text = Convert.ToInt16(Second).ToString();
            yield return new WaitForSeconds(0.1f);
            Second = Second - 0.1f;
            if (!_wallCollision.iscollide)
            {
                StopCoroutine(co_my_coroutine_1);
            }
        }

        yield return null;

    }
}

