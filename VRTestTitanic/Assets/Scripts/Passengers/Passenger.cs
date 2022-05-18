using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger
{
    public readonly bool survived;
    public readonly int pClass;
    public readonly string name;
    public readonly string fName;
    public readonly string lName;
    public readonly char sex;
    public readonly float age;

    public Passenger(bool survived, int pClass, string name, char sex, float age)
    {
        this.survived = survived;
        this.pClass = pClass;
        this.name = name;
        string[] names = name.Split(',');
        lName = names[0];
        fName = names[1];
        this.sex = sex;
        this.age = age;
    }
}
