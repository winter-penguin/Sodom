/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-22
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    public GameObject[] background;
    private const int BACKGROUND_IMAGE_CNT = 3; // 배경에 사용되는 이미지 갯수
    [SerializeField] private GameObject foreBuilding;
    [SerializeField] private GameObject midBuilding;
    [SerializeField] private GameObject backBuilding;

    public float playerDifferenceX;

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        background = 
            new GameObject[BACKGROUND_IMAGE_CNT] { foreBuilding, midBuilding, backBuilding };
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartCoroutine(PlayerPosDifference());
    }

    private IEnumerator PlayerPosDifference()
    {
        Vector3 playerCurrentPos = player.transform.position;

        while (player.GetComponent<MainCharacter>().isLive)
        {
            playerDifferenceX =player.transform.position.x- playerCurrentPos.x;  // 캐릭터의 현재 위치와 마지막으로 있던 지점의 차이점   (-) <---> (+)
            playerCurrentPos = player.transform.position;
            yield return new WaitForEndOfFrame();
        }
              
    }

}
