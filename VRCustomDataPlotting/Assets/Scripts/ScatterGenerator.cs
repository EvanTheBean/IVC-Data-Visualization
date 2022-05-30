using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGenerator : MonoBehaviour
{
    public static ScatterGenerator Instance { get; private set; }

    [SerializeField] DataPoint dataPointPrefab; 
    List<DataPoint> dataPoints = new List<DataPoint>();
    bool generated = false;

    [SerializeField] Transform scatterParent;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetPositions(int categoryIndex, axis axis)
    {
        if (generated)
        {
            foreach(DataPoint dataPoint in dataPoints)
            {
                dataPoint.SetPosition(categoryIndex, axis);
            }
        }
        else
        {
            foreach (DataObject obj in DataReader.Instance.GetDataObjects())
            {
                DataPoint newDataPoint = Instantiate(dataPointPrefab, scatterParent);
                newDataPoint.SetUp(obj);
                newDataPoint.SetPosition(categoryIndex, axis);
                dataPoints.Add(newDataPoint);

            }
            generated = true;
        }
    }
}
