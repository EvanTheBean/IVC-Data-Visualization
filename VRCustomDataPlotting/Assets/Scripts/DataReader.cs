using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;

public class DataReader : MonoBehaviour
{
    public static DataReader Instance { get; private set; }

    string[] dataCategories;
    List<DataObject> dataObjects = new List<DataObject>();

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
            
            string[] dataCategories = csvParser.ReadFields();

            //read data
            while (!csvParser.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[] fields = csvParser.ReadFields();
                dataObjects.Add(new DataObject(fields));
            }
        }
    }
}
