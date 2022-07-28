using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphButtonLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GraphsManager.Instance.FillWithButtons(transform);
    }

}
