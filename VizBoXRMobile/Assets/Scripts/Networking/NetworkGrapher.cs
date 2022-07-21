using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkGrapher : MonoBehaviour
{
    int x = 0;
    //TEST
    public void OnButtonPress()
    {
        x += 10;
        GameObject.Find("HOLDER(Clone)").transform.position = new Vector3(x, 0, 0);
    }
}
