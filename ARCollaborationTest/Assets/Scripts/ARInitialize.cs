using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;

public class ARInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<XRSessionSubsystemDescriptor> sessions = new List<XRSessionSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(sessions);
        Debug.Log("sessions found:" + sessions.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
