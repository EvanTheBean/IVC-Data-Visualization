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
    Connected,
    Color,
    Size,
    ShowOnClick
}

public enum ConnectedTypes
{
    Time,
    Visible,
    TimeLines,
    VisibleLines
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

    [HideInInspector]
    public List<gradientTypes> gTypes = new List<gradientTypes>();
    [HideInInspector]
    public List<catagoricalGradientsNames> catagoricalGradientsNames = new List<catagoricalGradientsNames>();
    [HideInInspector]
    public List<sequentialGradientsNames> sequentialGradientsNames = new List<sequentialGradientsNames>();
    [HideInInspector]
    public List<divergingGradientsNames> divergingGradientsNames = new List<divergingGradientsNames>();

    public List<string> path = new List<string>(1);

    public bool check, hiding,dataRead,dataLoaded;

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

        path.Clear();
    }
}