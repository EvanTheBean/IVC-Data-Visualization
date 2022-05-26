using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DataPoint : MonoBehaviour
{

public int PassengerId;

public int Survived;

public int Class;

public string Name;

public string Sex;

public float Age;

public int Sibsp;

public int Parch;

public int Ticket;

public float Fair;

public string Cabin;

public string Embarked;

public TextMeshProUGUI displayBox;
public void ShowDisplay()
{
displayBox.enabled = true;

displayBox.text = 
"Passenger Id: " + PassengerId.ToString() + "\n"
+
"Survived: " + Survived.ToString() + "\n"
+
"Class: " + Class.ToString() + "\n"
+
"Name: " + Name.ToString() + "\n"
+
"Sex: " + Sex.ToString() + "\n"
+
"Age: " + Age.ToString() + "\n"
+
"Sibsp: " + Sibsp.ToString() + "\n"
+
"Parch: " + Parch.ToString() + "\n"
+
"Ticket: " + Ticket.ToString() + "\n"
+
"Fair: " + Fair.ToString() + "\n"
+
"Cabin: " + Cabin.ToString() + "\n"
+
"Embarked: " + Embarked.ToString() + "\n"
;
}
public void HideDisplay()
{
displayBox.enabled = false;
}

}
