using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    GraphPositionEditor graph;
    // Start is called before the first frame update
    void Start()
    {
        graph = FindObjectOfType<GraphPositionEditor>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DoPlacementRoutine();
    }

    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    

    [SerializeField] GameObject objectToPlace;
    [SerializeField] ARAnchor anchorPrefab;

    //float placementCooldown = 10f;
    //float placementCooldownTimer = 10f;

    bool placedObject = false;


    void DoPlacementRoutine()
    {
        if (placedObject) 
        {
            Debug.Log("object placed");
            return;
        }

        // check for touch
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        DebugCanvas.Instance.Log("1");

        // place object on AR plane 
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds))
        {
            Pose hitPose = hits[0].pose;

            for (int i = 0; i < hits.Count; i++)
            {
                DebugCanvas.Instance.Log("Pose:" + hits[i].pose.ToString());
                DebugCanvas.Instance.Log("Distance:" + hits[i].distance.ToString());
            }


            graph.PlaceInAR(hits[0].pose);
            placedObject = true;
            DebugCanvas.Instance.Log("3");
            //placementCooldownTimer = 0;
        }
        else
        {
            UIManager.Singleton.DisplayWarning("Please scan area more before placing.");
        }
    }


    #region Touch Detection

    List<RaycastResult> results = new List<RaycastResult>();

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("0");
            touchPosition = Input.GetTouch(0).position;

            //Check for touch hitting canvas instead
            PointerEventData ptrEventData = new PointerEventData(EventSystem.current);
            ptrEventData.position = touchPosition;
            EventSystem.current.RaycastAll(ptrEventData, results);
            Debug.Log(results.Count < 1);
            return results.Count < 1;
        }
        touchPosition = Vector2.zero;
        return false;
    }

    #endregion
}
