using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample {
public class Paddle : MonoBehaviour
{
    /*Code Goals: Player will be able to do the following:
     * Pick up paddle
     * Move paddle
     * Put down paddle
     * If paddle falls off boat, paddle will respawn
     */

    /*Variables Needed:
     * Paddle Collider
     * "Under the Map" Collider
     *  Respawn Location
     */
    private Interactable interactable;

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Pick up paddle
    public void pickUpPaddle()
    {

    }

    //If paddle falls off boat, paddle will respawn
    public void paddleRespawn()
    {

    }
}
}