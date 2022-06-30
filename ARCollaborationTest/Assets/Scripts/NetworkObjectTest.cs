using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkObjectTest : NetworkBehaviour
{
    NetworkVariable<int> playerCount = new NetworkVariable<int>();
    NetworkVariable<float> random = new NetworkVariable<float>();

    public override void OnNetworkSpawn()
    {
        playerCount.Value = 0;
        DebugCanvas.Instance.Log("OnNetworkSpawn()");
        if (NetworkManager.Singleton.IsServer)
        {
            
            ChangeRandom();
            DebugCanvas.Instance.Log("am Owner");
        }
    }

    [ServerRpc(RequireOwnership =false)]
    void SetRandomServerRpc(ServerRpcParams rpcParams = default)
    {
        //if (!IsOwner)
        //{
        //    GetComponent<NetworkObject>().ChangeOwnership()
        //}

        random.Value = Random.Range(-3f, 3f);
    }


    public void ChangeRandom()
    {
        SetRandomServerRpc();
        DebugCanvas.Instance.Log(random.Value);
    }
    
    public void WhatIsRandom()
    {
        DebugCanvas.Instance.Log(random.Value);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
