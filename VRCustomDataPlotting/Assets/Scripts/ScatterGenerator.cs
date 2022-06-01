using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGenerator : MonoBehaviour
{
    public static ScatterGenerator Instance { get; private set; }

    [SerializeField] int axisTickCount = 4;
    [SerializeField] UIAxisLabel axisTickPrefab;
    [SerializeField] int[] maxAxisVal = new int[3];

    [SerializeField] DataPoint dataPointPrefab; 
    List<DataPoint> dataPoints = new List<DataPoint>();
    bool generated = false;

    [SerializeField] Transform scatterParent;

    const float DEFAULT_NORMSCALE = 1;
    float[] axisScales = new float[3] { 0.5f, 0.5f, 0.5f };
    float[] normalizeScale = new float[3] { DEFAULT_NORMSCALE, DEFAULT_NORMSCALE, DEFAULT_NORMSCALE};
    int[] axisFactors = new int[3] { -1, -1, -1 };

    List<UIAxisLabel> axisLabels = new List<UIAxisLabel>();

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

    void GeneratePoints()
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

    public void SetPositions(int categoryIndex, axis axis)
    {
        axisFactors[(int)axis] = categoryIndex;
        normalizeScale[(int)axis] = DEFAULT_NORMSCALE;
        if (!generated)
        {
            GeneratePoints();
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
        UpdateAxisLabels(axis);
    }

    public void ChangeAxisScale(float newScale, axis axisToChange)
    {
        axisScales[(int)axisToChange] = newScale;
        if (generated && (axisFactors[(int)axisToChange] >= 0))
        {
            foreach (DataPoint dataPoint in dataPoints)
            {
                dataPoint.SetPosition(axisFactors[(int)axisToChange], axisToChange, axisScales[(int)axisToChange] * normalizeScale[(int)axisToChange]);
            }
            UpdateAxisLabels(axisToChange);
        }
    }

    void SpawnAxisTicks(axis axis)
    {
        Vector3 instPos = Vector3.zero;
        for (int i = 0; i < axisTickCount; i++)
        {
            switch (axis)
            {
                case axis.X:
                    instPos = new Vector3((float)((i + 1) * maxAxisVal[(int)axis]) / (axisTickCount + 1), 0, -0.25f);
                    break;
                case axis.Y:
                    instPos = new Vector3(0, (float)((i + 1) * maxAxisVal[(int)axis]) / (axisTickCount + 1), -0.25f);
                    break;
                case axis.Z:
                    instPos = new Vector3(-0.25f, 0, (float)((i + 1) * maxAxisVal[(int)axis]) / (axisTickCount + 1));
                    break;

            }
            UIAxisLabel axisTick = Instantiate(axisTickPrefab, scatterParent);
            axisTick.transform.localPosition = instPos;
            axisTick.axis = axis;
            axisTick.SetDefaultVal();
            axisLabels.Add(axisTick);
        }
    }

    bool CheckAxisTicks(axis axis)
    {
        bool valid = false;
        foreach (UIAxisLabel label in axisLabels)
        {
            if (label.axis == axis)
            {
                valid = true;
            }
        }
        return valid;
    }

    void UpdateAxisLabels(axis axis)
    {
        if (!CheckAxisTicks(axis))
        {
            SpawnAxisTicks(axis);
        }

        foreach(UIAxisLabel label in axisLabels)
        {
            if (label.axis == axis)
            {
                label.UpdateScale(axisScales[(int)axis] * normalizeScale[(int)axis]);
            }
        }
    }
}
