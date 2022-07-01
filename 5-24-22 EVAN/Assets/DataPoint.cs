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

displayBox.text = 
"Search Term: " + variables["SearchTerm"][currentC].ToString() + "\n"
+
"Percentage: " + variables["Percentage"][currentC].ToString() + "\n"
+
"Num: " + variables["Num"][currentC].ToString() + "\n"
;
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
{displayBox.enabled = !displayBox.enabled;

displayBox.text = 
"Search Term: " + variables["SearchTerm"][currentC].ToString() + "\n"
+
"Percentage: " + variables["Percentage"][currentC].ToString() + "\n"
+
"Num: " + variables["Num"][currentC].ToString() + "\n"
;
}

 else if (VRToolChange.currentTool == 1) { 
GameObject.FindObjectOfType<Holder>().HideAll(this.gameObject);}
}}
