using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public enum rowType
{
    String,
    Int,
    Float,
    Bool
}

public enum axisType
{
    None,
    X,
    Y,
    Z,
    Color,
    Size,
    Lines,
    Width,
    Height,
    Length,
    Connected,
    Positional,
    ShowOnClick
}

public enum ConnectedTypes
{
    Time,
    Visible,
    TimeLines,
    VisibleLines
}

public enum ChartType
{
    Scatter,
    Line,
    Bar
}

[ExecuteInEditMode]
public class Holder : NetworkBehaviour
{
    public List<string> rowNames = new List<string>();
    public List<rowType> rowTypes = new List<rowType>();
    public List<axisType> axisTypes = new List<axisType>();
    public List<GameObject> objects = new List<GameObject>();

    public List<float> axisScales = new List<float>();
    public List<Gradient> axisGradients = new List<Gradient>();
    public List<Vector2> axisMinMax = new List<Vector2>();
    public List<ConnectedTypes> connectedTypes = new List<ConnectedTypes>();
    public List<bool> catagorical = new List<bool>();
    public List<float> offsets = new List<float>();
    public List<bool> isCatagorical = new List<bool>();
    public List<float> randomness = new List<float>();
    public List<ListWrapper> catagories = new List<ListWrapper> ();
    public List<bool> centered = new List<bool>();

    [HideInInspector]
    public List<gradientTypes> gTypes = new List<gradientTypes>();
    [HideInInspector]
    public List<catagoricalGradientsNames> catagoricalGradientsNames = new List<catagoricalGradientsNames>();
    [HideInInspector]
    public List<sequentialGradientsNames> sequentialGradientsNames = new List<sequentialGradientsNames>();
    [HideInInspector]
    public List<divergingGradientsNames> divergingGradientsNames = new List<divergingGradientsNames>();

    public List<string> path = new List<string>(1);

    public GameObject plane, lineGraph, XN, YN, ZN, Canvas, AxisX, AxisY, AxisZ;

    public bool check, hiding, dataRead, dataLoaded, bestFit, xN, yN, zN, xLines, yLines, zLines;
    public float Xn, Yn, Zn;
    public float overScale = 1f;
    public Color defaultColor = Color.white;
    public float Dalpha = 1f;

    public ChartType chartType = 0;

