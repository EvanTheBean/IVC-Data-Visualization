using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevMode : MonoBehaviour
{
    public CanvasGroup devmode, startup, simulate;
    public Slider waterSlider;
    public GameObject waterPlane;
    public static bool inDevMode;

    UserBasedSimulation usb;
    ControlWaterPlacement cwp;
    // Start is called before the first frame update
    void Start()
    {
        usb = GameObject.FindObjectOfType<UserBasedSimulation>();
        cwp = GameObject.FindObjectOfType<ControlWaterPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inDevMode)
        {
            waterSlider.minValue = cwp.placementPose.position.y;
            waterSlider.maxValue = Camera.main.gameObject.transform.position.y;
            waterPlane.transform.position = new Vector3(0, waterSlider.value, 0);
        }
    }

    public void Activate()
    {
        if(!inDevMode)
        {
            devmode.alpha = 1.0f;
            startup.alpha = 0.0f;
            simulate.alpha = 0.0f;
            inDevMode = true;
            waterPlane.SetActive(true);
        }
        else
        {
            devmode.alpha = 0.0f;
            startup.alpha = 0.0f;
            simulate.alpha = 1.0f;
            inDevMode = false;
            waterPlane.SetActive(false);
        }
    }
}
