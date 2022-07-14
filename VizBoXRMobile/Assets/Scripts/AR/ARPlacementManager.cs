using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    // Start is called before the first frame update
    void Start()
    {
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

    GameObject placedObject;


    void DoPlacementRoutine()
    {
        if (placedObject!= null) return;

        //if (placementCooldownTimer < placementCooldown)
        //{
        //    placementCooldownTimer += Time.deltaTime;
        //    return;
        //}

        // check for touch
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        DebugCanvas.Instance.Log("1");

        // place object on AR plane 
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds))
        {
            var hitPose = hits[0].pose;
            DebugCanvas.Instance.Log("2");
            ARAnchor anchor = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation);
            placedObject = Instantiate(objectToPlace, anchor.transform);
            DebugCanvas.Instance.Log("3");
            //placementCooldownTimer = 0;
        }
    }


    #region Touch Detection

    List<RaycastResult> results = new List<RaycastResult>();

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;

            //Check for touch hitting canvas instead
            PointerEventData ptrEventData = new PointerEventData(EventSystem.current);
            ptrEventData.position = touchPosition;
            EventSystem.current.RaycastAll(ptrEventData, results);

            return results.Count < 1;
        }
        touchPosition = Vector2.zero;
        return false;
    }

    #endregion
}
