using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeImage : MonoBehaviour
{
    [SerializeField] private Image speaker;
    [SerializeField] private Sprite muteSpeaker;
    [SerializeField] private Sprite unmuteSpeaker;
    [SerializeField] private Slider volumeSlider;

    private void Init()
    {
        if (volumeSlider.value > volumeSlider.minValue) { speaker.sprite = unmuteSpeaker; }
        else { speaker.sprite = muteSpeaker; }
    }

    private void Awake()
    {
        Init();
    }

    public void SoundCheck()
    {
        if (volumeSlider.value > volumeSlider.minValue) { speaker.sprite = unmuteSpeaker; }
        else { speaker.sprite = muteSpeaker; }
    }
}
