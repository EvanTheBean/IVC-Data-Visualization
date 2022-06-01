using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCreator : MonoBehaviour
{
    public string sceneName; //Name of the current scene
    public GameObject joySphere;
    public GameObject loveSphere;
    public GameObject fearSphere;
    public GameObject angerSphere;
    public GameObject sadnessSphere;
    public GameObject surpriseSphere;
    public GameObject player;

    public void visualCreator(int joy, int love, int fear, int anger, int sadness, int surprise, string joyDate, string loveDate, string fearDate, string angerDate, string sadnessDate, string surpriseDate)
    {
        Debug.Log(joy);
        Debug.Log(love);
        Debug.Log(fear);
        Debug.Log(anger);
        Debug.Log(sadness);
        Debug.Log(surprise);

        Double playerPositionZ = player.transform.position.z;

        if (String.Compare(sceneName, "PaintBlobVis", false) == 0)
        {
            // Changing the size of the spheres to match the amount of times the feeling is felt
            joySphere.transform.localScale = new Vector3( joy, joy, joy);
            loveSphere.transform.localScale = new Vector3(love, love, love);
            fearSphere.transform.localScale = new Vector3(fear, fear, fear);
            angerSphere.transform.localScale = new Vector3(anger, anger, anger);
            sadnessSphere.transform.localScale = new Vector3(sadness, sadness, sadness);
            surpriseSphere.transform.localScale = new Vector3(surprise, surprise, surprise);

            // Comparing Dates to "Today"
            DateTime today = DateTime.Now;

            Double distanceJoy = 0;
            TimeSpan distanceJoyTP;
            if (joyDate.Length == 0)
            {

            }
            else
            {
                DateTime ClosestDateJ = DateTime.Parse(joyDate);
                distanceJoyTP = today.Subtract(ClosestDateJ);
                distanceJoy = distanceJoyTP.TotalDays * 2;
            }

            Double distanceLove = 0;
            TimeSpan distanceLoveTP;
            if (loveDate.Length == 0)
            {

            }
            else
            {
                DateTime ClosestDateL = DateTime.Parse(loveDate);
                distanceLoveTP = today.Subtract(ClosestDateL);
                distanceLove = distanceLoveTP.TotalDays * 2;
            }

            Double distanceFear = 0;
            TimeSpan distanceFearTP;
            if (fearDate.Length == 0)
            {

            }
            else 
            {
                DateTime ClosestDateF = DateTime.Parse(fearDate);
                distanceFearTP = today.Subtract(ClosestDateF);
                distanceFear = distanceFearTP.TotalDays * 2;
            }

            Double distanceAnger = 0;
            TimeSpan distanceAngerTP;
            if (angerDate.Length == 0)
            {

            }
            else
            {
                DateTime ClosestDateA = DateTime.Parse(angerDate);
                distanceAngerTP = today.Subtract(ClosestDateA);
                distanceAnger = distanceAngerTP.TotalDays * 2;
            }

            Double distanceSadness = 0;
            TimeSpan distanceSadnessTP;
            if (sadnessDate.Length == 0)
            {
                
            } else
            {
                DateTime ClosestDateSa = DateTime.Parse(sadnessDate);
                distanceSadnessTP = today.Subtract(ClosestDateSa);
                distanceSadness = distanceSadnessTP.TotalDays * 2;
            }

            Double distanceSurprise = 0;
            TimeSpan distanceSurpriseTP;
            if (surpriseDate.Length == 0)
            {

            } else
            {
                DateTime ClosestDateSu = DateTime.Parse(surpriseDate);
                distanceSurpriseTP = today.Subtract(ClosestDateSu);
                distanceSurprise = distanceSurpriseTP.TotalDays * 2;
            }

            //positioning them accordingly in relation to the player
            joySphere.transform.Translate(0,0, (float)distanceJoy);
            loveSphere.transform.Translate(0, 0, (float)distanceLove);
            fearSphere.transform.Translate(0, 0, (float)distanceFear);
            angerSphere.transform.Translate(0, 0, (float)distanceAnger);
            sadnessSphere.transform.Translate(0, 0, (float)distanceSadness);
            surpriseSphere.transform.Translate(0, 0, (float)distanceSurprise);
        }
    }
}
