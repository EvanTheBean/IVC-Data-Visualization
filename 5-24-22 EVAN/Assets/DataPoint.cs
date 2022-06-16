using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 using UnityEngine.EventSystems; 

 public class DataPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

public List<string> NAME = new List<string>();
public List<int> x = new List<int>();
public List<int> y = new List<int>();
public List<int> z = new List<int>();
public List<float> random = new List<float>();
public List<bool> Thisisabool = new List<bool>();
public TextMeshProUGUI displayBox;
public int currentC;

public void OnPointerDown(PointerEventData eventData)
{
displayBox.enabled = !displayBox.enabled;

displayBox.text = 
"NAME: " + NAME[currentC].ToString() + "\n"
+
"x: " + x[currentC].ToString() + "\n"
+
"y: " + y[currentC].ToString() + "\n"
+
"z: " + z[currentC].ToString() + "\n"
+
"random: " + random[currentC].ToString() + "\n"
+
"This is a bool: " + Thisisabool[currentC].ToString() + "\n"
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
"NAME: " + NAME[currentC].ToString() + "\n"
+
"x: " + x[currentC].ToString() + "\n"
+
"y: " + y[currentC].ToString() + "\n"
+
"z: " + z[currentC].ToString() + "\n"
+
"random: " + random[currentC].ToString() + "\n"
+
"This is a bool: " + Thisisabool[currentC].ToString() + "\n"

}
