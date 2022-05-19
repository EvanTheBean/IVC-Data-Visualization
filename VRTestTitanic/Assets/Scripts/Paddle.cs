using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Paddle : MonoBehaviour
{
	const string WATER_TAG = "Finish";

	private Vector3 oldPosition;
	private float timeDiff;
	public Vector3 velocity;

	private float attachTime;

	private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

	private Interactable interactable;

	[SerializeField] Rigidbody player;

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
			// Save our position/rotation so that we can restore it when we detach
			//oldPosition = transform.position;
			//oldRotation = transform.rotation;

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

			// Restore position/rotation
			//transform.position = oldPosition;
			//transform.rotation = oldRotation;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log(other);

		if (other.tag == WATER_TAG)
        {

			Debug.Log("hi");
			oldPosition = transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == WATER_TAG)
        {
			velocity = (transform.position - oldPosition) / Time.fixedDeltaTime;
			velocity.y = 0;
			oldPosition = transform.position;
			Debug.Log(velocity);
		}
	}


    //-------------------------------------------------
    // Called when this GameObject becomes attached to the hand
    //-------------------------------------------------
    private void OnAttachedToHand(Hand hand)
	{
	}



	//-------------------------------------------------
	// Called when this GameObject is detached from the hand
	//-------------------------------------------------
	private void OnDetachedFromHand(Hand hand)
	{
	}


	//-------------------------------------------------
	// Called every Update() while this GameObject is attached to the hand
	//-------------------------------------------------
	private void HandAttachedUpdate(Hand hand)
	{
		player.AddForce(-velocity);
	}

	private bool lastHovering = false;
	private void Update()
	{
		//if (interactable.isHovering != lastHovering) //save on the .tostrings a bit
		//{
		//	lastHovering = interactable.isHovering;
		//}
	}


	//-------------------------------------------------
	// Called when this attached GameObject becomes the primary attached object
	//-------------------------------------------------
	//private void OnHandFocusAcquired(Hand hand)
	//{
	//}


	//-------------------------------------------------
	// Called when another attached GameObject becomes the primary attached object
	//-------------------------------------------------
	//private void OnHandFocusLost(Hand hand)
	//{
	//}
}