using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedWorldSpaceCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
