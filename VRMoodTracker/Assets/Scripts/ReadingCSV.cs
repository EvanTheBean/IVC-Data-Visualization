using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingCSV : MonoBehaviour
{
    List<Moods> moodList = new List<Moods>(); //Keeps tracks of all of the instances of the Mood class
    public int NumOfJoy;
    public int NumOfLove;
    public int NumOfFear;
    public int NumOfAnger;
    public int NumOfSadness;
    public int NumOfSurprise;

    public string ClosestDateJoy = "";
    public string CurrentDateJoy;

    public string ClosestDateLove = "";
    public string CurrentDateLove;

    public string ClosestDateFear = "";
    public string CurrentDateFear;

    public string ClosestDateAnger = "";
    public string CurrentDateAnger;

    public string ClosestDateSadness = "";
    public string CurrentDateSadness;

    public string ClosestDateSurprise = "";
    public string CurrentDateSurprise;

    public GameObject VisualCreator;
    public GameObject Pedestal;
    public GameObject Button;

    //Reads the file and sorts it into a list
    public void ReadFile()
    {
        Destroy(Pedestal);
        Destroy(Button);
        TextAsset moodSpreadsheet = Resources.Load<TextAsset>("MoodTracker"); //Finds the MoodTracker CSV File in the folders and references it

        //Testing to make sure that the CSV File is being read
        string fileContents = moodSpreadsheet.text;
        Debug.Log(fileContents);
        

        //Other things done in the tutorial - with explainations next to them
         string[] data = moodSpreadsheet.text.Split(new char[] { '\n' }); //Parses each line of the file and turns it into a spot in the array

         Debug.Log(data.Length); //Gets the length of the array and tells the user in the debug log
          
         for (int i = 1; i < data.Length - 1; i++) //Parses the array even more- getting rid of the first and last lines (the first line is headers for ease of editing and the last line is blank)
         {
            string[] row = data[i].Split(new char[] { ',' }); //Creates an array for each row detailing each item in it- seperated by the comma
            Moods m = new Moods();
            m.mood = row[0]; //Imports the mood and assigns it
            m.date = row[1]; //Import the date and assigns it
            //int.TryParse(row[2], out m.numdaystracked); //Import the days tracked and assigns it - turns it into an int if theres a number there

            moodList.Add(m);
         }

         JoyCounter();
    }

    //Figures out the amount of times the user inputted that they felt Joy
    public void JoyCounter()
    {
        NumOfJoy = 0;
        foreach (Moods m in moodList) 
        {
            if (String.Compare(m.mood, "Joy", true) == 0)
            {
                NumOfJoy++;
            }
        }
        LoveCounter();
    }

    //Figures out the amount of times the user inputted that they felt Love
    public void LoveCounter()
    {
        NumOfLove = 0;
        foreach (Moods m in moodList)
        {
            if (String.Compare(m.mood, "Love", true) == 0)
            {
                NumOfLove++;
            }
        }
        FearCounter();
    }

    //Figures out the amount of times the user inputted that they felt Fear
    public void FearCounter()
    {
        NumOfFear = 0;
        foreach (Moods m in moodList)
        {
            if (String.Compare(m.mood, "Fear", true) == 0)
            {
                NumOfFear++;
            }
        }
        AngerCounter();
    }

    //Figures out the amount of times the user inputted that they felt Anger
    public void AngerCounter()
    {
        NumOfAnger = 0;
        foreach (Moods m in moodList)
        {
            if (String.Compare(m.mood, "Anger", true) == 0)
            {
                NumOfAnger++;
            }
        }
        SadnessCounter();
    }

    //Figures out the amount of times the user inputted that they felt Sadness
    public void SadnessCounter()
    {
        NumOfSadness = 0;
        foreach (Moods m in moodList)
        {
            if (String.Compare(m.mood, "Sadness", true) == 0)
            {
                NumOfSadness++;
            }
        }
        SurpriseCounter();
    }

    //Figures out the amount of times the user inputted that they felt Surprise
    public void SurpriseCounter()
    {
        NumOfSurprise = 0;
        foreach (Moods m in moodList)
        {
            if (String.Compare(m.mood, "Surprise", true) == 0)
            {
                NumOfSurprise++;
            }
        }
        DateParser();
    }
    /*This method will go through for each mood and figure out the dates. 
     *It will compare the dates with each other to find the most recent.
     * It will only keep the most recent dates for each emotion.
     * It will then start the next function "Visual Creator"
     */

    /* Helpful Resources
     * https://www.tutorialspoint.com/datetime-compare-method-in-chash
     * https://www.codegrepper.com/code-examples/csharp/c%23+how+to+compare+2+dates+without+time
     * https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-datetime
     * https://www.c-sharpcorner.com/UploadFile/manas1/string-to-datetime-conversion-in-C-Sharp/
     */

    public void DateParser()
    {
        //This method will through for each mood and figure out the dates.
        foreach (Moods m in moodList)
        {
            string CurrentDate = m.date;
            DateTime CurrentDateDT = DateTime.Parse(CurrentDate);

            if (String.Compare(m.mood, "Joy", true) == 0)
            {
                if (ClosestDateJoy.Length == 0) //If the string "ClosestDateJoy" is empty
                {
                    ClosestDateJoy = CurrentDate;
                }
                else
                {
                    DateTime ClosestDateJ = DateTime.Parse(ClosestDateJoy);

                    if (ClosestDateJ.CompareTo(CurrentDateDT) < 0) //If the closest date happened before than the current date
                    {
                        ClosestDateJoy = CurrentDate;
                    }
                    else
                    {
                        ClosestDateJoy = ClosestDateJoy;
                    }
                }

            }
            else if (String.Compare(m.mood, "Love", true) == 0)
            {
                if (ClosestDateLove.Length == 0) //If the string "ClosestDateLove" is empty
                {
                    ClosestDateLove = CurrentDate;
                }
                else
                {
                    DateTime ClosestDateL = DateTime.Parse(ClosestDateLove);

                    if (ClosestDateL.CompareTo(CurrentDateDT) < 0) //If the closest date happened before than the current date
                    {
                        ClosestDateLove = CurrentDate;
                    }
                    else
                    {
                        ClosestDateLove = ClosestDateLove;
                    }
                }
            }
            else if (String.Compare(m.mood, "Fear", true) == 0)
            {
                if (ClosestDateFear.Length == 0) //If the string "ClosestDateFear" is empty
                {
                    ClosestDateFear = CurrentDate;
                }
                else
                {
                    DateTime ClosestDateF = DateTime.Parse(ClosestDateFear);

                    if (ClosestDateF.CompareTo(CurrentDateDT) < 0) //If the closest date happened before than the current date
                    {
                        ClosestDateFear = CurrentDate;
                    }
                    else
                    {
                        ClosestDateFear = ClosestDateFear;
                    }
                }
            }
            else if (String.Compare(m.mood, "Anger", true) == 0)
            {
                if (ClosestDateAnger.Length == 0) //If the string "ClosestDateAnger" is empty
                {
                    ClosestDateAnger = CurrentDate;
                }
                else
                {
                    DateTime ClosestDateA = DateTime.Parse(ClosestDateAnger);

                    if (ClosestDateA.CompareTo(CurrentDateDT) < 0) //If the closest date happened before than the current date
                    {
                        ClosestDateAnger = CurrentDate;
                    }
                    else
                    {
                        ClosestDateAnger = ClosestDateAnger;
                    }
                }
            }
            else if (String.Compare(m.mood, "Sadness", true) == 0)
            {
                if (ClosestDateSadness.Length == 0) //If the string "ClosestDateSadness" is empty
                {
                    ClosestDateSadness = CurrentDate;
                }
                else
                {
                    DateTime ClosestDateSd = DateTime.Parse(ClosestDateSadness);

                    if (ClosestDateSd.CompareTo(CurrentDateDT) < 0) //If the closest date happened before than the current date
                    {
                        ClosestDateSadness = CurrentDate;
                    }
                    else
                    {
                        ClosestDateSadness = ClosestDateSadness;
                    }

                }
            }
            else if (String.Compare(m.mood, "Surprise", true) == 0)
            {
                if (ClosestDateSurprise.Length == 0) //If the string "ClosestDateSurprise" is empty
                {
                    ClosestDateSurprise = CurrentDate;
                }
                else
                {
                    DateTime ClosestDateSu = DateTime.Parse(ClosestDateSurprise);

                    if (ClosestDateSu.CompareTo(CurrentDateDT) < 0) //If the closest date happened before than the current date
                    {
                        ClosestDateSurprise = CurrentDate;
                    }
                    else
                    {
                        ClosestDateSurprise = ClosestDateSurprise;
                    }
                }
            }
        }

        //Visual Creator Goes Here
        VisualCreator.GetComponent<VisualCreator>().visualCreator(NumOfJoy, NumOfLove, NumOfFear, NumOfAnger, NumOfSadness, NumOfSurprise, ClosestDateJoy, ClosestDateLove, ClosestDateFear, ClosestDateAnger, ClosestDateSadness, ClosestDateSurprise);
    }
}
