using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[ExecuteInEditMode]
[ExecuteAlways]
public class HeatMapShaderMath : MonoBehaviour
{
    public Material heatMapMaterial;

    float[] mPoints;
    int mHitCount;
    GameObject[] pointsA;
    List<GameObject> points = new List<GameObject>();

    private void Start()
    {
        mPoints = new float[4 * 512];
    }

    private void Update()
    {
        mPoints = new float[4 * 512];
        mHitCount = 0;

        if(transform.hasChanged)
        {
            pointsA = GameObject.FindGameObjectsWithTag("point");
            List<GameObject> points = pointsA.OrderBy(
x => Vector2.SqrMagnitude(x.transform.position - this.transform.position)
).ToList();
            //points.Reverse();
            transform.hasChanged = false;

            //Debug.Log(points.Count);
            for (int i = 0; mHitCount < 512 && i < points.Count; i++)
            {
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(points[i].transform.position);
                Vector3 pointData = Vector3.zero;

                if (screenPoint.z > 0)
                {
                    pointData.x = (screenPoint.x - (Screen.width / 2f)) / (Screen.width / 2f) * 5f;
                    pointData.y = (screenPoint.y - (Screen.height / 2f)) / (Screen.height / 2f) * 5f;
                    pointData.z = points[i].transform.localScale.x;
                    addHitPoint(pointData.x, pointData.y, pointData.z);
                }
            }
            Debug.Log(mHitCount);
        }
    }

    public void addHitPoint(float x, float y, float intensity)
    {
        mPoints[mHitCount * 4] = x;
        mPoints[mHitCount * 4 + 1] = y;
        mPoints[mHitCount * 4 + 2] = 1;
        mPoints[mHitCount * 4 + 3] = intensity;

        mHitCount++;
        //mHitCount %= 512;

        heatMapMaterial.SetFloatArray("_Hits", mPoints);
        heatMapMaterial.SetFloat("_HitCount", mHitCount);
    }

    public void updatePointList()
    {
        pointsA = GameObject.FindGameObjectsWithTag("point");
    }
}
