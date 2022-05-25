using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class ButtonPressed : MonoBehaviour
{
    //Variables for pressing button
    //private Vector3 oldPosition;
    //private Quaternion oldRotation;
    private float attachTime;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    private Interactable interactable;

	//Variables being tracked in the CSV file
	public string ButtonMood;
	public string MoodPressed;
	public string CurrentDate;
	public int DaysTracked;

	public GameObject CSVManager;

	//-------------------------------------------------
	void Awake()
	{
		interactable = this.GetComponent<Interactable>();
		CSVManager.GetComponent<ReadingCSV>().ReadFile();
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
			// Call this to continue receiving HandHoverUpdate messages,
			// and prevent the hand from hovering over anything else
			hand.HoverLock(interactable);

			//Save emotion, date, and time
			MoodPressed = ButtonMood;
			CurrentDate = System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy");

			CSVManager.GetComponent<WritingCSV>().WritingToFile(MoodPressed,CurrentDate);

			//Debug.Log(CurrentDate);
			//Debug.Log(MoodPressed);
		}
		else if (isGrabEnding)
		{
			// Detach this object from the hand
			hand.DetachObject(gameObject);

			// Call this to undo HoverLock
			hand.HoverUnlock(interactable);

			// Restore position/rotation
			//transform.position = oldPosition;
			//transform.rotation = oldRotation;
		}
	}


}
