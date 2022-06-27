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
    private VRPointer pointer;


    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
        data.useDragThreshold = true;
        pointer = GameObject.FindObjectOfType<VRPointer>();
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
	//Debug.Log("drag hold " + data.pointerDrag); 
        data.rawPointerPress = currentObject;
        pointer.ColorLine(true);

        Debug.Log("press on " + data.rawPointerPress.name);
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
        pointer.ColorLine(false);
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

        if(currentObject == null)
        {
            RaycastHit hit;
            Physics.Raycast(camera.gameObject.transform.position, camera.gameObject.transform.forward, out hit, pointer.defaultLength);
            //Debug.DrawRay(camera.gameObject.transform.position, camera.gameObject.transform.forward * pointer.defaultLength, Color.red);
            //Debug.Log(hit.collider);
            currentObject = hit.transform.gameObject;
            Debug.Log("was null " + currentObject.name);
        }
        else
        {
            Debug.Log(currentObject.name);
        }

	
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
                //ProcessHold(data);
            }

            ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.dragHandler);
        }
    }
}
