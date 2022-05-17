using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choice_Gateway : MonoBehaviour
{
    TextMeshProUGUI text;



    public void SetText(string choiceText)
    {
        text.SetText(choiceText);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
