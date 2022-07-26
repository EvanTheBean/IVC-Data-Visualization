using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPositionEditor : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        transform.position = new Vector3(0, -1.24f, 4.26f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
