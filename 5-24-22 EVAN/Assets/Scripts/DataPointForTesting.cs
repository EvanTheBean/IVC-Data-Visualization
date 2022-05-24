using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPointForTesting : MonoBehaviour
{
    public string dataS;
    public float dataF;
    public int dataI;
    public bool dataB;

    public Text displayBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowDisplay()
    {
        displayBox.enabled = true;
        displayBox.text = dataF.ToString() + "\n" + dataI.ToString();
    }

    void HideDisplay()
    {
        displayBox.enabled = false; 
    }
}
