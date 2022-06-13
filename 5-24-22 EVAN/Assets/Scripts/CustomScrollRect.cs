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
        Debug.Log("Begin Drag");
        dragStart = data.position;
        contentStart = contentBox.transform.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("End Drag");
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("Dragging");
        contentBox.transform.position += new Vector3(contentBox.transform.position.x + Mathf.Sign(data.position.x - dragStart.x), contentBox.transform.position.y, contentBox.transform.position.z);
    }


}
