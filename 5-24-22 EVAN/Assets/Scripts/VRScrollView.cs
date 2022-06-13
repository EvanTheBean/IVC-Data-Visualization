using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRScrollView : MonoBehaviour
{

    public SteamVR_Input_Sources trackPadSource;
    public SteamVR_Action_Vector2 trackPadAction;

    ScrollRect rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scroll = trackPadAction.GetAxis(trackPadSource);

        if (scroll != Vector2.zero)
        {
            rect.velocity = scroll;
        }

        Debug.Log(scroll);
    }
}
