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
    private const int BACKGROUND_IMAGE_CNT = 3; // ��濡 ���Ǵ� �̹��� ����
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

        while (/*player.GetComponent<MainCharacter>().isLive*/true)
        {
            playerDifferenceX =player.transform.position.x- playerCurrentPos.x;  // ĳ������ ���� ��ġ�� ���������� �ִ� ������ ������   (-) <---> (+)
            playerCurrentPos = player.transform.position;
            yield return new WaitForEndOfFrame();
        }
              
    }

}
