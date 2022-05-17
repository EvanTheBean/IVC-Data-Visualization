using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CameraControls cameraControls;
    private void Awake()
    {
        cameraControls = new CameraControls();
    }

    private void OnEnable()
    {
        cameraControls.Enable();
    }

    private void OnDisable()
    {
        cameraControls.Disable();
    }

    public Vector2 GetMouseDelta()
    {
        return cameraControls.Camera.Rotation.ReadValue<Vector2>();
    }
}
