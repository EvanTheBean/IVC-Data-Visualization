using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkGrapher : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("spawning network grapher");
        GetComponent<NetworkObject>().Spawn();
        

        foreach (Holder holder in FindObjectsOfType<Holder>())
        {
            SpawnPoints(holder);
        }
    }

    private void SpawnPoints(Holder holder)
    {
        DataPoint[] dataPoints = holder.transform.GetComponentsInChildren<DataPoint>();

        foreach (DataPoint dataPoint in dataPoints)
        {
            Debug.Log("spawning datapoint " + dataPoint.name);
            DataPoint netPoint = Instantiate(dataPoint);
            //dataPoint.Spawn();
        }
        InstantiateHolderClientRpc(holder);

    }

    [ClientRpc]
    void InstantiateHolderClientRpc(Holder holder)
    {

    }
}