    // Start is called before the first frame update
    void Start()
    {
        hiding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            check = false;
            foreach (string name in rowNames)
            {
                Debug.Log(name);
            }
        }
    }

    public void HideAll(GameObject keep)
    {
        Debug.Log("Hiding all but " + keep.name);
        hiding = !hiding;
        foreach (GameObject temp in objects)
        {
            if (temp != keep)
            {
                temp.SetActive(!hiding);
            }
        }
    }

    public void Reset()
    {
        rowNames.Clear();
        rowTypes.Clear();
        axisTypes.Clear();
        objects.Clear();

        axisScales.Clear();
        axisGradients.Clear();
        axisMinMax.Clear();
        connectedTypes.Clear();
        catagorical.Clear();
        offsets.Clear();

        gTypes.Clear();
        catagoricalGradientsNames.Clear();
        sequentialGradientsNames.Clear();
        divergingGradientsNames.Clear();

        isCatagorical.Clear();
        catagories.Clear();
        randomness.Clear();

        centered.Clear();

        path.Clear();

        defaultColor = Color.white;
        overScale = 1f;
        chartType = 0;
        Dalpha = 1f;
    }

    public override void OnNetworkSpawn()
    {
        SendHolderClientRpc(new HolderValues(this));
        SetAxis(new AxisValues(AxisX), new AxisValues(AxisY), new AxisValues(AxisZ));
    }

    public Vector3 CalculateCenterPoint()
    {
        Vector3 centerPoint = Vector3.zero;
        for (int i = 0; i < axisTypes.Count; i++)
        {
            if (axisTypes[i] == axisType.X)
            {
                centerPoint.x = ((axisMinMax[i].y + axisMinMax[i].x) / 2) + offsets[i];
            }
            if (axisTypes[i] == axisType.Y)
            {
                centerPoint.y = ((axisMinMax[i].y + axisMinMax[i].x) / 2) + offsets[i];
            }
            if (axisTypes[i] == axisType.Z)
            {
                centerPoint.z = ((axisMinMax[i].y + axisMinMax[i].x) / 2) + offsets[i];
            }
        }

        return centerPoint;
    }

    [ClientRpc]
    void SendHolderClientRpc(HolderValues vals)
    {
        
    }

    public void SetAxis(AxisValues x, AxisValues y, AxisValues z)
    {
        SetAxisClientRpc(x, y, z);
    }

    [ClientRpc]
    void SetAxisClientRpc(AxisValues x, AxisValues y, AxisValues z)
    {

    }

    private class HolderValues : INetworkSerializable
    {
        public List<string> rowNames = new List<string>();
        public List<rowType> rowTypes = new List<rowType>();
        public List<axisType> axisTypes = new List<axisType>();
        public List<GameObject> objects = new List<GameObject>();

        public List<float> axisScales = new List<float>();
        public List<Gradient> axisGradients = new List<Gradient>();
        public List<Vector2> axisMinMax = new List<Vector2>();
        public List<ConnectedTypes> connectedTypes = new List<ConnectedTypes>();
        public List<bool> catagorical = new List<bool>();
        public List<float> offsets = new List<float>();
        public List<bool> isCatagorical = new List<bool>();
        public List<float> randomness = new List<float>();
        public List<ListWrapper> catagories = new List<ListWrapper>();
        public List<bool> centered = new List<bool>();

        public List<gradientTypes> gTypes = new List<gradientTypes>();
        public List<catagoricalGradientsNames> catagoricalGradientsNames = new List<catagoricalGradientsNames>();
        public List<sequentialGradientsNames> sequentialGradientsNames = new List<sequentialGradientsNames>();
        public List<divergingGradientsNames> divergingGradientsNames = new List<divergingGradientsNames>();

        public List<string> path = new List<string>(1);

        public bool check, hiding, dataRead, dataLoaded, bestFit, xN, yN, zN, xLines, yLines, zLines;
        public float Xn, Yn, Zn;
        public float overScale = 1f;
        public Color defaultColor;

        public ChartType chartType = 0;

        public LineRendererValues lineRenderer = new LineRendererValues();
        public LineRendererValues lineGraph = new LineRendererValues();


        public HolderValues() { }

        public HolderValues(Holder holder)
        {
            rowNames = holder.rowNames;
            rowTypes = holder.rowTypes;
            axisTypes = holder.axisTypes;
            axisScales = holder.axisScales;
            axisGradients = holder.axisGradients;
            axisMinMax = holder.axisMinMax;
            connectedTypes = holder.connectedTypes;
            catagorical = holder.catagorical;
            offsets = holder.offsets;
            isCatagorical = holder.isCatagorical;
            randomness = holder.randomness;
            catagories = holder.catagories;
            centered = holder.centered;
            gTypes = holder.gTypes;
            catagoricalGradientsNames = holder.catagoricalGradientsNames;
            sequentialGradientsNames = holder.sequentialGradientsNames;
            divergingGradientsNames = holder.divergingGradientsNames;
            path = holder.path;

            check = holder.check;
            hiding = holder.hiding;
            dataRead = holder.dataRead;
            dataLoaded = holder.dataLoaded;
            bestFit = holder.bestFit;
            xN = holder.xN;
            yN = holder.yN;
            zN = holder.zN;
            xLines = holder.xLines;
            yLines = holder.yLines;
            zLines = holder.zLines;

            defaultColor = holder.defaultColor;
            chartType = holder.chartType;
            

            if (holder.bestFit)
            {
                lineRenderer = new LineRendererValues(holder.GetComponent<LineRenderer>());
            }
            if (chartType == ChartType.Line)
            {
                lineGraph = new LineRendererValues(holder.lineGraph.GetComponent<LineRenderer>());
            }

        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref rowNames);
            serializer.SerializeValue(ref rowTypes);
            serializer.SerializeValue(ref axisTypes);
            serializer.SerializeValue(ref axisScales);
            serializer.SerializeValue(ref axisGradients);
            serializer.SerializeValue(ref axisMinMax);
            serializer.SerializeValue(ref connectedTypes);
            serializer.SerializeValue(ref catagorical);
            serializer.SerializeValue(ref offsets);
            serializer.SerializeValue(ref isCatagorical);
            serializer.SerializeValue(ref randomness);
            serializer.SerializeValue(ref catagories);
            serializer.SerializeValue(ref centered);
            serializer.SerializeValue(ref gTypes);
            serializer.SerializeValue(ref catagoricalGradientsNames);
            serializer.SerializeValue(ref sequentialGradientsNames);
            serializer.SerializeValue(ref divergingGradientsNames);
            serializer.SerializeValue(ref path);
            serializer.SerializeValue(ref check);
            serializer.SerializeValue(ref hiding);
            serializer.SerializeValue(ref dataRead);
            serializer.SerializeValue(ref dataLoaded);
            serializer.SerializeValue(ref bestFit);
            serializer.SerializeValue(ref xN);
            serializer.SerializeValue(ref yN);
            serializer.SerializeValue(ref zN);
            serializer.SerializeValue(ref xLines);
            serializer.SerializeValue(ref yLines);
            serializer.SerializeValue(ref zLines);
            serializer.SerializeValue(ref defaultColor);
            serializer.SerializeValue(ref chartType);
            serializer.SerializeValue(ref lineRenderer);
            serializer.SerializeValue(ref lineGraph);
        }
    }
}


[System.Serializable]
public class ListWrapper
{
    public List<string> myList;
    public ListWrapper(List<string> temp)
    {
        myList = temp;
    }
    public ListWrapper()
    {
        myList = new List<string>();
    }

    public void ClearList()
    { 
        myList.Clear();
    }

    public int getCount()
    {
        return myList.Count;
    }

    public int IndexOf(string temp)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            if (myList[i] == temp)
            {
                return i;
            }
        }
        return -1;
    }

    public bool Contains(string temp)
    {
        if(IndexOf(temp) == -1)
        {
            return false;
        }
        return true;
    }

    public void Add(string temp)
    {
        myList.Add(temp);
    }
}