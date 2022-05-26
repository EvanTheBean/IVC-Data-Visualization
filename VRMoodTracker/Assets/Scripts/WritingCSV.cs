using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Text;
using System.Threading.Tasks;

public class WritingCSV : MonoBehaviour
{
    public void WritingToFile(string mood, string date)
    {
        //TextAsset moodSpreadsheet = Resources.Load<TextAsset>("MoodTracker"); //Finds the MoodTracker CSV File in the folders and references it
        string filepath = "Assets/Resources/MoodTracker.csv"; //Saves it as a String
        string sceneName = "PaintBlobVis";
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                file.WriteLine(mood + "," + date);
                file.Close();
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("The CSV file is not able to be found. Please check your files and run again.", ex);
        }
        SceneManager.LoadScene(sceneName);
    }
}
