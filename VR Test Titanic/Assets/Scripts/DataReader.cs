using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
public class DataReader
{
    string fileName;
    public DataReader(string fileName)
    {
        this.fileName = fileName;
    }

    public void Read()
    {
        using (TextFieldParser csvParser = new TextFieldParser(Application.streamingAssetsPath + '/' + fileName))
        {
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            // Skip the row with the column names
            csvParser.ReadLine();

            while (!csvParser.EndOfData)
            {
                // Read current line fields, pointer moves to the next line.
                string[] fields = csvParser.ReadFields();
                foreach (string field in fields)
                {
                    Debug.Log(field);
                }
            }
        }
    }
}
