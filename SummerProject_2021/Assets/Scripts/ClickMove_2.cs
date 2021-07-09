using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove_2 : MonoBehaviour
{
    private Camera camera1;
    public float speed = 10f;

    private Vector3 destination;

    Vector2 lastClickedPos;

    bool isMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(camera1.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                SetDestination(hit.point);
                isMove = true;
            }
        }
        if (isMove && (Vector3)transform.position != destination)
        {
            //float step = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
        }
        else
        {
            isMove = false;
        }
    }
    private void SetDestination(Vector3 dest)
    {
        destination = dest;
    }
}
