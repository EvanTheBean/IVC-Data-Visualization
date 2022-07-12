using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DataPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] public StringListDictionary variables = new StringListDictionary();
    public TextMeshProUGUI displayBox;
    public int currentC;

    public void OnPointerDown(PointerEventData eventData)
    {
            displayBox.enabled = !displayBox.enabled;
        string display = "";
        foreach(string var in variables.Keys)
        {
            display += var + " " + variables[var][currentC].ToString() + "\n";
        }
        displayBox.text = display;
    }
    public void HideDisplay()
    {
        //displayBox.enabled = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //displayBox.enabled = false;
    }
}