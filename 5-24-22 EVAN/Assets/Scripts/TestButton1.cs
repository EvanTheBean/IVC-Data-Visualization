using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestButton1 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add()
    {
        count++;
        text.text = count.ToString();
    }
}
