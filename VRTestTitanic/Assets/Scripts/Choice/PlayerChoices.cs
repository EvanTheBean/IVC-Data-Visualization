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
    private DataType dataType;

    public DataEntry(DataType dataType, string value)
    {
        this.dataType = dataType;
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
}
