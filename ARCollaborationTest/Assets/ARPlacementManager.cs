using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlacementManager : MonoBehaviour
{
    public static ARPlacementManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ReCreatePlacement(Transform t)
    {
        throw new NotImplementedException();
    }
}
