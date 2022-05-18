using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choice : MonoBehaviour
{
    public DataType dataType;
    [SerializeField] GameObject nextChoice;
    public TextMeshProUGUI percentText;

    public void MoveToNextChoice(Vector3 pos, float percent)
    {
        if (nextChoice != null)
        {
            nextChoice.SetActive(true);
            nextChoice.transform.position = pos;
            nextChoice.GetComponent<Choice>().percentText.SetText(percent + "% survived.");
        }    
        
        gameObject.SetActive(false);
    }
}
