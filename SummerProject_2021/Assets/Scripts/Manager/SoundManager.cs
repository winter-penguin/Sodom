/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-08-28
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	public struct Speaker // BGM 을 재생하는 스피커인지, SFX를 재생하는 스피커인지 구분하기 위한 구조체
	{
		public GameObject speakerObject;
		public AudioSource speaker;
		public SoundClipKinds soundSort;

		public Speaker(GameObject _gameObject, AudioSource _speaker, SoundClipKinds _soundSort)
		{
			speakerObject = _gameObject;
			speaker = _speaker;
			soundSort = _soundSort;
		}
	}

	[SerializeField] private int firstSoundIndex;

	public enum SoundClipKinds
	{
		BGM,
		SFX
	} // 해당 스피커가 재생하는 소리의 종류

	private AudioSource mainSpeaker; // BGM을 재생하는 메인 스피커
	public List<AudioClip> soundClips = new List<AudioClip>(); // BGM 으로 재생할 음악을 담아두는 리스트
	public List<Speaker> speakers = new List<Speaker>(); // 모든 종류의 스피커를 담아두는 리스트
	public readonly float defaultVolume = 0.5f; // 기본적으로 재생되는 소리의 볼륨 크기

	public void Init()
	{
		if (Camera.main != null)
			mainSpeaker = Camera.main.gameObject.GetComponent<AudioSource>(); // 메인 카메라에 있는 오디오 소스 컴포넌트 가져오기
		SetSoundClips(soundClips, firstSoundIndex);

		speakers.Add(new Speaker(mainSpeaker.gameObject, mainSpeaker, SoundClipKinds.BGM)); // BGM을 재생하는 스피커를 스피커 리스트에 추가

		Button[] tempButtons = FindObjectsOfType<Button>(); // SFX를 재생하는 버튼들을 가져오기
		for (var i = 0; i < tempButtons.Length; i++)
		{
			speakers.Add(new Speaker(tempButtons[i].gameObject, tempButtons[i].GetComponent<AudioSource>(),
				SoundClipKinds.SFX)); // SFX를 재생하는 버튼들의 스피커를 스피커 리스트에 추가
		}

		mainSpeaker.volume = PlayerPrefs.GetFloat("BGMVolume", defaultVolume); // BGM을 재생하는 스피커의 볼륨 설정

		for (var i = 0; i < speakers.Count; i++)
		{
			if (speakers[i].soundSort == SoundClipKinds.SFX)
			{
				speakers[i].speaker.volume =
					PlayerPrefs.GetFloat("SFXVolume", defaultVolume); // BGM 외에 SFX를 재생하는 스피커의 볼륨 설정
			}
		}
	}

	private void Awake()
	{
		Init();
	}


	/// <summary>
	/// 음악을 재생합니다.
	/// </summary>
	/// <param name="clipNumber">재생할 음악 index 번호</param>
	public void SpeakerPlay(int clipNumber)
	{
		if (!mainSpeaker.isPlaying)
		{
			mainSpeaker.clip = soundClips[clipNumber];
			mainSpeaker.Play();
		}
		else
		{
			mainSpeaker.Stop();
			mainSpeaker.clip = soundClips[clipNumber];
			mainSpeaker.Play();
		}
	}

	/// <summary>
	/// 음악을 중지합니다.
	/// </summary>
	public void SpeakerStop()
	{
		mainSpeaker.Stop();
	}

	/// <summary>
	/// 사운드 클립을 재설정하고 새로 음악을 시작합니다.
	/// </summary>
	/// <param name="_soundClips">씬에 재생되는 음악 리스트</param>
	/// <param name="_startClip">처음으로 재생되는 음악 index 번호</param>
	public void SetSoundClips(List<AudioClip> _soundClips, int _startClip)
	{
		SpeakerStop();
		soundClips = _soundClips;
		SpeakerPlay(_startClip);
	}

	/// <summary>
	/// 소리 볼륨 크기를 설정합니다.
	/// </summary>
	/// <param name="_soundKind">0:BGM, 1:SFX</param>
	public void SoundEqualizer(SoundClipKinds _soundKind, float _volume)
	{
		switch (_soundKind)
		{
			case SoundClipKinds.BGM:
				mainSpeaker.volume = _volume;
				PlayerPrefs.SetFloat("BGMVolume", _volume);
				return;
			case SoundClipKinds.SFX:
				for (var i = 0; i < speakers.Count; i++)
				{
					if (speakers[i].soundSort == SoundClipKinds.SFX) { speakers[i].speaker.volume = _volume; }
				}

				PlayerPrefs.SetFloat("SFXVolume", _volume);
				return;
		}
	}

	/// <summary>
	/// 현재 사운드 매니저가 관리하고 있는 스피커 컴포넌트를 가지고 있는 게임 오브젝트 리스트를 가져옵니다.
	/// </summary>
	/// <returns>스피커 컴포넌트를 가지고 있는 버튼 갯수</returns>
	public List<GameObject> GetSpeakerObjects()
	{
		List<GameObject> results = new List<GameObject>();

		for (var i = 0; i < speakers.Count; i++)
		{
			results.Add(speakers[i].speakerObject);
		}
		return results;
	}
}