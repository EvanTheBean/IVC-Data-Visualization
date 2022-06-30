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
        if (VRToolChange.currentTool == 0)
        {
            Debug.Log("tool 0, the text");
            displayBox.enabled = !displayBox.enabled;

            displayBox.text =
            "Search Term: " + variables["SearchTerm"][currentC].ToString() + "\n"
            +
            "Percentage: " + variables["Percentage"][currentC].ToString() + "\n"
            +
            "Num: " + variables["Num"][currentC].ToString() + "\n"
            ;
        }

        else if (VRToolChange.currentTool == 1)
        {
            Debug.Log("Trying to hide objects now :)");
            GameObject.FindObjectOfType<Holder>().HideAll(this.gameObject);
        }
        else
        {
            Debug.Log("For some reason this is not tool 1???");
        }
    }
public void HideDisplay()
{
displayBox.enabled = false;
}
public void OnPointerUp(PointerEventData eventData)
{
displayBox.enabled = false;
}
public void OnPointerClick(PointerEventData eventData)
{
if(VRToolChange.currentTool==0)
{
            Debug.Log("tool 0, the text");
            displayBox.enabled = !displayBox.enabled;

displayBox.text = 
"Search Term: " + variables["SearchTerm"][currentC].ToString() + "\n"
+
"Percentage: " + variables["Percentage"][currentC].ToString() + "\n"
+
"Num: " + variables["Num"][currentC].ToString() + "\n"
;
}

 else if (VRToolChange.currentTool == 1) {
            Debug.Log("Trying to hide objects now :)");
GameObject.FindObjectOfType<Holder>().HideAll(this.gameObject);}
else
        {
            Debug.Log("For some reason this is not tool 1???");
        }
}}
