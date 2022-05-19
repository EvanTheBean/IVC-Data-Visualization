using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.InputSystem;

public class ControlWaterPlacement : MonoBehaviour
{
    public GameObject water, displayPoint;
    private ARSessionOrigin origin;
    private ARRaycastManager raycastManager;
    public Pose placementPose;
    bool placedWater, isValid, touched;
    UserBasedSimulation usb;
    private TouchControls tc;

    private void Awake()
    {
        tc = new TouchControls();
    }

    private void OnEnable()
    {
        tc.Enable();
    }

    private void OnDisable()
    {
        tc.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.FindObjectOfType<ARSessionOrigin>();
        raycastManager = GameObject.FindObjectOfType<ARRaycastManager>();
        usb = GameObject.FindObjectOfType<UserBasedSimulation>();
        tc.Touch.TouchInput.started += ctx => Touched();
        placementPose.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(!placedWater)
        {
            UpdatePlacementPose();
            if(isValid && touched)
            {
                placedWater = true;
                water.transform.position = placementPose.position;
                displayPoint.SetActive(false);
                touched = false;
                //usb.height = usb.cHeight - water.transform.position.y;
                Debug.Log("Water Placed at " + water.transform.position);
            }
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        if(hits.Count > 0)
        {
            placementPose = hits[0].pose;
            displayPoint.transform.position = hits[0].pose.position;
            displayPoint.transform.rotation = hits[0].pose.rotation;
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }

    void Touched()
    {
        Debug.Log("It has been touched :)");
        touched = true;
    }
}
