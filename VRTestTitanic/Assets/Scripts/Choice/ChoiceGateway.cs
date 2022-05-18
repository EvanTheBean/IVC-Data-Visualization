using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceGateway : MonoBehaviour
{
    TextMeshProUGUI text;

    
    [SerializeField] string value;

    [SerializeField] Choice parentChoice;
    
    public void SetText(string choiceText)
    {
        text.SetText(choiceText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerChoices>().GoThroughGateway(new DataEntry(parentChoice.dataType, value));

            float percent = 0;

            switch (parentChoice.dataType) {
                case DataType.Age:
                    percent = FindObjectOfType<DataReader>().CalculateSurvivalChanceAge(int.Parse(value));
                    break;
                case DataType.Class:
                    percent = FindObjectOfType<DataReader>().CalculateSurvivalChanceClass(int.Parse(value));
                    break;
                case DataType.Sex:
                    percent = FindObjectOfType<DataReader>().CalculateSurvivalChanceSex(value[0]);
                    break;
            }

            Vector3 pos = other.transform.position;
            pos.y = 0;
            parentChoice.MoveToNextChoice(pos, percent);
        }
            
    }
}
