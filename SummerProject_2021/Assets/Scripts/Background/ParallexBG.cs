/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-22
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBG : MonoBehaviour
{
    private MainCharacter player;
    private CameraController cameraInfo;

    public enum BackgroundKinds { mainBuilding, frontBuilding, midBuilding, backBuilding };
    public BackgroundKinds sort;

    private float parallexSpeed;

    private void Init()
    {
        player = GameObject.FindWithTag("Player").GetComponent<MainCharacter>();
        cameraInfo = Camera.main.gameObject.GetComponent<CameraController>();
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        parallexSpeed = CalSpeed();
        StartCoroutine(ParallexMove());
    }

    private float CalSpeed()
    {
        float speed;
        switch (sort)
        {
            case BackgroundKinds.mainBuilding:
                speed = 1;
                break;
            case BackgroundKinds.frontBuilding:
                speed = 0.2f;
                break;
            case BackgroundKinds.midBuilding:
                speed = 0.05f;
                break;
            case BackgroundKinds.backBuilding:
                speed = 0.02f;
                break;
            default:
                speed = 0;
                break;
        }
        return speed;
    }

    private IEnumerator ParallexMove()
    {
        while (player.isLive)
        {
            Vector3 dir = new Vector3(cameraInfo.playerDifferenceX, 0, 0).normalized;
            gameObject.transform.Translate(dir * parallexSpeed * Time.deltaTime) ;

            yield return new WaitForEndOfFrame();
        }
    }
}
