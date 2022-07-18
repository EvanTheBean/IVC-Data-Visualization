using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

[System.Serializable]
public class XREvent : UnityEvent { };

public class ARActivator : MonoBehaviour
{
    public XREvent OnARStart;
    public XREvent OnARStop;
    XRLoader arLoader;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAR());
    }

    private IEnumerator StartAR()
    {
        arLoader = XRGeneralSettings.Instance.Manager.activeLoaders[0];
        Debug.Log("Init XR loader");

        var initSuccess = arLoader.Initialize();
        if (!initSuccess)
        {
            Debug.LogError("Error initializing selected loader.");
        }
        else
        {
            yield return null;
            Debug.Log("Start XR loader");
            var startSuccess = arLoader.Start();
            OnARStart.Invoke();
            LoaderUtility.Initialize();
            if (!startSuccess)
            {
                yield return null;
                Debug.LogError("Error starting selected loader.");
                arLoader.Deinitialize();
            }
        }
    }

    public void ExitAR()
    {
        Debug.Log("Stopping XR Loader...");
        arLoader.Stop();
        arLoader.Deinitialize();
        LoaderUtility.Deinitialize();
        arLoader = null;
        Debug.Log("XR Loader stopped completely.");

        OnARStop?.Invoke();
        Debug.Log("XR deinitialized.");
    }
}
