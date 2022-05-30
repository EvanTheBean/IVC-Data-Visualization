using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisBall : MonoBehaviour
{
    [SerializeField] TextMeshPro title;
    public int categoryIndex { get; private set; }

    Rigidbody rb;
    Vector3 originalPosition;
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        originalPosition = transform.position;
    }

    public void SetUp(string text, int index)
    {
        title.SetText(text);
        categoryIndex = index;
    }

    public void Lock()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.transform.localPosition = Vector3.zero;
    }

    public void Unlock()
    {
        rb.useGravity = true;
        transform.position = originalPosition;
    }
}
