using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;
using UnityEngine.XR.ARFoundation;
using System;
using Google.XR.ARCoreExtensions;

public class ARSessionManager : MonoBehaviour
{
    static public ARSessionManager Singleton;

    bool lobbyActive = false;

    private void InitLobby(ulong obj)
    {
        if (!lobbyActive)
        {
            lobbyActive = true;
            DebugCanvas.Instance.Log("ARSManager: InitLobby()");
        }
    }

    #region Object Placement

    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    ARRaycastManager arRaycastManager;

    [SerializeField] GameObject objectToPlace;
    [SerializeField] ARAnchor anchorPrefab;

    float placementCooldown = 10f;
    float placementCooldownTimer = 10f;

    void DoPlacementRoutine()
    {
        if (placementCooldownTimer < placementCooldown)
        {
            placementCooldownTimer += Time.deltaTime;
            return;
        }

        // check for touch
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (!FeatureMapQualityIsSufficient())
        {
            UIManager.Singleton.DisplayWarning("Please scan the area more and try again.");
            return;
        }


        // place object on AR plane 
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds))
        {
            var hitPose = hits[0].pose;

            ARAnchor anchor = Instantiate(anchorPrefab, hitPose.position, hitPose.rotation);
            Instantiate(objectToPlace, anchor.transform);
            placementCooldownTimer = 0;

            HostCloudAnchor(anchor);
        }
    }

    void RecreatePlacement(Transform transform) //place prefab at the given anchor
    {
        GameObject placedGameObject = Instantiate(objectToPlace, transform.position, transform.rotation);
        placedGameObject.transform.parent = transform;
    }


    #region Touch Detection

    List<RaycastResult> results = new List<RaycastResult>();

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount == 1)
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

    #endregion

    #region Cloud Anchors

    ARAnchorManager arAnchorManager;
    Pose GetCameraPose()
    {
        return new Pose(Camera.main.transform.position, Camera.main.transform.rotation);
    }

    bool FeatureMapQualityIsSufficient()
    {
        return arAnchorManager.EstimateFeatureMapQualityForHosting(GetCameraPose()) != FeatureMapQuality.Insufficient;
    }

    Queue<(ARCloudAnchor,Transform)> anchorsHosting = new Queue<(ARCloudAnchor,Transform)>();

    public void HostCloudAnchor(ARAnchor pendingHostAnchor)
    {
        DebugCanvas.Instance.Log("ARSManager: HostAnchor call in progress");

        //recommended up to 30 seconds of scanning before calling host anchor
        FeatureMapQuality quality = arAnchorManager.EstimateFeatureMapQualityForHosting(GetCameraPose());

        DebugCanvas.Instance.Log($"ARSManager: Feature Map Quality is: {quality}");

        ARCloudAnchor cloudAnchor = arAnchorManager.HostCloudAnchor(pendingHostAnchor, 1);

        if (cloudAnchor == null)
        {
            DebugCanvas.Instance.Log($"ARSManager: Unable to host cloud anchor {pendingHostAnchor}");
            UIManager.Singleton.DisplayWarning("Object was not able to be hosted on cloud.");
        }
        else
        {
            anchorsHosting.Enqueue((cloudAnchor, pendingHostAnchor.transform.GetChild(0)));
        }

    }

    Queue<ARCloudAnchor> anchorsResolving = new Queue<ARCloudAnchor>();
    public void Resolve(string anchorID)
    {
        DebugCanvas.Instance.Log("Resolve call in progress");

        ARCloudAnchor cloudAnchor = arAnchorManager.ResolveCloudAnchorId(anchorID);

        if (cloudAnchor == null)
        {
            DebugCanvas.Instance.Log($"Unable to resolve cloud anchor {anchorID}");
        }
        else
        {
            anchorsResolving.Enqueue(cloudAnchor);
        }
    }

    bool IsHostingComplete(ARCloudAnchor cloudAnchor, Transform obj)
    {
        CloudAnchorState cloudAnchorState = cloudAnchor.cloudAnchorState;

        if (cloudAnchorState == CloudAnchorState.Success)
        {
            //ADD ANCHOR ID IN LOBBY
            ARLobby.Singleton.AddCloudAnchor(cloudAnchor, obj);
            return true;

        }
        else if (cloudAnchorState != CloudAnchorState.TaskInProgress)
        {
            DebugCanvas.Instance.Log($"Error while hosting cloud anchor: {cloudAnchorState}");
            return true;
        }
        return false;
    }

    bool IsResolvingComplete(ARCloudAnchor cloudAnchor)
    {
        CloudAnchorState cloudAnchorState = cloudAnchor.cloudAnchorState;
        if (cloudAnchorState == CloudAnchorState.Success)
        {
            RecreatePlacement(cloudAnchor.transform);
            ARLobby.Singleton.AddCloudAnchor(cloudAnchor);
            return true;
        }
        else if (cloudAnchorState != CloudAnchorState.TaskInProgress)
        {
            DebugCanvas.Instance.Log($"Error while resolving cloud anchor: {cloudAnchorState}");
            return true;
        }
        return false;
    }



    ARCloudAnchor tempCloudAnchor;
    float safeToResolvePassed = 0f;
    float resolveAnchorPassedTimeout = 10f;
    void DoCloudAnchorRoutine()
    {
        //checking for host result
        if (anchorsHosting.Count > 0)
        {
            tempCloudAnchor = anchorsHosting.Peek().Item1;
            if (IsHostingComplete(tempCloudAnchor, anchorsHosting.Peek().Item2))
            {
                anchorsHosting.Dequeue();
            }
            return;
        }

        //checking for resolve result
        if (anchorsResolving.Count > 0 && safeToResolvePassed <= 0)
        {
            tempCloudAnchor = anchorsResolving.Peek();
            safeToResolvePassed = resolveAnchorPassedTimeout;

            if (IsResolvingComplete(tempCloudAnchor))
            {
                DebugCanvas.Instance.Log($"Resolving AnchorId: {tempCloudAnchor.cloudAnchorId}");
                anchorsResolving.Dequeue();
            }
        }
        else
        {
            safeToResolvePassed -= Time.deltaTime;
        }
    }
   

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Singleton = this;
        }

        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        arAnchorManager = FindObjectOfType<ARAnchorManager>();

        NetworkManager.Singleton.OnClientConnectedCallback += InitLobby;
    }

    // Update is called once per frame
    void Update()
    {
        if (lobbyActive)
        {
            DoPlacementRoutine();
            DoCloudAnchorRoutine();
        }
    }
}
