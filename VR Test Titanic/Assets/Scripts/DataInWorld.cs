using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataReader dataReader = new DataReader("train.csv");
        dataReader.Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
