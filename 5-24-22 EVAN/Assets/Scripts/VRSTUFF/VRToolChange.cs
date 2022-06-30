using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;
public class VRToolChange : MonoBehaviour
{
    public static int currentTool;
    public int tool4Dis;
    public int totalTools;

    public TextMeshProUGUI toolText;

    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Vector2 moveAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAction.GetAxis(targetSource) != Vector2.zero && moveAction.GetLastAxis(targetSource) == Vector2.zero)
        {
            currentTool++;
            currentTool = currentTool % totalTools;
            tool4Dis = currentTool;
            toolText.text = currentTool.ToString();
            Debug.Log("changing tool");
        }
    }
}
