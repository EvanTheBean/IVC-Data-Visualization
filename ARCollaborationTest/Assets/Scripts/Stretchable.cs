using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchable : ARObject
{
    float serverUpdateTimer = 0.1f;
    float serverUpdateTimerCounter = 0f;
    Vector3 lastScaleUpdate;

    private void Start()
    {
        lastScaleUpdate = transform.localScale;
    }

    private void Update()
    {
        serverUpdateTimerCounter += Time.deltaTime;
        if (serverUpdateTimerCounter >= serverUpdateTimer)
        {
            if (transform.localScale != lastScaleUpdate)
            {
                SendToServer("scale", transform.localScale);
                lastScaleUpdate = transform.localScale;
                serverUpdateTimerCounter = 0f;
            }
        }
    }

    public void SetScale(Vector3 vector3)
    {
        transform.localScale = vector3;
    }

    public override void EditObject<T>(string state, T val)
    {
        switch (state)
        {
            case "scale":
                transform.localScale = (Vector3)Convert.ChangeType(val, typeof(Vector3));
                break;
            default:
                DebugCanvas.Instance.Log("hello");
                break;
        }
    }
}
