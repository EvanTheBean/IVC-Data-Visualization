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

    UserData ud;
    DataLoader dl;

    // Start is called before the first frame update
    void Start()
    {
        ud = GetComponent<UserData>();
        dl = GetComponent<DataLoader>();
    }

    public void Simulate()
    {
        CalcData();
        waterPlane.transform.position = new Vector3(0,Mathf.Lerp(0,ud.height,percent),0);
        waterPlane.SetActive(true);
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
