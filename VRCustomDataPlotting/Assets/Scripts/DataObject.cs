using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject
{
    string[] data;
    
    public DataObject(string[] data)
    {
        this.data = data;
    }

    public string GetDataAt(int index)
    {
        return data[index];
    }

    public int Length()
    {
        return data.Length;
    }
}
