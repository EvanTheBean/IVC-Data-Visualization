using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DataPoint : MonoBehaviour
{

public string NAME;

public int x;

public int y;

public int z;

public float random;

public bool Thisisabool;

public TextMeshProUGUI displayBox;
public void ShowDisplay()
{
displayBox.enabled = true;

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

}
