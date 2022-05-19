using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


namespace Valve.VR.InteractionSystem.Sample {
    public class Paddle : MonoBehaviour
    {
        private Interactable interactable;
        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (Hand.AttachmentFlags.VelocityMovement) & (~Hand.AttachmentFlags.ParentToHand);


        void Start()
        {
            interactable = GetComponent<Interactable>();
        }

        private void OnHandHoverBegin(Hand hand)
        {
            hand.ShowGrabHint();
        }

        private void OnHandHoverEnd(Hand hand)
        {
            hand.HideGrabHint();
        }

        private void OnHandHoverUpdate(Hand hand)
        {
            GrabTypes grabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(gameObject);

            if (interactable.attachedToHand == null && grabType != GrabTypes.None)
            {
                hand.AttachObject(gameObject, grabType);
                hand.HoverLock(interactable);
            }
            else if (isGrabEnding)
            {
                hand.DetachObject(gameObject);
                hand.HoverUnlock(interactable);
            }
        }

        private void OnAttachedToHand(Hand hand)
        {

        }

        private void OnDetachedFromHand(Hand hand)
        {

        }

    }
}