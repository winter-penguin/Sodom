/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-23
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeEqualizer : MonoBehaviour
{
    private SoundManager SM;
    [SerializeField] private SoundManager.SoundClipKinds soundSort;
    private Slider volume;
    
    private void Init()
    {

        SM = FindObjectOfType<SoundManager>();
        volume = GetComponent<Slider>();
        if (soundSort == SoundManager.SoundClipKinds.BGM)
        {
            volume.value = PlayerPrefs.GetFloat("BGMVolume", SM.defaultVolume);
        }else if (soundSort == SoundManager.SoundClipKinds.SFX)
        {
            volume.value = PlayerPrefs.GetFloat("SFXVolume", SM.defaultVolume);
        }
        
        SM.SoundEqualizer(soundSort, volume.value);
    }

    private void Awake()
    {
        Init();
    }

    public void SetSoundVolume()
    {
        SM.SoundEqualizer(soundSort, volume.value);
    }
}
