using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;

public class CustomScrollRect : MonoBehaviour
{
    public Image area;
    public GameObject viewPort, contentBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("BeginDrag");
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
        Debug.Log("End Drag");
    }


}
