using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : MonoBehaviour
{
    public Holder holder;
    public int num1, num2;
    public bool fullBar, singleChunks;
    // Start is called before the first frame update
    void Start()
    {
        holder = GameObject.FindObjectOfType<Holder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNum1(int newNum1)
    {
        num1 = newNum1;
        fullBar = false;
        singleChunks = false;
    }


    public void SetNum2(int newNum2)
    {
        num2 = newNum2;
        fullBar = false;
        singleChunks = false;
    }

    public void FullBar()
    {
        fullBar = true;
    }

    public void SingleChunks(bool yes)
    {
        singleChunks = yes;
    }

    public void PosLerp(float percent)
    {
        if(fullBar)
        {
            num1 = Mathf.FloorToInt(percent);
            num2 = Mathf.CeilToInt(percent);
            if (!singleChunks)
            {
                percent = percent % 1;
            }
            else
            {
                percent = num1;
            }
        }
        //Debug.Log("yes");
        foreach(GameObject temp in holder.objects)
        {
            DataPoint tempDP = temp.GetComponent<DataPoint>();

            for (int j = 0; j < holder.axisTypes.Count; j++)
            {
                //Debug.Log(holder.rowNames[j]);

                if (holder.axisTypes[j] == axisType.X)
                {
                    float x = Mathf.Lerp(float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j], float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j],percent);
                    temp.transform.position = new Vector3(x, temp.transform.position.y, temp.transform.position.z);
                    //Debug.Log(x);
                }
                if (holder.axisTypes[j] == axisType.Y)
                {
                    float y = Mathf.Lerp(float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j], float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j], percent);
                    temp.transform.position = new Vector3(temp.transform.position.x, y, temp.transform.position.z);
                    //Debug.Log(y);

                }
                if (holder.axisTypes[j] == axisType.Z)
                {
                    float z = Mathf.Lerp(float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j], float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j], percent);
                    temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, z);
                    //Debug.Log(z);
                }
                if (holder.axisTypes[j] == axisType.Color)
                {
                    float col = Mathf.Lerp(float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j], float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j], percent);
                    temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate(col / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                }
                if (holder.axisTypes[j] == axisType.Size)
                {
                    float scale = Mathf.Lerp(float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j], float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j], percent);
                    temp.transform.localScale = Vector3.one * Mathf.Lerp(1, 1 * holder.axisScales[j], scale / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                }
            }
        }
    }
}
