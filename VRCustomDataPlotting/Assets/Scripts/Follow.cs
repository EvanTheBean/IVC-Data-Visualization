using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform followedObject;
    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - followedObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followedObject.position + distance;
    }
}
