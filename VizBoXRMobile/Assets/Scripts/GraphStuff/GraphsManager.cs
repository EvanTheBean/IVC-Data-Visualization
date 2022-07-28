using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GraphsManager : MonoBehaviour
{

    public static GraphsManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    List<GameObject> graphs = new List<GameObject>();

    [SerializeField] GameObject buttonPrefab;
    Transform buttonContainerStored;
    public void ReceiveGraph(GameObject graph)
    {
        graphs.Add(graph);

        if (buttonContainerStored != null)
        {
            Button button = Instantiate(buttonPrefab, buttonContainerStored).GetComponent<Button>();
            int x = graphs.Count - 1;
            button.onClick.AddListener(delegate { SelectGraph(x); });

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Graph " + (graphs.Count).ToString() + ": " + graph.GetComponentInChildren<Holder>().chartType.ToString();
        }
    }

    public void FillWithButtons(Transform buttonContainer)
    {
        buttonContainerStored = buttonContainer;
        for (int i = 0; i < graphs.Count; i++)
        {
            Button button = Instantiate(buttonPrefab, buttonContainer).GetComponent<Button>();

            int x = i;
            button.onClick.AddListener(delegate { SelectGraph(x); });

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Graph " + (i+1).ToString() + ": " + graphs[i].GetComponentInChildren<Holder>().chartType.ToString();
        }
    }

    public void SelectGraph(int index)
    {
        Debug.Log(index);
        foreach (GameObject graph in graphs)
        {
            graph.SetActive(false);
        }

        graphs[index].SetActive(true);
        SceneManager.LoadScene("VisualViewer");
    }
}
