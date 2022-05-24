using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEditorInternal;
using TMPro;
using UnityEngine.UI;


public enum rowType
{
    String,
    Int,
    Float,
    Bool
}

public enum axisType
{
    None,
    X,
    Y,
    Z,
    ShowOnClick
}

[ExecuteInEditMode]
public class Loader : EditorWindow
{
    public List<string> rowNames = new List<string>();
    public List<rowType> rowTypes = new List<rowType>();
    public List<axisType> axisTypes = new List<axisType>();
    public List<GameObject> objects = new List<GameObject>();

    bool dataRead;

    GameObject placeHolder;
    //Mesh mesh;
    //Material material;
    string path;

    [MenuItem("Tools/Loader")]
    static void Init()
    {
        Loader window = EditorWindow.GetWindow<Loader>();
        window.Show();
        window.dataRead = false;
    }

    private void OnGUI()
    {
        if(dataRead)
        {
            for (int i = 0; i < rowNames.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(rowNames[i], new GUILayoutOption[] { GUILayout.Width(100) });
                rowTypes[i] = (rowType)EditorGUILayout.EnumPopup(rowTypes[i]);
                axisTypes[i] = (axisType)EditorGUILayout.EnumPopup(axisTypes[i]);
                EditorGUILayout.EndHorizontal();
            }

            //mesh = (Mesh)EditorGUILayout.ObjectField("Shape (for now)", mesh, typeof(Mesh), false);
            //material = (Material)EditorGUILayout.ObjectField("Material (for now)", material, typeof(Material), false);
            placeHolder = (GameObject)EditorGUILayout.ObjectField("Object", placeHolder, typeof(GameObject), false);

            if (GUILayout.Button("Place Objects"))
            {
                CreateAll();
            }
        }
        else
        {
            if (GUILayout.Button("Load File"))
            {
                path = EditorUtility.OpenFilePanel("CSV", "", "csv");
                if (path != null)
                {
                    LoadRows();
                    dataRead = true;
                }
            }
        }

        //neededDifference = EditorGUILayout.FloatField(neededDifference);
    }

    void CreateAll()
    {
        CreateClass();
        CreateObjects();
        //Waiting();
    }

    void CreateClass()
    {
        string classPath = Application.dataPath + "/DataPoint.cs";
        StreamWriter writer = new StreamWriter(classPath, false);

        writer.WriteLine("using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\nusing UnityEngine.UI;\nusing TMPro;\npublic class DataPoint : MonoBehaviour\n{\n");
        
        for(int i =0; i < rowNames.Count; i++)
        {
            writer.WriteLine("public " + rowTypes[i].ToString().ToLower() + " " + rowNames[i].Replace(" ", "") + ";\n");
        }

        writer.WriteLine("public TextMeshProUGUI displayBox;");
        writer.WriteLine("public void ShowDisplay()\n{\ndisplayBox.enabled = true;\n");
        bool first = true;
        for (int i = 0; i < rowNames.Count; i++)
        {
            if(axisTypes[i] == axisType.ShowOnClick)
            {
                if(!first)
                {
                    writer.WriteLine("+");
                }
                if(first)
                {
                    writer.WriteLine("displayBox.text = ");
                    first = false;
                }
                writer.WriteLine(rowNames[i].Replace(" ", "") + ".ToString() + \"\n\"");
            }
        }
        writer.WriteLine(";\n}");

        writer.WriteLine("public void HideDisplay()\n{\ndisplayBox.enabled = false;\n}");

        writer.WriteLine("\n}");
        writer.Close();
        AssetDatabase.Refresh();
    }

    IEnumerator Waiting()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        CreateObjects();
    }

    void CreateObjects()
    {
        objects.RemoveAll((o) => o == null);
        objects.Clear();

        DataPoint[] currentData = GameObject.FindObjectsOfType<DataPoint>();


        string fileData = System.IO.File.ReadAllText(path);
        string[] lines = fileData.Split("\n"[0]);

        for(int i = 1; i < lines.Length; i++)
        {
            GameObject temp = Instantiate(placeHolder);
            temp.name = i.ToString();
            objects.Add(temp);
            //temp.AddComponent<MeshRenderer>();
            //temp.AddComponent<MeshFilter>();
            //temp.AddComponent<DataPoint>();
            DataPoint tempDP = temp.AddComponent<DataPoint>();

            //temp.GetComponent<MeshFilter>().mesh = mesh;
            //temp.GetComponent<MeshRenderer>().material = material;

            string[] lineData = lines[i].Trim().Split(","[0]);

            for(int j = 0; j < lineData.Length; j++)
            {
                switch(rowTypes[j])
                {
                    case rowType.Bool:
                        tempDP.GetType().GetField(rowNames[j].Replace(" ", "")).SetValue(tempDP, bool.Parse(lineData[j]));
                        break;
                    case rowType.Int:
                        tempDP.GetType().GetField(rowNames[j].Replace(" ", "")).SetValue(tempDP, int.Parse(lineData[j]));
                        break;
                    case rowType.Float:
                        tempDP.GetType().GetField(rowNames[j].Replace(" ", "")).SetValue(tempDP, float.Parse(lineData[j]));
                        break;
                    case rowType.String:
                        tempDP.GetType().GetField(rowNames[j].Replace(" ", "")).SetValue(tempDP, lineData[j]);
                        break;
                }

                if(axisTypes[j] == axisType.X)
                {
                    temp.transform.position = new Vector3(float.Parse(lineData[j]), temp.transform.position.y, temp.transform.position.z);
                }
                if (axisTypes[j] == axisType.Y)
                {
                    temp.transform.position = new Vector3(temp.transform.position.z, float.Parse(lineData[j]),temp.transform.position.z);
                }
                if (axisTypes[j] == axisType.Z)
                {
                    temp.transform.position = new Vector3(temp.transform.position.z, temp.transform.position.y, float.Parse(lineData[j]));
                }
            }

            tempDP.displayBox = temp.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void LoadRows()
    {
        string fileData = System.IO.File.ReadAllText(path);
        string[] lines = fileData.Split("\n"[0]);

        string[] line1 = lines[0].Trim().Split(","[0]);
        string[] line2 = lines[1].Trim().Split(","[0]);
        for (int i = 0; i < line1.Length; i ++)
        {
            rowNames.Add(line1[i]);

            int resultI;
            bool resultB;
            float resultF;
            if(bool.TryParse(line2[i], out resultB))
            {
                rowTypes.Add(rowType.Bool);
            }
            else if(int.TryParse(line2[i], out resultI))
            {
                rowTypes.Add(rowType.Int);
            }
            else if(float.TryParse(line2[i], out resultF))
            {
                rowTypes.Add(rowType.Float);
            }
            else
            {
                rowTypes.Add(rowType.String);
            }

            axisTypes.Add(axisType.None);
        }
    }
}
