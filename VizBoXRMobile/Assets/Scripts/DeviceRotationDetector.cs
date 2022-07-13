using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class RotationEvent : UnityEvent { }

public class DeviceRotationDetector : MonoBehaviour
{
    [SerializeField] RotationEvent OnRotateHorizontal;
    [SerializeField] RotationEvent OnRotateVertical;

    DeviceOrientation lastOrientation;

    // Update is called once per frame
    void Update()
    {
        DeviceOrientation orientation = Input.deviceOrientation;
        if (lastOrientation != orientation && orientation != DeviceOrientation.Unknown)
        {
            lastOrientation = orientation;

            if (orientation == DeviceOrientation.Portrait || orientation ==  DeviceOrientation.PortraitUpsideDown)
            {
                OnRotateVertical?.Invoke();
            }

            if (orientation == DeviceOrientation.LandscapeLeft || orientation == DeviceOrientation.LandscapeRight)
            {
                OnRotateHorizontal?.Invoke();
            }
        }
    }
}
