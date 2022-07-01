using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System;
using Google.XR.ARCoreExtensions;
using System.Linq;

public class ARLobby : NetworkBehaviour
{
    static public ARLobby Singleton;

    NetworkList<FixedString128Bytes> cloudAnchorIDs;
    Dictionary<FixedString128Bytes, ARObject> anchoredObjects = new Dictionary<FixedString128Bytes, ARObject>();

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

        FixedString128Bytes cloudID = changeEvent.Value;
        if (changeEvent.Type == NetworkListEvent<FixedString128Bytes>.EventType.Add && !anchoredObjects.ContainsKey(cloudID))
        {
            anchoredObjects.Add(cloudID, null);
            ARSessionManager.Singleton.Resolve(cloudID.ToString());
        }
    }

    public void AddCloudAnchor(ARCloudAnchor cloudAnchor)
    {
        ARObject arObj = cloudAnchor.transform.GetComponentInChildren<ARObject>();
        if (arObj == null) DebugCanvas.Instance.Log("ERROR: ARLobby Line 45 arObj is null");

        arObj.cloudID = cloudAnchor.cloudAnchorId;

        if (!anchoredObjects.ContainsKey(cloudAnchor.cloudAnchorId))
        {
            anchoredObjects.Add(cloudAnchor.cloudAnchorId, arObj);
            SetCloudAnchorServerRpc(cloudAnchor.cloudAnchorId);
        }
        else
        {
            anchoredObjects[cloudAnchor.cloudAnchorId] = arObj;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SetCloudAnchorServerRpc(string cloudID, ServerRpcParams rpcParams = default)
    {
        cloudAnchorIDs.Add(cloudID);
    }


    // USE TO SEND VALUES TO SERVER AND DISTRIBUTE TO ALL PLAYERS
    public void SendValToServer<T>(FixedString128Bytes cloudID, string state, T val)
    {
        // RPC functions cannot be generic so...
        // After adding a case scroll down to the rpc functions and copy+paste the two and change their types

        switch (val)
        {
            case int _:
                SendChangedValueToServerRpc(cloudID, state, (int)Convert.ChangeType(val, typeof(int)));
                break;

            case Vector3 _:
                SendChangedValueToServerRpc(cloudID, state, (Vector3)Convert.ChangeType(val, typeof(T)));
                break;

            case string _:
                SendChangedValueToServerRpc(cloudID, state, (string)Convert.ChangeType(val, typeof(T)));
                break;

            case float _:
                SendChangedValueToServerRpc(cloudID, state, (float)Convert.ChangeType(val, typeof(T)));
                break;

            case Vector2 _:
                SendChangedValueToServerRpc(cloudID, state, (Vector2)Convert.ChangeType(val, typeof(T)));
                break;

            case List<int> _:
                SendChangedValueToServerRpc(cloudID, state, new SerializableIntList((List<int>)Convert.ChangeType(val, typeof(T))));
                break;

            case List<string> _:
                SendChangedValueToServerRpc(cloudID, state, new SerializableStringList((List<string>)Convert.ChangeType(val, typeof(T))));
                break;

            case List<float> _:
                SendChangedValueToServerRpc(cloudID, state, new SerializableFloatList((List<float>)Convert.ChangeType(val, typeof(T))));
                break;

            default:
                Debug.LogError($"ARLobby|73 ERROR: Type {typeof(T)} not implemented for server value send.");
                DebugCanvas.Instance.Log($"ARLobby|73 ERROR: Type {typeof(T)} not implemented for server value send.");
                break;
        }
    }

    //================== RPC FUNCTIONS FOR SENDING VALUES BELOW ==================

    // INT
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, int val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, int val)
    {
        anchoredObjects[cloudID].EditObject(state, val);
    }

    // VECTOR 3
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, Vector3 val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, Vector3 val)
    {
        anchoredObjects[cloudID].EditObject(state, val);
    }

    // FLOAT
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, float val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, float val)
    {
        anchoredObjects[cloudID].EditObject(state, val);
    }

    // STRING
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, string val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, string val)
    {
        anchoredObjects[cloudID].EditObject(state, val);
    }

    // LIST <INT>
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, SerializableIntList val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, SerializableIntList val)
    {
        anchoredObjects[cloudID].EditObject(state, val.ToList());
    }

    // LIST <String>
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, SerializableStringList val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, SerializableStringList val)
    {
        anchoredObjects[cloudID].EditObject(state, val.ToList());
    }

    // LIST <Float>
    [ServerRpc(RequireOwnership = false)]
    void SendChangedValueToServerRpc(FixedString128Bytes cloudID, string state, SerializableFloatList val)
    {
        ChangeARObjectValueClientRpc(cloudID, state, val);
    }

    [ClientRpc]
    void ChangeARObjectValueClientRpc(FixedString128Bytes cloudID, string state, SerializableFloatList val)
    {
        anchoredObjects[cloudID].EditObject(state, val.ToList());
    }


    struct SerializableIntList : INetworkSerializable
    {
        public int[] array;
        
        public SerializableIntList(List<int> list)
        {
            array = list.ToArray();
        }

        public List<int> ToList()
        {
            return array.ToList();
        }

        public void NetworkSerialize<X>(BufferSerializer<X> serializer) where X : IReaderWriter
        {
            // Length
            int length = 0;
            if (!serializer.IsReader)
            {
                length = array.Length;
            }

            serializer.SerializeValue(ref length);

            // Array
            if (serializer.IsReader)
            {
                array = new int[length];
            }

            for (int n = 0; n < length; ++n)
            {
                serializer.SerializeValue(ref array[n]);
            }
        }
    }

    struct SerializableStringList : INetworkSerializable
    {
        public string[] array;

        public SerializableStringList(List<string> list)
        {
            array = list.ToArray();
        }

        public List<string> ToList()
        {
            return array.ToList();
        }

        public void NetworkSerialize<X>(BufferSerializer<X> serializer) where X : IReaderWriter
        {
            // Length
            int length = 0;
            if (!serializer.IsReader)
            {
                length = array.Length;
            }

            serializer.SerializeValue(ref length);

            // Array
            if (serializer.IsReader)
            {
                array = new string[length];
            }

            for (int n = 0; n < length; ++n)
            {
                serializer.SerializeValue(ref array[n]);
            }
        }
    }

    struct SerializableFloatList : INetworkSerializable
    {
        public float[] array;

        public SerializableFloatList(List<float> list)
        {
            array = list.ToArray();
        }

        public List<float> ToList()
        {
            return array.ToList();
        }

        public void NetworkSerialize<X>(BufferSerializer<X> serializer) where X : IReaderWriter
        {
            // Length
            int length = 0;
            if (!serializer.IsReader)
            {
                length = array.Length;
            }

            serializer.SerializeValue(ref length);

            // Array
            if (serializer.IsReader)
            {
                array = new float[length];
            }

            for (int n = 0; n < length; ++n)
            {
                serializer.SerializeValue(ref array[n]);
            }
        }
    }
}
