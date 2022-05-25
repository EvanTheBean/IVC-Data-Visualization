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
    Color,
    Size,
    ShowOnClick
}

[ExecuteInEditMode]
public class Holder : MonoBehaviour
{
    public  List<string> rowNames = new List<string>();
    public  List<rowType> rowTypes = new List<rowType>();
    public  List<axisType> axisTypes = new List<axisType>();
    public  List<GameObject> objects = new List<GameObject>();

    public List<float> axisScales = new List<float>();
    public List<Gradient> axisGradients = new List<Gradient>();
    public List<Vector2> axisMinMax= new List<Vector2>();

    public string path;

    public bool check;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            check = false;
            foreach (string name in rowNames)
            {
                Debug.Log(name);
            }
        }
    }
}
