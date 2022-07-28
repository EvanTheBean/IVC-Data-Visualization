using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCanvas : MonoBehaviour
{
    TextMeshProUGUI text;

    static public DebugCanvas Instance;

    public bool debug = true;

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

        text = GetComponentInChildren<TextMeshProUGUI>();        
    }

    public void Log(object output)
    {
        if (debug)
        {
            text.text += output.ToString() + '\n';
        }
    }
}
