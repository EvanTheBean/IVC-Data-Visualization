using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySlider : MonoBehaviour
{
    public float speed;
    public Slider slider;
    public bool playing;
    public bool reverse, bounce, loop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            if (!reverse)
            {
                slider.value += speed * Time.deltaTime;
            }
            else
            {
                slider.value -= speed * Time.deltaTime;
            }

            if (slider.value == slider.minValue && loop)
            {
                slider.value = slider.maxValue;
            }
            else if (slider.value == slider.maxValue && loop)
            {
                slider.value = slider.minValue;
            }
        }

        if ((slider.value == slider.minValue || slider.value == slider.maxValue) && bounce)
        {
            reverse = !reverse;
        }
        else if (slider.value == slider.minValue && !loop)
        {
            playing = false;
            reverse = false;
        }
        else if (slider.value == slider.maxValue)
        {
            playing = false;
            reverse = true;
        }
    }

    public void StartPlaying()
    {
        playing = !playing;
    }

    public void MultiplySpeed(float value)
    {
        speed = speed * value;
    }

    public void SetLoop()
    {
        loop = !loop;
    }

    public void SetBounce()
    {
        bounce = !bounce;
    }
}
