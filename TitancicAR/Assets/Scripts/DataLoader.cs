using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;

public enum colums
{
    passengerID,
    survived,
    pClass,
    lastName,
    firstName,
    sex,
    age,
    sibSp,
    parch,
    ticket,
    fare,
    cabin,
    embarked
}


[ExecuteInEditMode]
public class DataLoader : MonoBehaviour
{
    public bool load, check;
    public List<Passanger> passangers = new List<Passanger>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(load)
        {
            load = false;

            LoadData();

            foreach(Passanger passanger in passangers)
            {
                Debug.Log(passanger.name + " " + passanger.age);
            }

            //Debug.Log(passangers.Count);
        }

        if(check)
        {
            check = false;
            //Debug.Log(passangers.Count);

            for(int i = 0; i < passangers.Count; i++)
            {
                Debug.Log(i + " " + passangers[i].name + " " + passangers[i].age);
            }
        }
    }

    void LoadData()
    {
        passangers.Clear();
        string fileData = System.IO.File.ReadAllText(Application.dataPath + "/Resources/train.csv");
        string[] lines  = fileData.Split("\n"[0]);

        for(int i = 1; i < lines.Length; i++)
        {
            CreatePassenger(lines[i]);
        }
    }

    void CreatePassenger(string line)
    {
        string[] lineData = line.Trim().Split(","[0]);
        

        if(lineData.Length > 3)
        {
            Passanger temp = new Passanger();
            AssetDatabase.CreateAsset(temp, "Assets/Passengers/" + (lineData[(int)colums.lastName]).Replace("\"", "").Replace(" ", ""));
            passangers.Add(temp);

            temp.name = lineData[(int)colums.lastName] + ", " + lineData[(int)colums.firstName];
            temp.name = temp.name.Replace("\"", "");
            temp.survived = int.Parse(lineData[(int)colums.survived]) == 0 ? false : true;
            temp.isMale = lineData[(int)colums.sex] == "male" ? true : false;
            temp.ticket = lineData[(int)colums.ticket];
            try
            {
                temp.age = int.Parse(lineData[(int)colums.age]);
                temp.documentedAge = true;
            }
            catch
            {
                temp.age = 1000;
                temp.documentedAge = false;
            }
            temp.pClass = int.Parse(lineData[(int)colums.pClass]);
            temp.fare = float.Parse(lineData[(int)colums.fare]);
        }
    }
}
