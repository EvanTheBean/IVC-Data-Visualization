using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGenerator : MonoBehaviour
{
    public static ScatterGenerator Instance { get; private set; }

    [SerializeField] int[] maxAxisVal = new int[3];

    [SerializeField] DataPoint dataPointPrefab; 
    List<DataPoint> dataPoints = new List<DataPoint>();
    bool generated = false;

    [SerializeField] Transform scatterParent;

    float[] axisScales = new float[3] { 0.5f, 0.5f, 0.5f };
    float[] normalizeScale = new float[3] { 1f, 1f, 1f };
    int[] axisFactors = new int[3] { -1, -1, -1 };

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
        axisFactors[(int)axis] = categoryIndex;

        if (!generated)
        {
            foreach (DataObject obj in DataReader.Instance.GetDataObjects())
            {
                DataPoint newDataPoint = Instantiate(dataPointPrefab, scatterParent);
                newDataPoint.transform.localPosition = Vector3.zero;
                newDataPoint.SetUp(obj);
                dataPoints.Add(newDataPoint);

            }
            generated = true;
        }

        float maxVal = 0, val;
        foreach (DataPoint dataPoint in dataPoints)
        {
            val = dataPoint.SetPosition(categoryIndex, axis, axisScales[(int)axis] * normalizeScale[(int)axis]);
            if (maxVal < val)
            {
                maxVal = val;
            }
        }

        if (maxVal > maxAxisVal[(int)axis])
        {
            normalizeScale[(int)axis] = maxAxisVal[(int)axis] / maxVal;
            foreach (DataPoint dataPoint in dataPoints)
            {
                val = dataPoint.SetPosition(categoryIndex, axis, axisScales[(int)axis] * normalizeScale[(int)axis]);
            }
        }
        
    }

    public void ChangeAxisScale(float newScale, axis axisToChange)
    {
        axisScales[(int)axisToChange] = newScale;
        if (generated && axisFactors[(int)axisToChange] >= 0)
        {
            foreach (DataPoint dataPoint in dataPoints)
            {
                dataPoint.SetPosition(axisFactors[(int)axisToChange], axisToChange, axisScales[(int)axisToChange] * normalizeScale[(int)axisToChange]);
            }
        }
    }
}
