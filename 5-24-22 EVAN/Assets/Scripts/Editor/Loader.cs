using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEditorInternal;


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

    Mesh mesh;
    Material material;
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

            mesh = (Mesh)EditorGUILayout.ObjectField("Shape (for now)", mesh, typeof(Mesh), false);
            material = (Material)EditorGUILayout.ObjectField("Material (for now)", material, typeof(Material), false);

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
    }

    void CreateClass()
    {
        string classPath = Application.dataPath + "/DataPoint.cs";
        StreamWriter writer = new StreamWriter(classPath, false);

        writer.WriteLine("using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\npublic class DataPoint : MonoBehaviour\n{\n");
        
        for(int i =0; i < rowNames.Count; i++)
        {
            //writer.WriteLine("public " + rowTypes[i].ToString())
        }

        writer.WriteLine("\n}");
        writer.Close();
    }

    void PlaceObjects()
    {

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
                rowTypes.Add(rowType.Integer);
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
