using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceGateway : MonoBehaviour
{
    TextMeshProUGUI text;

    [SerializeField] DataType dataType;
    [SerializeField] string value;

    [SerializeField] GameObject nextChoice;

    public void SetText(string choiceText)
    {
        text.SetText(choiceText);
    }

    private void OnTriggerEnter(Collider other)
    {

        nextChoice.SetActive(true);
        gameObject.SetActive(false);    
    }
}
