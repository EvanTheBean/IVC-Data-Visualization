using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkGrapher : MonoBehaviour
{
    [SerializeField] GameObject dataPointPrefab;
    [SerializeField] GameObject holderPrefab;
    [SerializeField] GameObject rotatorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Holder holder in FindObjectsOfType<Holder>())
        {
            SpawnPoints(holder);
        }
    }

    private void SpawnPoints(Holder holder)
    {

        GameObject netHolder = SpawnHolder(holder);
        DataPoint[] dataPoints = holder.transform.GetComponentsInChildren<DataPoint>();
        foreach (DataPoint dataPoint in dataPoints)
        {
            GameObject netPoint = Instantiate(dataPointPrefab);
            netPoint.name = "NetPoint " + dataPoint.name;
            CopyComponentOnto(dataPoint.transform, netPoint);
            CopyComponentOnto(dataPoint.GetComponent<MeshFilter>(), netPoint);
            CopyComponentOnto(dataPoint.GetComponent<MeshRenderer>(), netPoint);
            CopyComponentOnto(dataPoint.GetComponent<SphereCollider>(), netPoint);
            CopyComponentOnto(dataPoint.GetComponent<LineRenderer>(), netPoint);
            CopyComponentOnto(dataPoint.GetComponent<DataPoint>(), netPoint);
            netPoint.GetComponent<NetworkObject>().Spawn();
            netPoint.transform.SetParent(netHolder.transform);
        }
        GameObject rotator = Instantiate(rotatorPrefab, netHolder.GetComponent<Holder>().CalculateCenterPoint(), Quaternion.identity);
        rotator.GetComponent<NetworkObject>().Spawn();
        netHolder.transform.SetParent(rotator.transform);
    }

    GameObject SpawnHolder(Holder holder)
    {
        GameObject netHolder = Instantiate(holderPrefab);
        CopyComponentOnto(holder, netHolder);
        CopyComponentOnto(holder.GetComponent<LineRenderer>(), netHolder);
        netHolder.GetComponent<NetworkObject>().Spawn();
        return netHolder;
    }

    void CopyComponentOnto<T>(T original, GameObject destination) where T : Component
    {
        if (typeof(T) == typeof(Transform))
        {
            Transform orig = original.transform;
            destination.transform.position = orig.position;
            destination.transform.rotation = orig.rotation;
            destination.transform.localScale = orig.localScale;
            return;
        }

        if (typeof(T) == typeof(LineRenderer))
        {
            LineRenderer line = (LineRenderer)Convert.ChangeType(original, typeof(LineRenderer));
            LineRenderer lineDestination = destination.GetComponent<LineRenderer>();

            if (line == null)
            {
                lineDestination.enabled = false;
                return;
            }

            Vector3[] positions = new Vector3[line.positionCount];
            line.GetPositions(positions);

            foreach(Vector3 vector in positions)
            {
                Debug.Log(vector);
            }    
            lineDestination.SetPositions(positions);

            lineDestination.widthCurve = line.widthCurve;

            return;
        }

        if (destination.GetComponent<T>() == null) destination.AddComponent<T>();
        System.Type type = original.GetType();
        Component copy = destination.GetComponent<T>();

        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }

        if (typeof(T) == typeof(MeshRenderer))
        {
            MeshRenderer renderer = (MeshRenderer)Convert.ChangeType(original, typeof(MeshRenderer));
            destination.GetComponent<MeshRenderer>().material.color = renderer.material.color;
        }
    }

}
