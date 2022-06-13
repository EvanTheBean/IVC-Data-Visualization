using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using System;

public class AnchorCreatedEvent : UnityEvent<Transform> { }

public class ARCloudAnchorManager : MonoBehaviour
{
    static public ARCloudAnchorManager Instance;

    [SerializeField] Camera arCamera = null;

    [SerializeField] float resolveAnchorPassedTimeout = 10.0f;
    float safeToResolvePassed = 0;

    ARAnchorManager arAnchorManager = null;

    ARAnchor pendingHostAnchor = null;

    ARCloudAnchor cloudAnchor = null;

    string anchorIDToResolve;

    bool anchorHostInProgress = false;

    bool anchorResolveInProgress = false;

    AnchorCreatedEvent cloudAnchorCreatedEvent = null;

    private void Awake()
    {
        cloudAnchorCreatedEvent = new AnchorCreatedEvent();
        cloudAnchorCreatedEvent.AddListener((t) => ARPlacementManager.Instance.ReCreatePlacement(t));
    }

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
    }

    private Pose GetCameraPose()
    {
        return new Pose(arCamera.transform.position, arCamera.transform.rotation);
    }



    #region Cloud Anchor Cycle

    public void QueueAnchor(ARAnchor anchor)
    {
        pendingHostAnchor = anchor;
    }

    public void HostAnchor()
    {
        DebugCanvas.Instance.Log("HostAnchor call in progress");

        //recommended up to 30 seconds of scanning before calling host anchor
        FeatureMapQuality quality = arAnchorManager.EstimateFeatureMapQualityForHosting(GetCameraPose());

        DebugCanvas.Instance.Log($"Feature Map Quality is: {quality}");

        cloudAnchor = arAnchorManager.HostCloudAnchor(pendingHostAnchor, 1);

        if (cloudAnchor == null)
        {
            DebugCanvas.Instance.Log($"Unable to host cloud anchor {pendingHostAnchor}");
        }
        else
        {
            anchorHostInProgress = true;
        }

    }

    public void Resolve()
    {
        DebugCanvas.Instance.Log("Resolve call in progress");

        cloudAnchor = arAnchorManager.ResolveCloudAnchorId(anchorIDToResolve);

        if (cloudAnchor == null)
        {
            DebugCanvas.Instance.Log($"Unable to resolve cloud anchor {anchorIDToResolve}");
        }
        else
        {
            anchorResolveInProgress = true;
        }

    }

    void CheckResolveProgress()
    {
        CloudAnchorState cloudAnchorState = cloudAnchor.cloudAnchorState;
        if (cloudAnchorState == CloudAnchorState.Success)
        {
            anchorResolveInProgress = false;
            cloudAnchorCreatedEvent?.Invoke(cloudAnchor.transform);
        }
        else if (cloudAnchorState != CloudAnchorState.TaskInProgress)
        {
            DebugCanvas.Instance.Log($"Error while resolving cloud anchor: {cloudAnchorState}");
            anchorResolveInProgress = false;
        }
    }

    void CheckHostingProgress()
    {
        CloudAnchorState cloudAnchorState = cloudAnchor.cloudAnchorState;

        if (cloudAnchorState == CloudAnchorState.Success)
        {
            anchorHostInProgress = false;
            anchorIDToResolve = cloudAnchor.cloudAnchorId;
        }
        else if (cloudAnchorState != CloudAnchorState.TaskInProgress)
        {
            DebugCanvas.Instance.Log($"Error while hosting cloud anchor: {cloudAnchorState}");
            anchorHostInProgress = false;
        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        //checking for host result
        if (anchorHostInProgress)
        {
            CheckHostingProgress();
            return;
        }    

        //checking for resolve result
        if (anchorResolveInProgress && safeToResolvePassed <= 0)
        {
            safeToResolvePassed = resolveAnchorPassedTimeout;

            if (!string.IsNullOrEmpty(anchorIDToResolve))
            {
                DebugCanvas.Instance.Log($"Resolving AnchorId: {anchorIDToResolve}");
                CheckResolveProgress();
            }
        }
        else
        {
            safeToResolvePassed -= Time.deltaTime;
        }
    }
}
