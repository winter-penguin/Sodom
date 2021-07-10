using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    private Camera camera1;

    private bool isMove;
    private Vector3 destination;
    public float speed = 5;

    public Rigidbody rb;

    Vector3 MousePosition;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            destination = new Vector3(MousePosition.x, transform.position.y,transform.position.z);
            Debug.Log(destination);

            isMove = true; //나중에 애니메이션 할때 필요하겠죠..?
        }
        if (isMove && transform.position != destination)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            /*
            if(transform.position.x > destination.x)
            {
                rb.AddForce(new Vector3(-1, 0, 0));
            }
            else rb.AddForce(new Vector3(1, 0, 0));
            */

        }
        else
        {
            isMove = false;
        }


        //Move();
    }
    /*
    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
    }
    private void Move()
    {
        if (isMove)
        {
            var dir = destination - transform.position;
            transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * 5f;
        }

        if(Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;
        }
    }
    */
}
