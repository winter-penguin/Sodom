using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject Player;

    public float left;
    public float right;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.transform.position.x > left && Player.transform.position.x < right)
        {
            transform.position = new Vector3(Player.transform.position.x, 3, -10);
        }
        
    }
}
