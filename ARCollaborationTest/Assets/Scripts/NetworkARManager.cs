using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkARManager : NetworkBehaviour
{
    NetworkVariable<int> cloudAnchorID = new NetworkVariable<int>();

    bool cloudAnchorLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
