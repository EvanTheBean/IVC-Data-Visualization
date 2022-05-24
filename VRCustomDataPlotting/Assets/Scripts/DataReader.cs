using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;

public class DataReader : MonoBehaviour
{
    public static DataReader Instance { get; private set; }

    string[] dataCategories;

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
            {
                string[] fields = csvParser.ReadFields();
                
            }
            //read data
            while (!csvParser.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[] fields = csvParser.ReadFields();
                Debug.Log("DataReader -> Read(): " + fields.Length.ToString() + " fields identified.");

                
            }
        }
    }
}
