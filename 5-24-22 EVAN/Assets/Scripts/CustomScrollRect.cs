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
        float currentLoc = contentBox.GetComponent<RectTransform>().anchoredPosition.x * -1f;
        float currentPer = currentLoc % size;
        if(currentPer > size /2f)
        {
            contentBox.GetComponent<RectTransform>().anchoredPosition -= new Vector2(currentPer - (size / 2f), 0);
        }
        else
        {
            contentBox.GetComponent<RectTransform>().anchoredPosition += new Vector2(currentPer,0);
        }

        Debug.Log(currentLoc + " " + currentPer + " " + contentBox.GetComponent<RectTransform>().anchoredPosition.x);

        if (contentBox.GetComponent<RectTransform>().anchoredPosition.x < 0)
        {
            contentBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, contentBox.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if (contentBox.GetComponent<RectTransform>().anchoredPosition.x > (contentBox.transform.childCount - 1) * size)
        {
            contentBox.GetComponent<RectTransform>().anchoredPosition = new Vector2((contentBox.transform.childCount - 1) * size, contentBox.GetComponent<RectTransform>().anchoredPosition.y);
        }

    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log("Dragging");
        Vector3 dif = data.pointerCurrentRaycast.worldPosition - dragStart;
        Vector3 right = this.transform.right;
        Vector3 difRot = new Vector3(dif.x * right.x, dif.y * right.y, dif.z * right.z);
        contentBox.GetComponent<RectTransform>().anchoredPosition += new Vector2(difRot.magnitude * 100f * Mathf.Sign(difRot.x), 0);

        if(contentBox.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            contentBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, contentBox.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else if (contentBox.GetComponent<RectTransform>().anchoredPosition.x < ((contentBox.transform.childCount - 1) * size) * -1)
        {
            contentBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(((contentBox.transform.childCount - 1) * size) * -1, contentBox.GetComponent<RectTransform>().anchoredPosition.y);
        }

        //Debug.Log(contentBox.GetComponent<RectTransform>().position);
        dragStart = data.pointerCurrentRaycast.worldPosition;
    }


}
