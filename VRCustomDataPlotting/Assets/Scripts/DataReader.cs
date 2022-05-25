using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System;

public enum dataType
{
    Int,
    Float,
    String,
    StringArray,
    NULL
};

public class DataReader : MonoBehaviour
{
    public static DataReader Instance { get; private set; }

    string[] dataCategories;
    dataType[] dataTypes;
    List<DataObject> dataObjects = new List<DataObject>();

    [SerializeField] AxisBall axisBallPrefab;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }    
        else
        {
            Instance = this;
        }
    }

    public void Read(string filePath)
    {
        using (TextFieldParser csvParser = new TextFieldParser(filePath))
        {
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            //identify column names
            
            dataCategories = csvParser.ReadFields();
            dataTypes = new dataType[dataCategories.Length];
            //read data
            while (!csvParser.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[] fields = csvParser.ReadFields();

                if (fields.Length != dataCategories.Length)
                {
                    Debug.LogError("WARNING: field count incorrect");
                }
                dataObjects.Add(new DataObject(fields));
            }
            csvParser.Close();
        }
        IdentifyTypes();
    }

    void IdentifyTypes()
    {
        List<string> allOfCategory;
        for(int i = 0; i < dataCategories.Length; i++)
        {
            allOfCategory = new List<string>();
            foreach(DataObject data in dataObjects)
            {
                string dataEntry = data.GetDataAt(i);
                if (dataEntry != "")
                {
                    allOfCategory.Add(dataEntry);
                }
            }

            dataType result = dataType.NULL;
            foreach(string data in allOfCategory)
            {
                if (int.TryParse(data, out int a) && (result == dataType.NULL || result == dataType.Int))
                {
                    result = dataType.Int;
                }
                else if (float.TryParse(data, out float b) && (result == dataType.NULL || result == dataType.Float))
                {
                    result = dataType.Float;
                }
                else if (StringArrayTryParse(data, out string[] c) && (result == dataType.NULL || result == dataType.StringArray))
                {
                    result = dataType.StringArray;
                }
                else
                {
                    result = dataType.String;
                    break;
                }
            }

            dataTypes[i] = result;
        }
        SpawnAxisBalls();
    }

    bool StringArrayTryParse(string str, out string[] strArray)
    {
        strArray = new string[0];
        if (!((str[0] == '[' || str[0] == '{') && (str[str.Length - 1] == ']' || str[str.Length - 1] == '}')))
        {
            return false;
        }

        if (str.Length == 2)
        {
            return true;
        }

        StringReader strReader = new StringReader(str.Substring(1,str.Length-2).Replace('\'', '\"'));
        using (TextFieldParser csvParser = new TextFieldParser(strReader))
        {
            csvParser.SetDelimiters(new string[] { ", ", "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            strArray = csvParser.ReadFields();

            csvParser.Close();
            strReader.Close();
        }
        return true;
    }

    private void SpawnAxisBalls()
    {
        Vector3 instantiatePos = new Vector3(-6, 0, 2);
        for(int i = 0; i < dataCategories.Length; i++)
        {
            if (dataTypes[i] == dataType.Float || dataTypes[i] == dataType.Int)
            {
                AxisBall ball = Instantiate(axisBallPrefab, instantiatePos, Quaternion.identity);
                ball.SetUp(dataCategories[i], i);
                instantiatePos.x += 1.5f;
            }
        }
    }

    public List<DataObject> GetDataObjects()
    {
        return dataObjects;
    }

}
