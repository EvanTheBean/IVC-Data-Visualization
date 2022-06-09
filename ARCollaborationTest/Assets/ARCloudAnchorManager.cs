using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class UnityEventResolver : UnityEvent<Transform> { }

public class ARCloudAnchorManager : MonoBehaviour
{
    static public ARCloudAnchorManager Instance;

    [SerializeField] Camera arCamera = null;

    [SerializeField] float resolveAnchorPassedTimeout = 10.0f;
    float safeToResolvePassed = 0;

    ARAnchorManager arAnchorManager = null;

    ARAnchor pendingHostAnchor = null;

    ARCloudAnchor cloudAnchor = null;

    string anchorToResolve;

    bool anchorUpdateInProgress = false;

    bool anchorResolveInProgress = false;

    UnityEventResolver resolver = null;

    private void Awake()
    {
        resolver = new UnityEventResolver();
        resolver.AddListener((t) => ARPlacementManager.Instance.ReCreatePlacement(t));
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

    #region Anchor Cycle



    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
