using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, 3, -10);
    }
}
