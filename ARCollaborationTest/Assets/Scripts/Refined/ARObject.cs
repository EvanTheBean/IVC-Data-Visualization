using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using System;

public class ARObject : MonoBehaviour
{
    public FixedString128Bytes cloudID;

    public virtual void EditObject<T>(string state, T val)
    {
        switch (state)
        {
            case "IMPLEMENT CUSTOM CASE":
                break;
            default:
                DebugCanvas.Instance.Log("State does not exist");
                break;
        }
    }

    protected void SendToServer<T>(string state, T val)
    {
        ARLobby.Singleton.SendValToServer(cloudID, state, val);
    }
}
