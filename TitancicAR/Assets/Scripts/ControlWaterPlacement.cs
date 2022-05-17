using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class ControlWaterPlacement : MonoBehaviour
{
    public GameObject water;
    private ARSessionOrigin origin;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    bool placedWater, isValid;
    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.FindObjectOfType<ARSessionOrigin>();
        raycastManager = GameObject.FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!placedWater)
        {
            UpdatePlacementPose();
            if(isValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                placedWater = true;
                water.transform.position = placementPose.position;
            }
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        if(hits.Count > 0)
        {
            placementPose = hits[0].pose;
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }
}
