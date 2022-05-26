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

    public void visualCreator(int joy, int love, int fear, int anger, int sadness, int surprise)
    {
        Debug.Log(joy);
        Debug.Log(love);
        Debug.Log(fear);
        Debug.Log(anger);
        Debug.Log(sadness);
        Debug.Log(surprise);

        if (String.Compare(sceneName, "PaintBlobVis", false) == 0)
        {
            // Changing the size of the spheres to match the amount of times the feeling is felt
            joySphere.transform.localScale = new Vector3( joy, joy, joy);
            loveSphere.transform.localScale = new Vector3(love, love, love);
            fearSphere.transform.localScale = new Vector3(fear, fear, fear);
            angerSphere.transform.localScale = new Vector3(anger, anger, anger);
            sadnessSphere.transform.localScale = new Vector3(sadness, sadness, sadness);
            surpriseSphere.transform.localScale = new Vector3(surprise, surprise, surprise);

            // Parsing the different dates to figure out which is the most recent

        }
    }
}
