using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class DataPoint : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler, INetworkSerializable
{

    [SerializeField] public StringListDictionary variables = new StringListDictionary();
    [SerializeField] public List<string> annotations = new List<string>();
    public TextMeshProUGUI displayBox;
    public int currentC;

    public void OnPointerDown(PointerEventData eventData)
    {
        displayBox.enabled = !displayBox.enabled;
        string display = "";
        foreach (string var in variables.Keys)
        {
            if (var != "Annotations")
                display += var + " " + variables[var][currentC].ToString() + "\n";
        }
        display += "Annotations: ";
        for (int i = 0; i < variables["Annotations"].Count; i++)
        {
            display += variables["Annotations"][0].ToString() + "\n";
        }
        for (int i = 0; i < annotations.Count; i++)
        {
            display += annotations[i].ToString() + "\n";
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

    [ClientRpc]
    void SendDataPointDataClientRpc(DataPoint dataPoint, Vector3 scale, Color meshColor)
    {
        var json = JsonUtility.ToJson(dataPoint);
        JsonUtility.FromJsonOverwrite(json, this);

        transform.localScale = scale;
        GetComponent<MeshRenderer>().material.color = meshColor;

    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref variables);
        serializer.SerializeValue(ref annotations);
    }
}