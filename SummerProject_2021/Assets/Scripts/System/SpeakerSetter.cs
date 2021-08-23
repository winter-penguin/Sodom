/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-23
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSetter : MonoBehaviour
{
    private SoundManager SM;
    private AudioSource sound;
    [SerializeField] private SoundManager.SoundClipKinds soundSort = SoundManager.SoundClipKinds.SFX;

    private void Init()
    {
        SM = FindObjectOfType<SoundManager>();
        sound = GetComponent<AudioSource>();
        if (soundSort == SoundManager.SoundClipKinds.BGM)
        {
            sound.volume = PlayerPrefs.GetFloat("BGMVolume", SM.defaultVolume);
        }else if (soundSort == SoundManager.SoundClipKinds.SFX)
        {
            sound.volume = PlayerPrefs.GetFloat("SFXVolume", SM.defaultVolume);
        }
        SM.speakers.Add(new SoundManager.Speaker(GetComponent<AudioSource>(), soundSort));
    }

    private void Awake()
    {
        Init();
    }
}
