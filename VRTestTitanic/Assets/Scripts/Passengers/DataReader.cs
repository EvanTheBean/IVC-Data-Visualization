using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
public class DataReader : MonoBehaviour
{
    string fileName;
    List<Passenger> allPassengers = new List<Passenger>();

    const string FILE_NAME = "train.csv";

    private void Start()
    {
        fileName = FILE_NAME;

        Read();
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

                if (fields[5] == "")
                {
                    continue;
                }

                bool survived = true;
                if (fields[1] == "0")
                {
                    survived = false;
                }

                allPassengers.Add(new Passenger(survived, int.Parse(fields[2]), fields[3], fields[4][0],float.Parse(fields[5])));
            }
        }
    }

    public float CalculateSurvivalChance(int pClass, char sex, float age)
    {
        int similarPassengers = 0;
        int survived = 0;

        foreach (Passenger pass in allPassengers)
        {
            if (pass.pClass == pClass && (pass.sex == char.ToLower(sex) || sex == 'o') && (pass.age < age + 5 && pass.age > age-5))
            {
                similarPassengers++;
                if (pass.survived)
                {
                    survived++;
                }
            }
        }

        if (similarPassengers == 0)
        {
            return 100;
        }

        return survived * 100 / similarPassengers;

    }

    public float CalculateSurvivalChanceSex(char sex)
    {
        int similarPassengers = 0;
        int survived = 0;
        
        foreach (Passenger pass in allPassengers)
        {
            if (pass.sex == char.ToLower(sex) || sex == 'o')
            {
                similarPassengers++;
                if (pass.survived)
                {
                    survived++;
                }
            }
        }

        return survived * 100 / similarPassengers;

    }

    public float CalculateSurvivalChanceClass(int pClass)
    {
        int similarPassengers = 0;
        int survived = 0;

        foreach (Passenger pass in allPassengers)
        {
            if (pass.pClass == pClass)
            {
                similarPassengers++;
                if (pass.survived)
                {
                    survived++;
                }
            }
        }

        return survived * 100 / similarPassengers;
    }

    public float CalculateSurvivalChanceAge(int age)
    {
        int similarPassengers = 0;
        int survived = 0;

        foreach (Passenger pass in allPassengers)
        {
            if (pass.age < age + 5 && pass.age > age - 5)
            {
                similarPassengers++;
                if (pass.survived)
                {
                    survived++;
                }
            }
        }

        return survived * 100 / similarPassengers;
    }

    public List<Passenger> GetPassengersList()
    {
        if (allPassengers.Count == 0)
        {
            fileName = FILE_NAME;
            Read();
        }

        return allPassengers;
    }

}
