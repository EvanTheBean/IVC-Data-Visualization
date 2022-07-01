using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchable : ARObject
{
    public void SetScale(Vector3 vector3)
    {
        transform.localScale = vector3;
        SendToServer("scale",vector3);
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
