using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool InvertY;
    public float zoomSpeed, moveSpeed;
    public Vector2 rotSpeeds;
    int invert;
    // Start is called before the first frame update
    void Start()
    {
        if (InvertY)
        {
            invert = -1;
        }
        else
        {
            invert = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
            {
                //Code for action on mouse moving left
                //print("Mouse moved lr");
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotSpeeds.x, 0), Space.World);
                //Debug.Log(new Vector3(0, Input.GetAxis("Mouse X") * rotSpeeds.x,0));
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                //Code for action on mouse moving left
                //print("Mouse moved ud");
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotSpeeds.y * invert, 0, 0), Space.Self);
                //Debug.Log(new Vector3(Input.GetAxis("Mouse Y") * rotSpeeds.y, 0, 0));
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
            {
                transform.position += transform.right * Input.GetAxis("Mouse X") * moveSpeed;
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                transform.position += transform.up * Input.GetAxis("Mouse Y") * moveSpeed;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0)
        {
            transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }

        /*
         * if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed primary button.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
        */
    }
}
