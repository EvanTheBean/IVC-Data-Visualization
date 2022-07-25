using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestFit
{
    public bool FitIndexed(bool x, bool y, bool z, Holder holder)
    {
        LineRenderer lr = holder.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);
        // Compute the mean of the points.
        Vector2 mean = Vector2.zero;

        for (int i = 0; i < holder.objects.Count; i++)
        {
            if (x && y)
            {
                mean.x += holder.objects[i].transform.position.x;
                mean.y += holder.objects[i].transform.position.y;
            }
            if (x && z)
            {
                mean.x += holder.objects[i].transform.position.x;
                mean.y += holder.objects[i].transform.position.z;
            }
            if (y && z)
            {
                mean.x += holder.objects[i].transform.position.y;
                mean.y += holder.objects[i].transform.position.z;
            }
        }
        mean /= holder.objects.Count;

        if (!float.IsInfinity(mean.x) && !float.IsInfinity(mean.y))
        {
            // Compute the covariance matrix of the points.
            float covar00 = (float)0, covar01 = (float)0;
            int currentIndex = holder.objects.Count;
            for (int i = 0; i < holder.objects.Count; ++i)
            {
                Vector2 diff = Vector2.zero;
                if (x && y)
                {
                    diff = new Vector2(holder.objects[i].transform.position.x, holder.objects[i].transform.position.y) - mean;
                }
                if (x && z)
                {
                    diff = new Vector2(holder.objects[i].transform.position.x, holder.objects[i].transform.position.z) - mean;
                }
                if (y && z)
                {
                    diff = new Vector2(holder.objects[i].transform.position.y, holder.objects[i].transform.position.z) - mean;
                }
                covar00 += diff[0] * diff[0];
                covar01 += diff[0] * diff[1];
            }

            // Decompose the covariance matrix.
            if (covar00 > 0)
            {
                float a = covar01 / covar00;
                if (x && y)
                {
                    Vector3 pos = new Vector3(0, a * (-mean.x) + mean.y, 0);
                    lr.SetPosition(0, pos);
                    pos = new Vector3(mean.x * 2, a * (mean.x) + mean.y, 0);
                    lr.SetPosition(1, pos);
                    return true;
                }
                if (x && z)
                {
                    Vector3 pos = new Vector3(0, 0, a * (-mean.x) + mean.y);
                    lr.SetPosition(0, pos);
                    pos = new Vector3(mean.x * 2, 0, a * (mean.x) + mean.y);
                    lr.SetPosition(1, pos);
                    return true;
                }
                if (y && z)
                {
                    Vector3 pos = new Vector3(0, 0, a * (-mean.x) + mean.y);
                    lr.SetPosition(0, pos);
                    pos = new Vector3(0, mean.x * 2, a * (mean.x) + mean.y);
                    lr.SetPosition(1, pos);
                    return true;
                }
            }
        }


        lr.SetPosition(0, Vector2.zero);
        lr.SetPosition(1, Vector2.zero);
        return false;
    }

    public bool FitIndexed3D(Holder holder)
    {
        // Compute the mean of the points.
        Vector3 point1, point2, point3;
        Vector3 mean = Vector3.zero;
        for (int i = 0; i < holder.objects.Count; ++i)
        {
            mean += holder.objects[i].transform.position;
        }
        mean /= holder.objects.Count;

        if (!float.IsInfinity(mean.x) && !float.IsInfinity(mean.y))
        {
            // Compute the covariance matrix of the points.
            float covar00 = (float)0, covar01 = (float)0, covar02 = (float)0;
            float covar11 = (float)0, covar12 = (float)0;
            int currentIndex = holder.objects.Count;
            for (int i = 0; i < holder.objects.Count; ++i)
            {
                Vector3 diff = holder.objects[i].transform.position - mean;
                covar00 += diff[0] * diff[0];
                covar01 += diff[0] * diff[1];
                covar02 += diff[0] * diff[2];
                covar11 += diff[1] * diff[1];
                covar12 += diff[1] * diff[2];
            }

            // Decompose the covariance matrix.
            float det = covar00 * covar11 - covar01 * covar01;
            if (det != 0)
            {
                float invDet = (float)1 / det;
                //point1 = mean;
                //point2.x = (covar11 * covar02 - covar01 * covar12) * invDet;
                //point2.y = (covar00 * covar12 - covar01 * covar02) * invDet;
                //point2.z = 0;

                float a = (covar11 * covar02 - covar01 * covar12) * invDet;
                float b = (covar00 * covar12 - covar01 * covar02) * invDet;

                point1 = new Vector3(0, 0, a * (-mean.x) + b * (-mean.y) + mean.z);
                point2 = new Vector3(0, ((-mean.z) - a * (-mean.x)) / b + mean.y, 0);
                point3 = new Vector3(((-mean.z) - b * (-mean.y)) / a + mean.x, 0, 0);

                /*
                Debug.Log(point1 + " " + point2 + " " + point3);
                Debug.DrawLine(point1, point2,Color.white, 3);
                Debug.DrawLine(point1, point3, Color.white, 3);
                Debug.DrawLine(point3, point2, Color.white, 3);
                */

                Plane temp = new Plane();
                temp.Set3Points(point1, point2, point3);

                Vector3 sideA = point1 - point2;
                Vector3 sideB = point3 - point2;
                Vector3 planenormal = (Vector3.Cross(sideA, sideB));

                holder.plane.transform.up = planenormal;
                //holder.plane.transform.rotation = Quaternion.LookRotation(temp.normal);
                holder.plane.transform.position = mean;
                //holder.plane.transform.position = point2;

                //Debug.Log(planenormal + " " + holder.plane.transform.rotation.eulerAngles + " " + sideB + " " + mean);

                holder.plane.transform.localScale = mean / 4f;
                //holder.plane.transform.localScale = Vector3.one * 5;

                return true;
            }
        }

        point1 = Vector3.zero;
        point2 = Vector3.zero;
        point3 = Vector3.zero;
        return false;
    }
}
