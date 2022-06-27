using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRScrollViewControl : MonoBehaviour
{
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Vector2 moveAction;
    public CustomScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveAction.GetAxis(targetSource) != Vector2.zero)
        {
            if(moveAction.GetLastAxis(targetSource) == Vector2.zero)
            {
                scrollRect.BeginDrag(moveAction.GetAxis(targetSource));
            }
            else
            {
                scrollRect.Drag(moveAction.GetAxis(targetSource));
            }
        }
        else
        {
            if(moveAction.GetLastAxis(targetSource) != Vector2.zero)
            {
                scrollRect.EndDrag();
            }
        }
    }
}
