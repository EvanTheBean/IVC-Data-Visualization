using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGraphController : MonoBehaviour
{
    Transform graph;

    // Start is called before the first frame update
    void Start()
    {
        graph = FindObjectOfType<Holder>().transform;
        graph.gameObject.SetActive(true);
        graph.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
