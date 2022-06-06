using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AxisAdjuster : MonoBehaviour
{
    [SerializeField] axis axis;

    [SerializeField] LinearMapping linearMapping;
    private float currentLinearMapping = float.NaN;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLinearMapping != linearMapping.value)
        {
            currentLinearMapping = linearMapping.value;

            ScatterGenerator.Instance.ChangeAxisScale(currentLinearMapping, axis);
        }
    }
}
