
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    private GameManager GM;
    private ClockSystem clockSystem;
    [SerializeField] private GameObject NPCGroup;
    
    private Trader trader;

    [SerializeField] private Image fadeWindow;
    private bool fadeCheck;
    private Coroutine coFarmingEvent;

    private bool isFarming;
    public bool IsFarming
    {
        get { return isFarming; }
        set
        {
            isFarming = value;
            if (isFarming)
            {
                fadeWindow.gameObject.SetActive(true);
                clockSystem.timeScale = 0;
                if(coFarmingEvent == null) coFarmingEvent = StartCoroutine(FarmingEvent());
            }
            else { clockSystem.timeScale = 1; }
        }
    }

    private bool isTrading;

    public bool IsTrading
    {
        get { return isTrading; }
        set
        {
            isTrading = value;
            if (isTrading)
            {
                StartCoroutine(TradingEvent());
            }
        }
    }
    
    private FarmingSystem farmingSystem;
    
    private void Init()
    {
        GM = FindObjectOfType<GameManager>();
        clockSystem = FindObjectOfType<ClockSystem>();
        if(NPCGroup == null) {NPCGroup = GameObject.Find("NPC_Gruop"/*이름 수정할것*/);}
        trader = NPCGroup.transform.Find("Trader").gameObject.GetComponent<Trader>();
        farmingSystem = FindObjectOfType<FarmingSystem>();
        fadeWindow = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        fadeWindow.gameObject.SetActive(false);
    }
    
    private void Awake()
    {
        Init();
    }

    private IEnumerator TradingEvent()
    {
        trader.TraderAppear();
        yield return StartCoroutine(trader.TraderMoving());
    }

    private IEnumerator FarmingEvent()
    {
        yield return StartCoroutine(FadeEvent(3f));
        farmingSystem.GoFarming();
        clockSystem.Hour += 8;
        coFarmingEvent = null;
        IsFarming = false;
    }

    private IEnumerator FadeEvent(float fadeTime)
    {
        fadeCheck = false;
        
        yield return StartCoroutine(FadeIn(fadeTime));
        if (fadeCheck)
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(FadeOut(fadeTime));
        }
    }

    private IEnumerator FadeIn(float fadeInTime)
    {
        Color tempColor = fadeWindow.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeInTime;
            if (tempColor.a > 1f) { tempColor.a = 1f; }
            fadeWindow.color = tempColor;

            yield return null;
        }

        fadeCheck = true;
    }

    private IEnumerator FadeOut(float fadeOutTime)
    {
        Color tempColor = fadeWindow.color;
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeOutTime;
            if (tempColor.a < 0f) { tempColor.a = 0f; }

            fadeWindow.color = tempColor;

            yield return null;
        }
        
        fadeWindow.gameObject.SetActive(false);
    }
}
