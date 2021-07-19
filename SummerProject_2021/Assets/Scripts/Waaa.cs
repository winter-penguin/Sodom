using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waaa : MonoBehaviour
{
    private Rigidbody2D rg;
    public int xSpeed = 1;
    public int ySpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
