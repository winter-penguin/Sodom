using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectLoader : MonoBehaviour
{
    public SoundManager SM;
    
    private void Init()
    {
        SM = FindObjectOfType<SoundManager>();  // 게임 매니저에 있는 사운드 매니저 스크립트를 가져옵니다.
        SM.Init();  // 사운드 매니저를 새로 초기화합니다.
        SM.SpeakerPlay(0);
    }
    
    private void Start()
    {
        //TODO : 지금 처음으로 메인 씬 로드할때 SM.Init()이 두번 불림. 수정 해야됨.
        Init(); // 씬이 새로 로드 될 때마다 초기화 해줍니다.
        Destroy(gameObject);
    }

    
}
