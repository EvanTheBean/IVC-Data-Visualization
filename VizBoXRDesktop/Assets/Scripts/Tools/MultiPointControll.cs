using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MultiPointControll : MonoBehaviour
{
    Holder holder; 
    // Start is called before the first frame update
    void Start()
    {
        holder = GameObject.Find("Holder").GetComponent<Holder>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeBased(bool time, List<GameObject> points)
    {
        
    }

    public void ControlLines(bool show, List<GameObject> points)
    {
        if(show)
        {
            foreach(GameObject go in points)
            {
                LineRenderer lr = go.GetComponent<LineRenderer>();
                DataPoint dp = go.GetComponent<DataPoint>();

                lr.positionCount = holder.path.Count;
                for(int i = 0; i < lr.positionCount; i++)
                {
                    Vector3 temp = Vector3.zero;




                    lr.SetPosition(i, temp);
                }
            }
        }
    }
}
