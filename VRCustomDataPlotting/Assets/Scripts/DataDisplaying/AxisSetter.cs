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
            GameObject obj = other.gameObject;
            AxisBall newAxisBall = null;
            while (newAxisBall == null && obj != null)
            {
                newAxisBall = obj.GetComponentInParent<AxisBall>();
                obj = obj.transform.parent.gameObject;
            }

            if (newAxisBall != axisBall)
            {
                if (axisBall != null)
                {
                    axisBall.transform.SetParent(null);
                    axisBall.Unlock();    
                }

                axisBall = newAxisBall;
                axisBall.transform.SetParent(transform);
                axisBall.transform.localPosition = Vector3.zero;

                axisBall.Lock();
                ScatterGenerator.Instance.SetPositions(axisBall.categoryIndex, axis);
            }
        }
    }
}
