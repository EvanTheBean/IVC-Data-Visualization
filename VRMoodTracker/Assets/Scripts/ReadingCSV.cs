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
            //It will compare the dates with each other to find the most recent.
        }
        //Visual Creator Goes Here
        //VisualCreator.GetComponent<VisualCreator>().visualCreator(NumOfJoy, NumOfLove, NumOfFear, NumOfAnger, NumOfSadness, NumOfSurprise);
    }
}
