using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInput : BaseInputModule
{
    public Camera camera;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject = null;
    private PointerEventData data = null;


    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        if(newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(data.pointerPressRaycast.gameObject);
        ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.beginDragHandler);
	    Debug.Log("drag hold " + data.pointerDrag); 
        data.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if(data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }
        ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.endDragHandler);

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.pointerDrag = null;
        data.rawPointerPress = null;
    }

    private void ProcessHold(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.dragHandler);
    }

    public override void Process()
    {
        data.Reset();

        data.position = new Vector2(camera.pixelWidth / 2f, camera.pixelHeight / 2f);

        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        if(camera.gameObject.GetComponent<VRPointer>().show)
        {
            HandlePointerExitAndEnter(data, currentObject);

            if (clickAction.GetStateDown(targetSource))
            {
                ProcessPress(data);
            }

            if (clickAction.GetStateUp(targetSource))
            {
                ProcessRelease(data);
            }

            if(clickAction.GetState(targetSource))
            {
                ProcessHold(data);
            }

            ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.dragHandler);
        }
    }
}
