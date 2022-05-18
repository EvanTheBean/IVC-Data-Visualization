using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choice : MonoBehaviour
{
    public DataType dataType;
    [SerializeField] GameObject nextChoice;
    public TextMeshProUGUI percentText;
    [SerializeField] GameObject finalResults;

    public void MoveToNextChoice(Vector3 pos, float percent)
    {
        if (nextChoice != null)
        {
            nextChoice.SetActive(true);
            nextChoice.transform.position = pos;
            nextChoice.GetComponent<Choice>().percentText.SetText(percent + "% survived.");
        }
        else
        {
            finalResults.SetActive(true);
            finalResults.transform.position = pos;
            percent = FindObjectOfType<PlayerChoices>().DetermineSurvivalability();
            finalResults.GetComponent<Choice>().percentText.SetText("Total results: " + percent + "% of people like you survived.");
            
        }
        
        gameObject.SetActive(false);
    }
}
