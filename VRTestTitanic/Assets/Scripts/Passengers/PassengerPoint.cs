using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using TMPro;

public class PassengerPoint : MonoBehaviour
{
	Passenger passengerInfo;

	[SerializeField] TextMeshProUGUI generalText;
	[SerializeField] GameObject textBackground;

	private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (Hand.AttachmentFlags.VelocityMovement) & (~Hand.AttachmentFlags.ParentToHand);

	private Interactable interactable;

	//-------------------------------------------------
	void Awake()
	{

		generalText.text = "";
		textBackground.SetActive(false);
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
		string textOutput = string.Format("Name: {0} {1}\nClass: {2}\nAge: {3}\nSex: {4}\n", passengerInfo.fName,passengerInfo.lName, passengerInfo.pClass, passengerInfo.age, passengerInfo.sex);
		if (passengerInfo.survived)
		{
			textOutput += "Survived";
		}
		else
        {
			textOutput += "Did not survive";
        }
		generalText.text = textOutput;
		textBackground.SetActive(true);
	}



	//-------------------------------------------------
	// Called when this GameObject is detached from the hand
	//-------------------------------------------------
	private void OnDetachedFromHand(Hand hand)
	{
		generalText.text = "";
		textBackground.SetActive(false);
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

	public void SetPassenger(Passenger newPassenger)
    {
		passengerInfo = newPassenger;
    }
}

