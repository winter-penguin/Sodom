using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private ClickMovement _clickMovement;
    public GameObject Player;
    public GameObject SleepingAnimation;
    public bool isSleeping;
    private Image _image;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _clickMovement = GameObject.FindWithTag("Player").GetComponent<ClickMovement>();
        _spriteRenderer = Player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isSleeping)
        {
            if (_clickMovement.isFirst_ing || _clickMovement.isNormalMoving)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
                SleepingAnimation.SetActive(false);
                Time.timeScale = 1.0f;
                isSleeping = false;
            }
        }
    }

    public void StartSleep()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);
        _clickMovement.isNormalMoving = false;
        
        SleepingAnimation.SetActive(true);
        Time.timeScale = 5.0f;
        isSleeping = true;
    }
}
