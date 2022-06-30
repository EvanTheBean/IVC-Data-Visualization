using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class ARPlacementManager : MonoBehaviour
{
    public static ARPlacementManager Instance;

    ARRaycastManager arRaycastManager;
    [SerializeField] GameObject placedPrefab;

    GameObject placedGameObject = null;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (placedGameObject != null)
            return;

        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds))
        {
            var hitPose = hits[0].pose;

            placedGameObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            ARAnchor anchor = placedGameObject.AddComponent<ARAnchor>();

            ARCloudAnchorManager.Instance.QueueAnchor(anchor);
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 )
        {
            touchPosition = Input.GetTouch(0).position;

            PointerEventData ptrEventData = new PointerEventData(EventSystem.current);
            ptrEventData.position = touchPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ptrEventData, results);

            return results.Count < 1;
        }
        touchPosition = Vector2.zero;
        return false;
    }

    internal void ReCreatePlacement(Transform transform)
    {
        placedGameObject = Instantiate(placedPrefab, transform.position, transform.rotation);
        placedGameObject.transform.parent = transform;
    }
}
