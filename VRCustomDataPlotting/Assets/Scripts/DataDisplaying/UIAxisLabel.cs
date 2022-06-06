using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIAxisLabel : MonoBehaviour
{
    [SerializeField] public axis axis;

    float defaultVal;

    TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        SetDefaultVal();
    }

    internal void UpdateScale(float newScale, int sigFigs = 1)
    {   
        if (newScale == 0)
        {
            return;
        }

        float currentPosVal = (defaultVal / newScale); //the value that the tick is currently located at

        // convert to X.X * 10^X
        int magnitude = 0;
        while (currentPosVal / 10 > 1)
        {
            currentPosVal /= 10;
            magnitude++;
        }

        Vector3 newPos = transform.localPosition;
        switch (axis)
        {
            case axis.X:
                newPos.x = (float)Math.Round((double)currentPosVal, sigFigs) * Mathf.Pow(10, magnitude) * newScale;
                break;
            case axis.Y:
                newPos.y = (float)Math.Round((double)currentPosVal, sigFigs) * Mathf.Pow(10, magnitude) * newScale;
                break;
            case axis.Z:
                newPos.z = (float)Math.Round((double)currentPosVal, sigFigs) * Mathf.Pow(10, magnitude) * newScale;
                break;
        }

        transform.localPosition = newPos;
        text.SetText(((float)Math.Round((double)currentPosVal, 1) * Mathf.Pow(10, magnitude)).ToString());
    }

    public void SetDefaultVal()
    {
        text = GetComponent<TextMeshPro>();

        switch (axis)
        {
            case axis.X:
                defaultVal = transform.localPosition.x;
                break;
            case axis.Y:
                defaultVal = transform.localPosition.y;
                break;
            case axis.Z:
                defaultVal = transform.localPosition.z;
                break;
        }
    }
}
