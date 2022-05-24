using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingCSV : MonoBehaviour
{
    List<Moods> moodList = new List<Moods>(); //Keeps tracks of all of the instances of the Mood class
    
    // Start is called before the first frame update
    void Start()
    {
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
            int.TryParse(row[2], out m.numdaystracked); //Import the days tracked and assigns it - turns it into an int if theres a number there

            moodList.Add(m);
         }

        foreach (Moods m in moodList) //To test that they were all parsed and turned into their own Moods in the list
        {
            Debug.Log(m.mood);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
