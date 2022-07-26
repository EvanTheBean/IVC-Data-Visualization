using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class DataPoint : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] public StringListDictionary variables = new StringListDictionary();
    [SerializeField] public List<string> annotations = new List<string>();
    public TextMeshProUGUI displayBox;
    public int currentC;

    private void Start()
    {
        transform.parent.GetComponent<Holder>().objects.Add(gameObject);
    }

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
    void SendDataPointDataClientRpc(DataPointValues vals)
    {
        variables = vals.variables;
        annotations = vals.annotations;

        transform.localScale = vals.scale;
        GetComponent<MeshRenderer>().material.color = vals.meshColor;
    }

    class DataPointValues : INetworkSerializable
    {
        public StringListDictionary variables = new StringListDictionary();
        public List<string> annotations = new List<string>();

        public Vector3 scale;
        public Color meshColor;

        public DataPointValues() { }

        public DataPointValues(DataPoint point)
        {
            variables = point.variables;
            annotations = point.annotations;

            scale = point.transform.localScale;
            meshColor = point.GetComponent<MeshRenderer>().material.color;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref variables);
            serializer.SerializeValue(ref annotations);
            serializer.SerializeValue(ref scale);
            serializer.SerializeValue(ref meshColor);
        }
    }

}