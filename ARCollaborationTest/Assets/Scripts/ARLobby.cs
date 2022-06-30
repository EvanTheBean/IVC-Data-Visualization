using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System;
using Google.XR.ARCoreExtensions;

public class ARLobby : NetworkBehaviour
{
    static public ARLobby Singleton;

    NetworkList<FixedString128Bytes> cloudAnchorIDs;
    Dictionary<FixedString128Bytes, GameObject> anchoredObjects = new Dictionary<FixedString128Bytes, GameObject>();

    private void Start()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Singleton = this;
        }

        cloudAnchorIDs = new NetworkList<FixedString128Bytes>();
        cloudAnchorIDs.OnListChanged += OnCloudIDsChanged;
    }

    private void OnCloudIDsChanged(NetworkListEvent<FixedString128Bytes> changeEvent)
    {
        //IMPLEMENT ADD TO DICTIONARY

        FixedString128Bytes cloudID = changeEvent.Value;
        if (changeEvent.Type == NetworkListEvent<FixedString128Bytes>.EventType.Add && !anchoredObjects.ContainsKey(cloudID)) 
        {
            ARSessionManager.Singleton.Resolve(cloudID.ToString());        
        }
    }

    public void AddCloudAnchor(ARCloudAnchor cloudAnchor)
    {
        anchoredObjects.Add(cloudAnchor.cloudAnchorId, cloudAnchor.transform.GetChild(0).gameObject);
        SetCloudAnchorServerRpc(cloudAnchor.cloudAnchorId);
    }

    [ServerRpc(RequireOwnership = false)]
    void SetCloudAnchorServerRpc(string cloudID, ServerRpcParams rpcParams = default)
    {
        cloudAnchorIDs.Add(cloudID);
    }
    

}
