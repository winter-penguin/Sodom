using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    private enum FieldType
    {
        health,
        hungry,
        thirst,
        fatigue
    }
    
    [SerializeField] private FieldType fieldType;
    private GameManager GM;
    private CharacterValue charValue;
    private float fieldValue;
    private Image imgComp;

    [SerializeField] private Sprite level1;
    [SerializeField] private Sprite level2;
    [SerializeField] private Sprite level3;
    
    private void Init()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        imgComp = gameObject.GetComponent<Image>();
        charValue = FindObjectOfType<CharacterValue>();
    }
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartCoroutine(CheckIconStatus());
    }
    
    /// <summary>
    /// 캐릭터의 수치에 따라 이미지 교체
    /// </summary>
    /// <param name="_value">아이콘이 가리키는 수치 값</param>
    private void IconSpriteChange(float _value)
    {
        if (_value > 80 && imgComp.sprite != level1)
        {
            imgComp.sprite = level1;
        }else if ((_value <= 80 && _value > 30) && imgComp.sprite != level2)
        {
            imgComp.sprite = level2;
        }else if(_value <= 30 && imgComp.sprite != level3)
        {
            imgComp.sprite = level3;
        }
    }

    private IEnumerator CheckIconStatus()
    {
        switch (fieldType)
        {
            case FieldType.health:
                while (!GM.isEnd)
                {
                    IconSpriteChange(charValue.health);
                    
                    yield return null;
                }
                break;
            
            case FieldType.hungry:
                while (!GM.isEnd)
                {
                    IconSpriteChange(charValue.hunger);
                    yield return null;
                }

                break;
            
            case FieldType.thirst:
                while (!GM.isEnd)
                {
                    IconSpriteChange(charValue.thirst);
                    yield return null;
                }

                break;
            
            case FieldType.fatigue :
                while (!GM.isEnd)
                {
                    IconSpriteChange(charValue.fatigue);
                    yield return null;
                }

                break;
        }
    }

 
}
