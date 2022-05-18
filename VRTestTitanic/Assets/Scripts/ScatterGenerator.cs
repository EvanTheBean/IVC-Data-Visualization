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

        foreach(Passenger passenger in passengers)
        {
            PassengerPoint point = Instantiate(passengerPointPrefab, graphParent);
            point.SetPassenger(passenger);
            point.transform.localPosition = new Vector3(passenger.age / 3, passenger.pClass, 0);
        }
    }
}
