using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserBasedSimulation : MonoBehaviour
{
    public GameObject waterPlane;
    public List<Passanger> valid = new List<Passanger>();
    public int dead, total;
    public float percent;
    public Passanger closest;
    public float height, cHeight;
    ControlWaterPlacement cwp;
    public float goalHeight;
    public float fillTime;
    float tempTime;
    bool flowingIn;

    UserData ud;
    DataLoader dl;

    // Start is called before the first frame update
    void Start()
    {
        ud = GetComponent<UserData>();
        dl = GetComponent<DataLoader>();
        //cHeight = Camera.main.gameObject.transform.position.y;// * 1.1f;
        cwp = GameObject.FindObjectOfType<ControlWaterPlacement>();
    }

    public void Update()
    {
        height = Camera.main.gameObject.transform.position.y - cwp.placementPose.position.y;
        if(flowingIn)
        {
            waterPlane.transform.position = new Vector3(0, Mathf.Lerp(cwp.placementPose.position.y, goalHeight, tempTime/fillTime), 0);
            tempTime += Time.deltaTime;
            if(tempTime > fillTime)
            {
                flowingIn = false;
            }
        }
    }

    public void Simulate()
    {
        CalcData();
        //waterPlane.transform.position = new Vector3(0,Mathf.Lerp(cwp.placementPose.position.y,height,percent),0);
        goalHeight = Mathf.Lerp(cwp.placementPose.position.y, height, percent);
        waterPlane.SetActive(true);
        Debug.Log("The water is at " + waterPlane.transform.position + " " + dead + " " + total + " " + percent);
        flowingIn = true;
        tempTime = 0;
    }

    void CalcData()
    {
        foreach (Passanger passanger in dl.passangers)
        {
            if ((passanger.age > ud.ages.x && passanger.age < ud.ages.y))
            {
                if ((passanger.isMale && (ud.gender == Genders.male || ud.gender == Genders.other)) || (!passanger.isMale && (ud.gender == Genders.female || ud.gender == Genders.other)))
                {
                    if (ud.pClass == 0 || ud.pClass == passanger.pClass)
                    {
                        valid.Add(passanger);
                        if (!passanger.survived)
                        {
                            dead++;
                        }
                    }
                }
            }
        }

        total = valid.Count;
        percent = (float)dead / (float)total;
    }
}
