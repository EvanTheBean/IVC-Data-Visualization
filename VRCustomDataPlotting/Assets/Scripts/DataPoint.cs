using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using TMPro;

public enum axis
{
    X,
    Y,
    Z
}

public class DataPoint : MonoBehaviour
{
    DataObject data;

	List<axis> undefined = new List<axis>();

    public void SetUp(DataObject dataObj)
    {
        data = dataObj;
    }

    public float SetPosition(int categoryIndex, axis axis, float scale)
    {
        Vector3 newPos = transform.localPosition;
        
        if (!float.TryParse(data.GetDataAt(categoryIndex), out float val))
        {
            gameObject.SetActive(false);
			undefined.Add(axis);
            return 0.0f;
        }

		if (undefined.Contains(axis))
        {
			undefined.Remove(axis);
        }

		if (undefined.Count != 0)
        {
			return 0.0f;
        }

        val *= scale;
        gameObject.SetActive(true);
        switch (axis)
        {
            case axis.X:
                newPos.x = val;
                break;
            case axis.Y:
                newPos.y = val;
                break;
            case axis.Z:
                newPos.z = val;
                break;
        }
		
        transform.localPosition = newPos;
		return val;
    }


	[SerializeField] TextMeshPro generalText;

	private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (Hand.AttachmentFlags.VelocityMovement) & (~Hand.AttachmentFlags.ParentToHand);

	private Interactable interactable;

	//-------------------------------------------------
	void Awake()
	{
		generalText.text = "";
		interactable = this.GetComponent<Interactable>();
	}


	//-------------------------------------------------
	// Called when a Hand starts hovering over this object
	//-------------------------------------------------
	private void OnHandHoverBegin(Hand hand)
	{
	}


	//-------------------------------------------------
	// Called when a Hand stops hovering over this object
	//-------------------------------------------------
	private void OnHandHoverEnd(Hand hand)
	{
	}


	//-------------------------------------------------
	// Called every Update() while a Hand is hovering over this object
	//-------------------------------------------------
	private void HandHoverUpdate(Hand hand)
	{
		GrabTypes startingGrabType = hand.GetGrabStarting();
		bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

		if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
		{

			// Call this to continue receiving HandHoverUpdate messages,
			// and prevent the hand from hovering over anything else
			hand.HoverLock(interactable);

			// Attach this object to the hand
			hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
		}
		else if (isGrabEnding)
		{
			// Detach this object from the hand
			hand.DetachObject(gameObject);

			// Call this to undo HoverLock
			hand.HoverUnlock(interactable);

		}
	}


	//-------------------------------------------------
	// Called when this GameObject becomes attached to the hand
	//-------------------------------------------------
	private void OnAttachedToHand(Hand hand)
	{
		DisplayData(true);
	}

	//-------------------------------------------------
	// Called when this GameObject is detached from the hand
	//-------------------------------------------------
	private void OnDetachedFromHand(Hand hand)
	{
		DisplayData(false);
	}


	private void DisplayData(bool display)
	{
		if (display)
		{
			//text formatting
			string textOutput = "";

			for(int i = 0; i < data.Length(); i++)
			{
				textOutput += DataReader.Instance.GetCategory(i) + ": " + data.GetDataAt(i) + "\n";
            }
			generalText.text = textOutput;
		}
		else
		{
			generalText.text = "";
		}
	}






	//-------------------------------------------------
	// Called every Update() while this GameObject is attached to the hand
	//-------------------------------------------------
	private void HandAttachedUpdate(Hand hand)
	{
	}

	private bool lastHovering = false;
	private void Update()
	{
		if (interactable.isHovering != lastHovering) //save on the .tostrings a bit
		{
			lastHovering = interactable.isHovering;
		}
	}


	//-------------------------------------------------
	// Called when this attached GameObject becomes the primary attached object
	//-------------------------------------------------
	private void OnHandFocusAcquired(Hand hand)
	{
	}


	//-------------------------------------------------
	// Called when another attached GameObject becomes the primary attached object
	//-------------------------------------------------
	private void OnHandFocusLost(Hand hand)
	{
	}
}
