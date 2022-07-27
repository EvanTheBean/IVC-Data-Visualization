using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchable : MonoBehaviour
{


    private void Update()
    {
        CheckInput();

    }

    public void SetScale(Vector3 vector3)
    {
        transform.localScale = vector3;
    }


    Touch[] touches;

    float initialDistance;
    Vector3 initialScale;

    void CheckInput()
    {
        if (Input.touchCount >= 2)
        {
            touches = new Touch[Input.touchCount];
            for (int i = 0; i < Input.touchCount; i++)
            {
                touches[i] = Input.GetTouch(i);
            }

            Vector2[] positions = new Vector2[Input.touchCount];
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Ended || touches[i].phase == TouchPhase.Canceled)
                {
                    return;
                }
                positions[i] = touches[i].position;
            }

            Vector2 center = CalcCenterOfPoints(positions);
            if (!(Physics.Raycast(Camera.main.ScreenPointToRay(center), out RaycastHit hit)
                && hit.collider.gameObject == gameObject))
            {
                return;
            }
            else
            {
                //DebugCanvas.Instance.Log("Ray hit stretchable");
            }

            bool noneBegan = true;
            foreach (Touch touch in touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initialDistance = CalcTotalDistFromPoint(positions, center);
                    initialScale = hit.transform.localScale;
                    noneBegan = false;
                }
            }

            if (noneBegan)
            {
                float currentDistance = CalcTotalDistFromPoint(positions, center);
                if (Mathf.Approximately(initialDistance, 0)) return;

                float factor = currentDistance / initialDistance;


                SetScale(initialScale * factor);
            }

        }
    }

    Vector2 CalcCenterOfPoints(Vector2[] points)
    {
        Vector2 total = Vector2.zero;
        for (int i = 0; i < points.Length; i++)
        {
            total += points[i];
        }

        return total / points.Length;
    }

    float CalcTotalDistFromPoint(Vector2[] points, Vector2 point)
    {
        float total = 0;
        foreach (Vector2 pt in points)
        {
            total += Vector2.Distance(point, pt);
        }
        return total;
    }
}
