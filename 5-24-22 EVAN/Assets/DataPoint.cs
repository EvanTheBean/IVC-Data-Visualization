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
"NAME: " + variables["NAME"][currentC].ToString() + "\n"
+
"x: " + variables["x"][currentC].ToString() + "\n"
+
"y: " + variables["y"][currentC].ToString() + "\n"
+
"z: " + variables["z"][currentC].ToString() + "\n"
+
"random: " + variables["random"][currentC].ToString() + "\n"
+
"This is a bool: " + variables["Thisisabool"][currentC].ToString() + "\n"
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
displayBox.enabled = !displayBox.enabled;

displayBox.text = 
"NAME: " + variables["NAME"][currentC].ToString() + "\n"
+
"x: " + variables["x"][currentC].ToString() + "\n"
+
"y: " + variables["y"][currentC].ToString() + "\n"
+
"z: " + variables["z"][currentC].ToString() + "\n"
+
"random: " + variables["random"][currentC].ToString() + "\n"
+
"This is a bool: " + variables["Thisisabool"][currentC].ToString() + "\n"
;
}}
