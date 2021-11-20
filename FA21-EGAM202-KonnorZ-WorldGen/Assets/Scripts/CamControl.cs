using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{

    public float speed;
    public float zoomSpeed;
    public float rotateSpeed;

    float maxHeight = 40f;
    float minHeight = 4f;

    Vector2 p1;
    Vector2 p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalSpeed = speed * Input.GetAxis("Horizontal");
        float VerticalSpeed = speed * Input.GetAxis("Vertical");
        float ScrollSpeed = -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");


        Vector3 verticalMove = new Vector3(0, ScrollSpeed, 0);
        Vector3 lateralMove = HorizontalSpeed * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= VerticalSpeed;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        CamRotation();

    }

    public void CamRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            p1 = p2;
        }
    }
}
