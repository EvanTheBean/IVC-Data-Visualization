using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIInputModule : PointerInputModule
{

    public override void Process()
    {
        EventSystem.current.RaycastAll(data, m_RaycastResultCache);

        foreach (RaycastResult raycastResult in m_RaycastResultCache)
        {
                Debug.Log(raycastResult);
                IPointerClickHandler clickHandler = raycastResult.gameObject.GetComponent<IPointerClickHandler>();
                if (clickHandler == null)
                    clickHandler = raycastResult.gameObject.GetComponentInParent<IPointerClickHandler>();

                if (clickHandler != null)
                {
                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    clickHandler.OnPointerClick(pointerEventData);
                }
        }
        
    }
}
