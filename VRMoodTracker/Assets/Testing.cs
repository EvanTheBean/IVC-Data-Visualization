using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject CSVManager;
    // Start is called before the first frame update
    void Start()
    {
        CSVManager.GetComponent<ReadingCSV>().ReadFile();
        CSVManager.GetComponent<ReadingCSV>().JoyCounter();
        CSVManager.GetComponent<ReadingCSV>().LoveCounter();
        CSVManager.GetComponent<ReadingCSV>().FearCounter();
        CSVManager.GetComponent<ReadingCSV>().AngerCounter();
        CSVManager.GetComponent<ReadingCSV>().SadnessCounter();
        CSVManager.GetComponent<ReadingCSV>().SurpriseCounter();
    }
}
