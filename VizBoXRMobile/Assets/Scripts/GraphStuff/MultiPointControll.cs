using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class MultiPointControll : MonoBehaviour
{
    public Holder holder; 
    // Start is called before the first frame update
    void Start()
    {
        holder = FindObjectOfType<Holder>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeBased(bool time)
    {
        List<GameObject> points = holder.objects;
        if (!time)
        {
            foreach(GameObject p in points)
            {
                DataPoint tempDP = p.GetComponent<DataPoint>();
                while(p.transform.childCount < holder.path.Count + 1)
                {
                    GameObject tempChild  = new GameObject();
                    tempChild.AddComponent<MeshRenderer>();
                    tempChild.GetComponent<MeshRenderer>().sharedMaterial = p.GetComponent<MeshRenderer>().sharedMaterial;
                    tempChild.AddComponent<MeshFilter>();
                    tempChild.GetComponent<MeshFilter>().sharedMesh = p.GetComponent<MeshFilter>().sharedMesh;
                    tempChild.transform.parent = p.transform;
                }

                bool hitCanvas = false;
                int i = 0;
                for (int k = 1; k < holder.path.Count + 1; k++)
                {
                    Transform child = p.transform.GetChild(k);

                    i = k - 1;

                    //Debug.Log(k + " " + i + " " + child.name);

                    if (child.GetComponent<Canvas>() == null)
                    {
                        Vector3 pos = Vector3.zero;


                        for (int j = 0; j < holder.axisTypes.Count; j++)
                        {
                            //Debug.Log(holder.rowNames[j]);

                            if (holder.axisTypes[j] == axisType.X)
                            {
                                pos.x = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][i]) * holder.axisScales[j] + holder.offsets[j];
                                //Debug.Log(x);
                            }
                            if (holder.axisTypes[j] == axisType.Y)
                            {
                                pos.y = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][i]) * holder.axisScales[j] + holder.offsets[j];

                            }
                            if (holder.axisTypes[j] == axisType.Z)
                            {
                                pos.z = float.Parse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][i]) * holder.axisScales[j] + holder.offsets[j];
                            }
                        }

                        //pos = p.transform.position - pos;
                        child.position = Vector3.zero;
                        child.position += pos;
                        child.localScale = Vector3.one;
                        child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        //child.gameObject.GetComponent<LineRenderer>().enabled = false;
                    }
                    else
                    {
                        hitCanvas = true;
                        //Debug.Log("hit canvas :)");
                    }
                }
            }
        }
        else
        {
            foreach (GameObject p in points)
            {
                //Debug.Log("Ugh1");
                foreach (Transform child in p.transform.GetComponentsInChildren<Transform>())
                {
                    //Debug.Log(child.name);
                    //Debug.Log("Ugh2");
                    if (child.GetComponent<MeshRenderer>() != null && child != p.transform)
                    {
                        //Debug.Log("Hiding");
                        child.gameObject.GetComponent<MeshRenderer>().enabled = false;

                    }
                }
            }
        }
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
