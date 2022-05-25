using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum axis
{
    X,
    Y,
    Z
}

public class DataPoint : MonoBehaviour
{
    DataObject data;
    
    public void SetUp(DataObject dataObj)
    {
        data = dataObj;
    }

    public void SetPosition(int categoryIndex, axis axis)
    {
        Vector3 newPos = transform.position;
        
        if (!float.TryParse(data.GetDataAt(categoryIndex), out float val))
        {
            Debug.Log("no data for " + data.GetDataAt(0) + " found at index " + categoryIndex.ToString());
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        switch (axis)
        {
            case axis.X:
                newPos.x = val;
                break;
            case axis.Y:
                newPos.y = val;
                break;
            case axis.Z:
                newPos.z = val;
                break;
        }

        transform.position = newPos;
    }

}
