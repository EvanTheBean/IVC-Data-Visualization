using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;

public class CustomScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image area;
    public GameObject viewPort, contentBox;

    public Vector3 dragStart, contentStart;

    public float size;
    // Start is called before the first frame update
    void Start()
    {
        size = contentBox.transform.GetChild(0).gameObject.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log("Begin Drag");
        dragStart = data.pointerCurrentRaycast.worldPosition;
        contentStart = contentBox.GetComponent<RectTransform>().position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("End Drag");
    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log("Dragging");
        Vector3 dif = data.pointerCurrentRaycast.worldPosition - dragStart;
        Vector3 right = this.transform.right;
        Vector3 difRot = new Vector3(dif.x * right.x, dif.y * right.y, dif.z * right.z);
        contentBox.GetComponent<RectTransform>().anchoredPosition += new Vector2(difRot.magnitude * 10f, 0);
        Debug.Log(contentBox.GetComponent<RectTransform>().position);
        dragStart = data.pointerCurrentRaycast.worldPosition;
    }


}
