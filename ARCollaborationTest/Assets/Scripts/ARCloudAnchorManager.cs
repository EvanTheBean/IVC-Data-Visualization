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
    bool anchorResolved = false;

    AnchorCreatedEvent cloudAnchorCreatedEvent = null;

    //string authToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjgzZWMwMDJjZDY4ZWZiNDQwNzU1NTkwY2RlZjVmMDM1Njk3NGFlODMifQ.eyJpc3MiOiJlbWN0ZXN0QGFyY29sbGFidGVzdC5pYW0uZ3NlcnZpY2VhY2NvdW50LmNvbSIsImF1ZCI6Imh0dHBzOi8vYXJjb3JlY2xvdWRhbmNob3IuZ29vZ2xlYXBpcy5jb20vIiwiZXhwIjoxNjU1MzE5OTI3LCJpYXQiOjE2NTUzMTYzMjcsInN1YiI6ImVtY3Rlc3RAYXJjb2xsYWJ0ZXN0LmlhbS5nc2VydmljZWFjY291bnQuY29tIn0.UgCu2kQ6QvDBjAQF5PCmNCJ23lsK5CNWwzFDZuCjzuYd8C7uZMNmVgAopUv34X831waCxJYNWnnLlN9XeY6gUL-aZDDBwN8Q-sluUSesat_IASL9godQlNqMJnD30A0OJVRQ4wCOWqEFPszoDWPx7bVEqHuCaHyFJyJYq-ffJ5I-NoO1zftUJ4zoMjzzJYcalP2cZB1mKS75BkJ7VBcmkvK2U3XWMIKbvubKTjGPhEKZBn5I_WznsekofaML_-xlOvh6W-7WIYV3QOvfJ8bP5mc3o8YxYLI4t0b-dg7dpiLS1-8ufBvJChTlhtW2ao9l7z5G5rraDwUD2n3zd1rv3Q";

    private void Awake()
    {
#if UNITY_IPHONE
        arAnchorManager.SetAuthToken(authToken);
#endif

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

    public void Resolve(string anchorID)
    {
        anchorIDToResolve = anchorID;
        DebugCanvas.Instance.Log("Resolve call in progress");

        cloudAnchor = arAnchorManager.ResolveCloudAnchorId(anchorID);

        if (cloudAnchor == null)
        {
            DebugCanvas.Instance.Log($"Unable to resolve cloud anchor {anchorID}");
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
            //SET ANCHOR ID IN LOBBY
            //GameObject.FindObjectOfType<ARLobby>().AddCloudAnchor(anchorIDToResolve);


        }
        else if (cloudAnchorState != CloudAnchorState.TaskInProgress)
        {
            DebugCanvas.Instance.Log($"Error while hosting cloud anchor: {cloudAnchorState}");
            anchorHostInProgress = false;
        }
    }

    #endregion


    public void SetAnchorIDToResolve(string id)
    {
        anchorIDToResolve = id;
    }

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
