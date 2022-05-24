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
NAME.ToString()

+
random.ToString()

;
}
public void HideDisplay()
{
displayBox.enabled = false;
}

}
