using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 using UnityEngine.EventSystems; 

 public class DataPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
public TextMeshProUGUI displayBox;
public int currentC;

    public Dictionary<string, List<string>> variables;

public void OnPointerDown(PointerEventData eventData)
{
        variables["Name"][currentC].ToString();
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
