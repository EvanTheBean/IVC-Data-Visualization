using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    Sex,
    Age,
    Class
};

public class DataEntry
{
    public DataType choiceType;
    public string value;

    public DataEntry(DataType dataType, string value)
    {
        this.choiceType = dataType;
        this.value = value;
    }
}


public class PlayerChoices : MonoBehaviour
{
    List<DataEntry> choices = new List<DataEntry>();

    public void GoThroughGateway(DataEntry choice)
    {
        choices.Add(choice);
    }

    public float DetermineSurvivalability()
    {
        float age = 0;
        int pClass = 0;
        char sex = '0';
        foreach (DataEntry choice in choices)
        {
            
            switch (choice.choiceType)
            {
                case DataType.Age:
                    age = float.Parse(choice.value);
                    break;
                case DataType.Sex:
                    sex = choice.value[0];
                    break;
                case DataType.Class:
                    pClass = int.Parse(choice.value);
                    break;
            }
        }
        Debug.Log("Class: " + pClass.ToString() + "Sex: " + sex.ToString() + "Age: " + age.ToString());
        return FindObjectOfType<DataReader>().CalculateSurvivalChance(pClass, sex, age);
    }
}
