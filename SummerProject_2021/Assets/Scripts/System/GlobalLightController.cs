/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-24
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] [Range(0, 1)] private float time;
    [SerializeField] private Light2D sunLight;
    private ClockSystem clock;
    

    private void Init()
    {
        sunLight = GetComponent<Light2D>();
        sunLight.color = lightColor.Evaluate(time);
        clock = FindObjectOfType<ClockSystem>();
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        // TODO : 테스트를 위해 clock.Min/59 사용, 본 게임에서는 clock.Hour/23 사용 예정
        time = (float)clock.Min/59;
        sunLight.color = lightColor.Evaluate(time);
    }
}
