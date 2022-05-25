using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSetter : MonoBehaviour
{
    [SerializeField] axis axis;
    AxisBall axisBall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "axisball")
        {
            if (axisBall != null)
            {
                axisBall.transform.SetParent(null);
                axisBall.Unlock();
                axisBall = null;
            }

            GameObject obj = other.gameObject;
            while (axisBall == null && obj != null)
            {
                axisBall = obj.GetComponentInParent<AxisBall>();
                obj = obj.transform.parent.gameObject;
            }

            axisBall.Lock();
            axisBall.transform.SetParent(transform);
            axisBall.transform.position = Vector3.zero;
            ScatterGenerator.Instance.SetPositions(axisBall.categoryIndex, axis);
        }
    }
}
