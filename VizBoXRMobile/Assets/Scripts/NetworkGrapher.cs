using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkGrapher : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DataPoint[] dataPoints = FindObjectsOfType<DataPoint>();
        foreach (DataPoint dataPoint in dataPoints)
        {
            dataPoint.GetComponent<NetworkObject>().Spawn();
            dataPoint.transform.SetParent(null);
        }
        FindObjectOfType<Holder>().GetComponent<NetworkObject>().Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
