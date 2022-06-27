using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class VRToolChange : MonoBehaviour
{
    public static int currentTool;
    public int totalTools;

    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Vector2 moveAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAction.GetAxis(targetSource) != Vector2.zero)
        {
            currentTool++;
            currentTool = currentTool % totalTools;
        }
    }
}
