using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ButtonRemover : MonoBehaviour
{
    // Start is called before the first frame update
    private Image _image;
    public float alpha;
    public GameObject ButtonParent;
    public GameObject mainCharacter;
    public GameObject GaugeBarObject;
    public GameObject HandButton;
    private ClickMovement _clickMovement;
    private WallCollision _wallCollision;
    private GaugeBar _gaugeBar;

    public bool Button_ing;
    public bool isCollide;
    Coroutine co_my_coroutine;
    
    //public bool isClickButton;
    void Start()//잠금 버튼 오브젝트 시작
    {
        _image = this.gameObject.GetComponent<Image>();
        _wallCollision = ButtonParent.GetComponent<WallCollision>();
        _clickMovement = mainCharacter.GetComponent<ClickMovement>();
        _gaugeBar = GaugeBarObject.GetComponent<GaugeBar>();

    }

    public void ButtonRemove()
    {
        if (_wallCollision.iscollide)
        {
            _gaugeBar.StartPercentGauge();
        }
        else
        {
            if (!Button_ing)
            {
                co_my_coroutine = StartCoroutine(WaitUntilButton());
            }
        }
        //StartCoroutine(WaitUntilButton());
        
        
        //image.CrossFadeAlpha(0,1,true);
        //isClickButton = true;
        
    }

    public void StartButtonFade()
    {
        StartCoroutine(ButtonFade());
    }

    public void StopButtonRemove()
    {
        StopCoroutine(co_my_coroutine);
    }

    IEnumerator WaitUntilButton()//버튼이랑 가까워지고, 콜라이더랑 부딪혀서 멈출까지 

    {
        Button_ing = true;
        while (!_wallCollision.iscollide)//문과 닿지 않았을때 아래 반복
        {
            
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Wait for touching button...");
            if (!_clickMovement.isButtonClick)//문과 닿지 않았는데 버튼말고 다른곳을 눌렀을 때 이 코루틴 중지
            {
                StopCoroutine(co_my_coroutine);
                Button_ing = false;
            }
        }
        

        if (_clickMovement.isButtonClick)
        {
            //StartCoroutine(ButtonFade());
            _gaugeBar.StartPercentGauge(); ////게이지바 오브젝트 시작1
            
        }

        Button_ing = false;
    }
    
    //IEnumerator ReachDoor()

    IEnumerator ButtonFade()//이건 중간에 끊키면 안되기때문에 if문 뺌, 게이지UI 실행시키고 띄우기, 그다음 손모양 버튼 활성화
    {
        //_image.CrossFadeAlpha(1,1,true);
        while (_image.color.a > 0)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        HandButton.SetActive(true);
        this.gameObject.SetActive(false);//잠금 버튼 오브젝트 끝
        
    }

    // Update is called once per frame
    void Update()
    {
        alpha = _image.color.a;
        isCollide = _clickMovement.isButtonClick;
        
    }
}
