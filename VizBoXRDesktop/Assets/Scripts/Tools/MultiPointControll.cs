using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MultiPointControll : MonoBehaviour
{
    public Holder holder; 
    // Start is called before the first frame update
    void Start()
    {
        holder = GameObject.Find("Holder").GetComponent<Holder>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeBased(bool time)
    {
        
    }

    public void ControlLines(bool show)
    {
        List<GameObject> points = holder.objects;
        foreach (GameObject go in points)
        {
            LineRenderer lr = go.GetComponent<LineRenderer>();
            DataPoint tempDP = go.GetComponent<DataPoint>();

            if (show)
            {

                lr.enabled = true;
                lr.positionCount = holder.path.Count;
                for (int i = 0; i < lr.positionCount; i++)
                {
                    Vector3 temp = Vector3.zero;

                    for (int j = 0; j < holder.axisTypes.Count; j++)
                    {
                        //Debug.Log(holder.rowNames[j]);

                        if (holder.axisTypes[j] == axisType.X)
                        {
                            temp.x = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][i]) * holder.axisScales[j] + holder.offsets[j];
                            //Debug.Log(x);
                        }
                        if (holder.axisTypes[j] == axisType.Y)
                        {
                            temp.y = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][i]) * holder.axisScales[j] + holder.offsets[j];

                        }
                        if (holder.axisTypes[j] == axisType.Z)
                        {
                            temp.z = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][i]) * holder.axisScales[j] + holder.offsets[j];
                        }


                        lr.SetPosition(i, temp);
                    }
                }
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
}
