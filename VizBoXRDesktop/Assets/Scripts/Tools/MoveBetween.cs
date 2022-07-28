using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoveBetween : MonoBehaviour
{
    public Holder holder;
    public List<Slider> sliders;
    public int num1, num2;
    public bool fullBar, singleChunks;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Slider s in sliders)
        {
            s.maxValue = holder.path.Count - 1;
        }
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
        //Debug.Log(percent);
        if (fullBar)
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
        foreach (GameObject temp in holder.objects)
        {
            DataPoint tempDP = temp.GetComponent<DataPoint>();
            tempDP.currentC = num2;


            for (int j = 0; j < holder.axisTypes.Count; j++)
            {
                //Debug.Log(temp.name + " " + j);
                if (holder.axisTypes[j] == axisType.X)
                {
                    float x = 0;
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        replaceF = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float replaceF2 = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(replaceF, replaceF2, percent);
                        //Debug.Log(x + " " + replaceF  + " " + num1 + " " + replaceF2 + " " + num2 + " " + percent);
                    }
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(num, number, percent);
                        //temp.transform.position = new Vector3(holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);
                    }
                    temp.transform.position = new Vector3(x * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);

                  //  GetMinMax(j);

                    //    CreateAxisRows(new Vector2((holder.axisMinMax[j].x + holder.offsets[j]) / holder.axisScales[j], (holder.axisMinMax[j].y + holder.offsets[j]) / holder.axisScales[j]), new Vector2(holder.offsets[j], holder.axisMinMax[j].y), axisType.X);
                    //Debug.Log(holder.axisMinMax[j].y);

                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        Debug.Log("Bigger X");
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j] + holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        Debug.Log("SmallerX");
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j] + holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);
                    }
                    */
                }
                if (holder.axisTypes[j] == axisType.Y)
                {
                    float x = 0;
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        replaceF = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float replaceF2 = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(replaceF, replaceF2, percent);
                    }
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(num, number, percent);
                        //temp.transform.position = new Vector3(holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);
                    }
                    temp.transform.position = new Vector3(temp.transform.position.x, x * holder.axisScales[j] + holder.offsets[j], temp.transform.position.z);

                    // GetMinMax(j);

                    //CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j] + holder.offsets[j]), axisType.Y);
                    //   CreateAxisRows(new Vector2((holder.axisMinMax[j].x + holder.offsets[j]) / holder.axisScales[j], (holder.axisMinMax[j].y + holder.offsets[j]) / holder.axisScales[j]), new Vector2(holder.offsets[j], holder.axisMinMax[j].y), axisType.Y);
                    //Debug.Log(holder.axisMinMax[j].y);
                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        Debug.Log("Bigger Y");
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        Debug.Log("SmallerY");
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);
                    }
                    */

                }
                if (holder.axisTypes[j] == axisType.Z)
                {
                    float x = 0;
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        replaceF = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float replaceF2 = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(replaceF, replaceF2, percent);
                    }
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(num, number, percent);
                        //temp.transform.position = new Vector3(holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);
                    }
                    temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, x * holder.axisScales[j] + holder.offsets[j]);

                    //                    GetMinMax(j);

                    //CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j] + holder.offsets[j]), axisType.Z);
                    //                  CreateAxisRows(new Vector2((holder.axisMinMax[j].x + holder.offsets[j]) / holder.axisScales[j], (holder.axisMinMax[j].y + holder.offsets[j]) / holder.axisScales[j]), new Vector2(holder.offsets[j], holder.axisMinMax[j].y), axisType.Z);
                    //Debug.Log(holder.axisMinMax[j].y);
                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        Debug.Log("GreaterZ");
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        Debug.Log("SmallerZ");
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);
                    }
                    */
                }
                if (holder.axisTypes[j] == axisType.Height)
                {
                    float x = 0;
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        replaceF = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float replaceF2 = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(replaceF, replaceF2, percent);
                    }
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j] + holder.offsets[j];
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j] + holder.offsets[j];
                        //temp.transform.localScale = new Vector3(temp.transform.localScale.x, holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.z);
                        x = Mathf.Lerp(num, number, percent);
                    }

                    temp.transform.localScale = new Vector3(temp.transform.localScale.x, x * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.z);
                    if (!holder.axisTypes.Contains(axisType.Y) && !holder.centered[j])
                    {
                        temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.localScale.y / 2f, temp.transform.position.z);
                    }
                }
                if (holder.axisTypes[j] == axisType.Width)
                {
                    float x = 0;
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        replaceF = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float replaceF2 = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(replaceF, replaceF2, percent);
                    }
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j] + holder.offsets[j];
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j] + holder.offsets[j];
                        //temp.transform.localScale = new Vector3(temp.transform.localScale.x, holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.z);
                        x = Mathf.Lerp(num, number, percent);
                    }

                    temp.transform.localScale = new Vector3(x * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.y, temp.transform.localScale.z);
                        if (!holder.axisTypes.Contains(axisType.X) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.localScale.x / 2f, temp.transform.position.y, temp.transform.position.z);
                        }
                }
                if (holder.axisTypes[j] == axisType.Length)
                {
                    float x = 0;
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        replaceF = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float replaceF2 = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(replaceF, replaceF2, percent);
                    }
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j] + holder.offsets[j];
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j] + holder.offsets[j];
                        //temp.transform.localScale = new Vector3(temp.transform.localScale.x, holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.z);
                        x = Mathf.Lerp(num, number, percent);
                    }
                    temp.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, x * holder.axisScales[j] + holder.offsets[j]);
                        if (!holder.axisTypes.Contains(axisType.Z) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.localScale.z / 2f);
                        }
                    
                }
                if (holder.axisTypes[j] == axisType.Color)
                {
                    float x = 0;
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        float count = holder.catagories[j].getCount();
                        x = Mathf.Lerp(num, number, percent);
                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate((x + holder.offsets[j]) / count);
                        //Debug.Log((num + (float)holder.offsets[j]) / count);
                    }
                    else if (holder.rowTypes[j] == rowType.Bool)
                    {
                        float num = Convert.ToInt16(bool.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]));
                        float number = Convert.ToInt16(bool.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]));
                        x = Mathf.Lerp(num, number, percent);
                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate((x - holder.axisMinMax[j].x) + holder.offsets[j] / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                    }
                    else if (holder.rowTypes[j] == rowType.Int || holder.rowTypes[j] == rowType.Float)
                    {
                        float num = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        x = Mathf.Lerp(num, number, percent);
                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate((x - holder.axisMinMax[j].x) + holder.offsets[j] / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                    }
                }
                if (holder.axisTypes[j] == axisType.Size)
                {
                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        float count = holder.catagories[j].getCount();
                        temp.transform.localScale = Vector3.one * (Mathf.Lerp(1, holder.axisScales[j], (Mathf.Lerp(num, number, percent) / count) + holder.offsets[j]));
                    }
                    else if (holder.rowTypes[j] == rowType.Bool)
                    {
                        float num = Convert.ToInt16(bool.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]));
                        float number = Convert.ToInt16(bool.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]));
                        float x = Mathf.Lerp(num, number, percent);

                        temp.transform.localScale = Vector3.one * Mathf.Lerp(1, 1 * holder.axisScales[j], x / (holder.axisMinMax[j].y - holder.axisMinMax[j].x)) * holder.offsets[j];
                    }
                    else if (holder.rowTypes[j] == rowType.Int || holder.rowTypes[j] == rowType.Float)
                    {
                        float num = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]);
                        float number = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]);
                        float x = Mathf.Lerp(num, number, percent);

                        temp.transform.localScale = Vector3.one * Mathf.Lerp(1, 1 * holder.axisScales[j], x / (holder.axisMinMax[j].y - holder.axisMinMax[j].x)) * holder.offsets[j];
                    }
                }
                if (holder.axisTypes[j] == axisType.Positional)
                {
                    float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num1]) * holder.axisScales[j] + holder.offsets[j];
                    float number = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][num2]) * holder.axisScales[j] + holder.offsets[j];

                    float x = Mathf.Lerp(num, number, percent);
                    if (!holder.axisTypes.Contains(axisType.X))
                    {
                        temp.transform.position = new Vector3(x + UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j], temp.transform.position.y, temp.transform.position.z);
                    }
                    if (!holder.axisTypes.Contains(axisType.Y))
                    {
                        if (holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Z))
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, x + UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j], temp.transform.position.z);
                        }
                        else
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j], temp.transform.position.z);
                        }
                    }
                    if (!holder.axisTypes.Contains(axisType.Z))
                    {
                        if (holder.axisTypes.Contains(axisType.X))
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, x + UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j]);
                        }
                        else
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j]);
                        }
                    }

                }

                if (holder.bestFit)
                {
                    BestFit best = new BestFit();
                    if (holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Y) && holder.axisTypes.Contains(axisType.Z))
                    {
                        //best.FitIndexed3D(holder);
                        //holder.GetComponent<LineRenderer>().enabled = false;
                        holder.plane.GetComponent<MeshRenderer>().enabled = true;
                    }
                    else if (holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Y))
                    {
                        //XY line
                        //holder.GetComponent<LineRenderer>().enabled = true;
                        //holder.plane.GetComponent<MeshRenderer>().enabled = false;
                        best.FitIndexed(true, true, false, holder);
                    }
                    else if (holder.axisTypes.Contains(axisType.Z) && holder.axisTypes.Contains(axisType.Y))
                    {
                        //holder.GetComponent<LineRenderer>().enabled = true;
                        //holder.plane.GetComponent<MeshRenderer>().enabled = false;
                        best.FitIndexed(false, true, true, holder);
                        //YZ line
                    }
                    else if (holder.axisTypes.Contains(axisType.Z) && holder.axisTypes.Contains(axisType.X))
                    {
                        //holder.GetComponent<LineRenderer>().enabled = true;
                        //holder.plane.GetComponent<MeshRenderer>().enabled = false;
                        best.FitIndexed(true, false, true, holder);
                        //XZ line
                    }
                }
            }







                for (int i = 0; i < holder.axisTypes.Count; i++)
            {
                if (holder.axisTypes[i] == axisType.Lines)
                {
                    List<GameObject> connected = new List<GameObject>(holder.objects);
                    connected = connected.OrderBy(x => x.GetComponent<DataPoint>().variables[holder.rowNames[i].Replace(" ", "")][x.GetComponent<DataPoint>().currentC]).ToList();
                    LineRenderer lr = holder.lineGraph.GetComponent<LineRenderer>();
                    lr.positionCount = connected.Count;
                    for (int j = 0; j < lr.positionCount; j++)
                    {
                        lr.SetPosition(j, connected[j].transform.position);
                    }
                }
                if (!holder.axisTypes.Contains(axisType.Lines) && holder.chartType == ChartType.Line)
                {
                    List<GameObject> connected = new List<GameObject>(holder.objects);
                    if (!holder.xLines && !holder.yLines && !holder.zLines)
                    {
                        connected = connected.OrderBy(x => x.transform.position.magnitude).ToList();
                    }
                    else if (holder.yLines)
                    {
                        connected = connected.OrderBy(x => x.transform.position.y).ToList();
                    }
                    else if (holder.xLines)
                    {
                        connected = connected.OrderBy(x => x.transform.position.x).ToList();
                    }
                    else if (holder.zLines)
                    {
                        connected = connected.OrderBy(x => x.transform.position.z).ToList();
                    }
                    LineRenderer lr = holder.lineGraph.GetComponent<LineRenderer>();
                    lr.positionCount = connected.Count;
                    for (int j = 0; j < lr.positionCount; j++)
                    {
                        lr.SetPosition(j, connected[j].transform.position);
                        //Debug.Log(connected[j].transform.position.magnitude + " " + connected[j].name);
                    }
                }
            }
        }
    }
}
