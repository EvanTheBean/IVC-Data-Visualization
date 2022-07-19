using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkGrapher : NetworkBehaviour
{
    GameObject holderPrefab;
    List<GameObject> latestPoints = new List<GameObject>();

    [ClientRpc]
    void InstantiateHolderClientRpc(Holder holder)
    {
        if (holderPrefab == null) holderPrefab = PrefabManager.Instance.holderPrefab;

        GameObject spawnHolder = Instantiate(holderPrefab);
        var json = JsonUtility.ToJson(holder);
        JsonUtility.FromJsonOverwrite(json, spawnHolder.GetComponent<Holder>());


    }
}
