using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

[ExecuteInEditMode]
public class Holder : MonoBehaviour
{
    public List<string> rowNames = new List<string>();
    public List<rowType> rowTypes = new List<rowType>();
    public List<axisType> axisTypes = new List<axisType>();
    public List<GameObject> objects = new List<GameObject>();

    public List<float> axisScales = new List<float>();
    public List<Gradient> axisGradients = new List<Gradient>();
    public List<Vector2> axisMinMax = new List<Vector2>();
    public List<string> connectedTypes = new List<string>();

    public List<string> path = new List<string>(1);

    public bool check, hiding;

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
}