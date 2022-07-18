using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkGrapher : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DataPoint[] dataPoints = FindObjectsOfType<DataPoint>();
        foreach (DataPoint dataPoint in dataPoints)
        {
            dataPoint.transform.SetParent(null);
            dataPoint.GetComponent<NetworkObject>().Spawn();
        }
        FindObjectOfType<Holder>().GetComponent<NetworkObject>().Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
