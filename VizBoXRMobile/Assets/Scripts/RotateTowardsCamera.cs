using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-dir);
    }
}