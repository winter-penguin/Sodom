using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPoint : MonoBehaviour
{
    [System.Serializable]
    public class Floor
    {
        public Transform up;
        public Transform down;
        public GameObject floor;
    }
    public Floor[] floor;
    private Transform stairstart;
    private Transform stairend;

    private bool isMoving;
    public int speed = 200;
    Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {
        stairstart = floor[0].down;
        stairend = floor[0].up;
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = true;
            
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            rg.isKinematic = true;
            transform.position = Vector2.MoveTowards(transform.position, stairend.position, Time.deltaTime * speed);
        }
        if(transform.position.x > stairend.position.x)
        {
            isMoving = false;
            rg.isKinematic = false;
        }
    }
}
