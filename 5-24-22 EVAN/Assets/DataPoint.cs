using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DataPoint : MonoBehaviour
{

public string tag;

public string description;

public int languageset1freq;

public int languageset2freq;

public float languageset1relativefreq;

public float languageset2relativefreq;

public float relativedifference;

public float llvalue;

public int languageset1messagefreq;

public int languageset2messagefreq;

public float languageset1messagerelativefreq;

public int languageset2messagerelativefreq;

public int messagerelativedifference;

public string examples;

public string examples1;

public string examples2;

public string examples3;

public string examples4;

public TextMeshProUGUI displayBox;
public void ShowDisplay()
{
displayBox.enabled = true;

displayBox.text = 
"tag: " + tag.ToString() + "\n"
+
"description: " + description.ToString() + "\n"
+
"language set 1 freq: " + languageset1freq.ToString() + "\n"
+
"language set 2 freq: " + languageset2freq.ToString() + "\n"
+
"language set 1 relative freq: " + languageset1relativefreq.ToString() + "\n"
+
"language set 2 relative freq: " + languageset2relativefreq.ToString() + "\n"
+
"relative difference: " + relativedifference.ToString() + "\n"
+
"ll value: " + llvalue.ToString() + "\n"
+
"language set 1 message freq: " + languageset1messagefreq.ToString() + "\n"
+
"language set 2 message freq: " + languageset2messagefreq.ToString() + "\n"
+
"language set 1 message relative freq: " + languageset1messagerelativefreq.ToString() + "\n"
+
"language set 2 message relative freq: " + languageset2messagerelativefreq.ToString() + "\n"
+
"message relative difference: " + messagerelativedifference.ToString() + "\n"
+
"examples: " + examples.ToString() + "\n"
+
"examples1: " + examples1.ToString() + "\n"
+
"examples2: " + examples2.ToString() + "\n"
+
"examples3: " + examples3.ToString() + "\n"
+
"examples4: " + examples4.ToString() + "\n"
;
}
public void HideDisplay()
{
displayBox.enabled = false;
}

}
