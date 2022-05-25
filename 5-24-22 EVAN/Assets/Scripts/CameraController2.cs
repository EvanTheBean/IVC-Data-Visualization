using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Copyright Evan Koppers - 4/25/2022
*/
public class CameraController2 : MonoBehaviour
{
    public bool InvertY, simulate3ButtonMouse;
    public float zoomSpeed, moveSpeed;
    public Vector2 rotSpeeds;
    public Vector2 yRotLimits = new Vector2(-40,60);
    int invert;
    public GameObject selectedObject;
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

        yRotLimits.x *= -1;
        yRotLimits.y = 360 - yRotLimits.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2) || (simulate3ButtonMouse && (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl))))
        {
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
            {
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotSpeeds.x, 0), Space.World);
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotSpeeds.y * invert, 0, 0), Space.Self);

                //Limit angles here :)
                Debug.Log(transform.localEulerAngles);
                if (transform.rotation.eulerAngles.z > 1 || transform.rotation.eulerAngles.z < -1)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                }

                if (transform.rotation.eulerAngles.x < yRotLimits.y && transform.rotation.eulerAngles.x > 180)
                {
                    Debug.Log("Up");
                    transform.rotation = Quaternion.Euler(yRotLimits.y, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }
                else if (transform.rotation.eulerAngles.x > yRotLimits.x && transform.rotation.eulerAngles.x < 180)
                {
                    Debug.Log("Down");
                    transform.rotation = Quaternion.Euler(yRotLimits.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }


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

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(selectedObject != null)
                {
                    //selectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    selectedObject.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(selectedObject.GetComponent("Halo"), false, null);
                    selectedObject.GetComponent<DataPoint>().HideDisplay();
                }

                selectedObject = hit.collider.gameObject;
                //selectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                selectedObject.GetComponent("Halo").GetType().GetProperty("enabled").SetValue(selectedObject.GetComponent("Halo"), true, null);
                selectedObject.GetComponent<DataPoint>().ShowDisplay();
                //Debug.Log("clicked " + selectedObject.name);
            }
        }

        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0 || (simulate3ButtonMouse && Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl)))
        {
            transform.position += transform.forward * (Input.GetAxis("Mouse ScrollWheel") + (Input.GetAxis("Mouse Y") * invert * (simulate3ButtonMouse ? 1f : 0f))) * zoomSpeed;
        }
    }
}
