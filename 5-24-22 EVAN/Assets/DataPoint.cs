using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 using UnityEngine.EventSystems; 

 public class DataPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

public string NAME;

public int x;

public int y;

public int z;

public float random;

public bool Thisisabool;

public TextMeshProUGUI displayBox;
public void OnPointerDown(PointerEventData eventData)
{
displayBox.enabled = !displayBox.enabled;
        Debug.Log(displayBox.enabled);

displayBox.text = 
"NAME: " + NAME.ToString() + "\n"
+
"x: " + x.ToString() + "\n"
+
"y: " + y.ToString() + "\n"
+
"z: " + z.ToString() + "\n"
+
"random: " + random.ToString() + "\n"
+
"This is a bool: " + Thisisabool.ToString() + "\n"
;
}
public void HideDisplay()
{
displayBox.enabled = false;
}
public void OnPointerUp(PointerEventData eventData)
{
//displayBox.enabled = false;
}

public void OnPointerClick(PointerEventData eventData)
{
displayBox.enabled = !displayBox.enabled;
        Debug.Log(displayBox.enabled);

displayBox.text = 
"NAME: " + NAME.ToString() + "\n"
+
"x: " + x.ToString() + "\n"
+
"y: " + y.ToString() + "\n"
+
"z: " + z.ToString() + "\n"
+
"random: " + random.ToString() + "\n"
+
"This is a bool: " + Thisisabool.ToString() + "\n"
;
}

}
