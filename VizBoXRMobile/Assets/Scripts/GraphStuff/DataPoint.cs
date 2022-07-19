using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class DataPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, INetworkSerializable
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
            if(var != "Annotations")
            display += var + " " + variables[var][currentC].ToString() + "\n";
        }
        display += "Annotations: ";
        for(int i = 0; i < variables["Annotations"].Count; i++)
        {
            display += variables["Annotations"][0].ToString() + "\n";
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

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        //serializer.SerializeValue(ref variables);
    }
}