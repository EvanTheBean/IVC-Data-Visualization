using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGenerator : MonoBehaviour
{
    [SerializeField] Transform graphParent;
    [SerializeField] PassengerPoint passengerPointPrefab;
    [SerializeField] Material diedPointMaterial;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGraph();
    }

    void GenerateGraph()
    {
        List<Passenger> passengers = GetComponent<DataReader>().GetPassengersList();
        List<Vector3> usedPositions = new List<Vector3>();

        foreach(Passenger passenger in passengers)
        {
            PassengerPoint point = Instantiate(passengerPointPrefab, graphParent);
            point.SetPassenger(passenger);
            if (!passenger.survived)
            {
                point.GetComponentInChildren<MeshRenderer>().material = diedPointMaterial;
            }
            Vector3 pos = new Vector3(passenger.age / 3, (-passenger.pClass + 3) / 2f, 0);
            while (usedPositions.Contains(pos))
            {
                pos.z += 0.3f;
            }
            usedPositions.Add(pos);
            point.transform.localPosition = pos;
        }
    }
}
