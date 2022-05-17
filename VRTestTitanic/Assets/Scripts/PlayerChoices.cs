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
}


public class PlayerChoices : MonoBehaviour
{
    List<DataEntry> choices;

    void GoThroughGateway(DataEntry choice)
    {
        choices.Add(choice);
    }    
}
