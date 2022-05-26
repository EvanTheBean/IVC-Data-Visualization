using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class PaintBlobButton : MonoBehaviour
{
	//Variables for pressing button
	//private Vector3 oldPosition;
	//private Quaternion oldRotation;
	private float attachTime;
	private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
	private Interactable interactable;

	public GameObject CSVManager;

	//-------------------------------------------------
	void Awake()
	{
		interactable = this.GetComponent<Interactable>();
	}


	//-------------------------------------------------
	// Called when a Hand starts hovering over this object
	//-------------------------------------------------
	private void OnHandHoverBegin(Hand hand)
	{
		hand.ShowGrabHint();
	}


	//-------------------------------------------------
	// Called when a Hand stops hovering over this object
	//-------------------------------------------------
	private void OnHandHoverEnd(Hand hand)
	{
		hand.HideGrabHint();
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
			CSVManager.GetComponent<ReadingCSV>().ReadFile();
		}
		else if (isGrabEnding)
		{

		}
	}


}

