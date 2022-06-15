using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRPointer : MonoBehaviour
{
    public float defaultLength = 5f;
    public GameObject dot;
    public VRInput inputModule;

    private LineRenderer lr = null;

    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;
    public bool show;
    public bool colored;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        dot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(show)
        {
            UpdateLine();
        }

        if(clickAction.GetStateDown(targetSource))
        {
            show = !show;
            if(!show)
            {
                lr.enabled = false;
                dot.SetActive(false);
            }
            else
            {
                lr.enabled = true;
                dot.SetActive(true);
            }
        }
    }

    private void UpdateLine()
    {
        PointerEventData data = inputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;
        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        dot.transform.position = endPosition;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);

        return hit; 
    }

    public void ColorLine(bool yes)
    {
        if(yes)
        {
	        //Debug.Log("Colored");
            colored = true;
            lr.startColor = Color.red;
        }
        else
        {
            //Debug.Log("UnColored");
            colored = false;
            lr.startColor = Color.white;
        }
    }
}
