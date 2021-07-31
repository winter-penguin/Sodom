using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform up;
    public Transform down;

    public float stairspeed = 200f;

    ClickMovement clickmovement;
    // Start is called before the first frame update
    void Awake()
    {
        clickmovement = GetComponent<ClickMovement>();
    }
    void Floor_StairValue()
    {

    }

    // Update is called once per frame
    

}
