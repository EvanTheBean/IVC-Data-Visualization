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
public class Holder : NetworkBehaviour, INetworkSerializable
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

    public GameObject plane, lineGraph, XN, YN, ZN;

    public bool check, hiding,dataRead,dataLoaded, bestFit,xN, yN, zN, xLines, yLines, zLines;
    public float Xn, Yn, Zn;
    public float overScale = 1f;
    public Color defaultColor;

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
    }

    public override void OnNetworkSpawn()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        Vector3[] linePositions = new Vector3[line.positionCount];
        line.GetPositions(linePositions);
        SendHolderClientRpc(this, line.enabled, linePositions);
    }

    [ClientRpc]
    void SendHolderClientRpc(Holder holder, bool lineEnabled, Vector3[] linePositions)
    {
        
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
        serializer.SerializeValue(ref gTypes);
        serializer.SerializeValue(ref catagoricalGradientsNames);
        serializer.SerializeValue(ref sequentialGradientsNames);
        serializer.SerializeValue(ref divergingGradientsNames);
        serializer.SerializeValue(ref path);
        serializer.SerializeValue(ref isCatagorical);
        serializer.SerializeValue(ref catagories);
        serializer.SerializeValue(ref chartType);

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