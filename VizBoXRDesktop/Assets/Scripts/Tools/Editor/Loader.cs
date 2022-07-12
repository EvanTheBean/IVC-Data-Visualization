using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System;
using UnityEditorInternal;
using TMPro;
using UnityEngine.UI;



[ExecuteInEditMode]
public class Loader : EditorWindow
{
    bool dataRead;
    bool dataLoaded;

    GameObject placeHolder;
    GameObject placingText;
    //Mesh mesh;
    //Material material;
    //string path;
    Holder holder;
    public List<GameObject> axisText = new List<GameObject>();

    [MenuItem("Tools/Loader")]
    static void Init()
    {
        Loader window = EditorWindow.GetWindow<Loader>();

        window.placeHolder = Resources.Load("Prefabs/placeholder") as GameObject;

        window.Show();
        window.dataRead = false;
        window.holder = GameObject.FindObjectOfType<Holder>();

        if (window.holder.rowNames.Count > 0)
        {
            window.dataRead = true;
        }
        if (window.holder.objects.Count > 0)
        {
            window.dataLoaded = true;
        }

        window.placingText = Resources.Load("Prefabs/AxisText") as GameObject;
        window.axisText = new List<GameObject>(GameObject.FindGameObjectsWithTag("axisText"));

        //Debug.Log("I am being a bitch");
    }

    private void OnGUI()
    {
        if (dataRead)
        {
            GUI.changed = false;
            for (int i = 0; i < holder.rowNames.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(holder.rowNames[i], new GUILayoutOption[] { GUILayout.Width(100) });
                holder.rowTypes[i] = (rowType)EditorGUILayout.EnumPopup(holder.rowTypes[i]);
                holder.axisTypes[i] = (axisType)EditorGUILayout.EnumPopup(holder.axisTypes[i]);
                if (holder.axisTypes[i] == axisType.X || holder.axisTypes[i] == axisType.Y || holder.axisTypes[i] == axisType.Z || holder.axisTypes[i] == axisType.Size)
                {
                    holder.axisScales[i] = EditorGUILayout.Slider(holder.axisScales[i], 0.1f, 5f);
                }
                else if (holder.axisTypes[i] == axisType.Color)
                {
                    holder.axisGradients[i] = EditorGUILayout.GradientField(holder.axisGradients[i]);
                }
                else if (holder.axisTypes[i] == axisType.Connected)
                {
                    holder.connectedTypes[i] = EditorGUILayout.TextField(holder.connectedTypes[i]);
                }
                EditorGUILayout.EndHorizontal();
            }

            //mesh = (Mesh)EditorGUILayout.ObjectField("Shape (for now)", mesh, typeof(Mesh), false);
            //material = (Material)EditorGUILayout.ObjectField("Material (for now)", material, typeof(Material), false);
            placeHolder = (GameObject)EditorGUILayout.ObjectField("Object", placeHolder, typeof(GameObject), false);

            if (GUI.changed)
            {
                ResetAxis();
                UpdateObjects();
            }
            /*
            if (GUILayout.Button("Place Objects"))
            {
                CreateAll();
            }
            */
            /*
            if(holder.axisTypes.Contains(axisType.ShowOnClick))
            {
                if(GUILayout.Button("Change What is Shown"))
                {
                    CreateClass();
                }
            }
            */
            if (holder.axisTypes.Contains(axisType.Connected))
            {
                foreach (string path in holder.path)
                {
                    EditorGUILayout.LabelField(path);
                }
                if (GUILayout.Button("Add File"))
                {
                    holder.path.Add(null);
                    holder.path[holder.path.Count - 1] = EditorUtility.OpenFilePanel("CSV", "", "csv");
                    if (holder.path[holder.path.Count - 1] != null)
                    {
                        LoadSecondFile(holder.path.Count - 1);
                    }
                }
            }

            /*
            if (GUILayout.Button("Load File"))
            {
                holder.path[0] = EditorUtility.OpenFilePanel("CSV", "", "csv");
                if (holder.path[0] != null)
                {
                    LoadRows();
                    dataRead = true;
                    CreateAll();
                    dataLoaded = true;
                }
            }
            */
        }
        else
        {
            placeHolder = (GameObject)EditorGUILayout.ObjectField("Object", placeHolder, typeof(GameObject), false);
            if (GUILayout.Button("Load File"))
            {
                if (holder.path.Count <= 0)
                {
                    holder.path.Add(null);
                }
                holder.path[0] = EditorUtility.OpenFilePanel("CSV", "", "csv");
                if (holder.path != null)
                {
                    // Debug.Log("Loading Rows");
                    LoadRows();
                    dataRead = true;
                    //Debug.Log("Creating All");
                    CreateAll();
                    dataLoaded = true;
                }
            }
        }

        //neededDifference = EditorGUILayout.FloatField(neededDifference);
    }

    void CreateAll()
    {
        //Debug.Log("going to create class");
        //CreateClass();
        //Debug.Log("CreatedClass");
        CreateObjects();
        //Waiting();
    }

    void CreateClass()
    {
        
        //Debug.Log("starting");

        //AssetDatabase.Refresh();

        string classPath = Application.dataPath + "/DataPoint.cs";
        StreamWriter writer = new StreamWriter(classPath, false);

        //AssetDatabase.Refresh();

        writer.WriteLine("using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\nusing UnityEngine.UI;\nusing TMPro;\n using UnityEngine.EventSystems; \n\n public class DataPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler\n{\n");
        writer.WriteLine("[SerializeField] public StringListDictionary variables = new StringListDictionary();");

        /*
        for (int i = 0; i < holder.rowNames.Count; i++)
        {
            writer.WriteLine("public List<" + holder.rowTypes[i].ToString().ToLower() + "> " + holder.rowNames[i].Replace(" ", "") + " = new List<" + holder.rowTypes[i].ToString().ToLower() + ">();");
        }
        */

        writer.WriteLine("public TextMeshProUGUI displayBox;");
        writer.WriteLine("public int currentC;\n");

        //Debug.Log("variables declared");

        writer.WriteLine("public void OnPointerDown(PointerEventData eventData)\n{\nif(VRToolChange.currentTool==0)\n{displayBox.enabled = !displayBox.enabled;\n");
        bool first = true;
        for (int i = 0; i < holder.rowNames.Count; i++)
        {
            //if(holder.axisTypes[i] == axisType.ShowOnClick)
            //{
            if (!first)
            {
                writer.WriteLine("+");
            }
            if (first)
            {
                writer.WriteLine("displayBox.text = ");
                first = false;
            }
            writer.WriteLine("\"" + holder.rowNames[i] + ": \" + variables[\"" + holder.rowNames[i].Replace(" ", "") + "\"][currentC].ToString() + \"\\n\"");
            //}
        }
        writer.WriteLine(";\n}");

        writer.Write("\n else if (VRToolChange.currentTool == 1) { \n");
        writer.Write("GameObject.FindObjectOfType<Holder>().HideAll(this.gameObject);}");

        //Debug.Log("function last");
        writer.WriteLine("\n}");

        //Debug.Log("function1");

        writer.WriteLine("public void HideDisplay()\n{\n//displayBox.enabled = false;\n}");
        writer.WriteLine("public void OnPointerUp(PointerEventData eventData)\n{\n//displayBox.enabled = false;\n}");

        writer.WriteLine("public void OnPointerClick(PointerEventData eventData)\n{\nif(VRToolChange.currentTool==0)\n{displayBox.enabled = !displayBox.enabled;\n");
        first = true;
        for (int i = 0; i < holder.rowNames.Count; i++)
        {
            //if(holder.axisTypes[i] == axisType.ShowOnClick)
            //{
            if (!first)
            {
                writer.WriteLine("+");
            }
            if (first)
            {
                writer.WriteLine("displayBox.text = ");
                first = false;
            }
            writer.WriteLine("\"" + holder.rowNames[i] + ": \" + variables[\"" + holder.rowNames[i].Replace(" ", "") + "\"][currentC].ToString() + \"\\n\"");
            //}
        }
        writer.WriteLine(";\n}");

        writer.Write("\n else if (VRToolChange.currentTool == 1) { \n");
        writer.Write("GameObject.FindObjectOfType<Holder>().HideAll(this.gameObject);}");

        //Debug.Log("function last");
        writer.WriteLine("\n}}");
        writer.Close();
        AssetDatabase.Refresh();

        //Debug.Log("It should be new");
    }

    IEnumerator Waiting()
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        CreateObjects();
    }

    void CreateObjects()
    {
        holder.objects.Clear();
        DataPoint[] currentData = GameObject.FindObjectsOfType<DataPoint>();

        foreach (DataPoint point in currentData)
        {
            holder.objects.Add(point.gameObject);
        }

        //Debug.Log(holder.objects.Count);

        foreach (GameObject @object in holder.objects)
        {
            DestroyImmediate(@object);
        }

        holder.objects.RemoveAll((o) => o == null);


        string fileData = System.IO.File.ReadAllText(holder.path[0]);
        string[] lines = fileData.Split("\n"[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            GameObject temp = Instantiate(placeHolder);
            temp.name = i.ToString();
            holder.objects.Add(temp);
            temp.transform.parent = holder.gameObject.transform;
            //temp.AddComponent<MeshRenderer>();
            //temp.AddComponent<MeshFilter>();
            //temp.AddComponent<DataPoint>();
            DataPoint tempDP;
            temp.AddComponent<DataPoint>();
            tempDP = temp.GetComponent<DataPoint>();

            EditorUtility.SetDirty(tempDP);

            //Debug.Log(i);

            for (int j = 0; j < holder.rowNames.Count; j++)
            {
                //Debug.Log(j + " " + holder.rowNames[j]);
                List<string> tempList = new List<string>();
                tempDP.variables.Add(holder.rowNames[j].Replace(" ", ""), tempList);
                Debug.Log(holder.rowNames[j].Replace(" ", ""));
            }

            foreach (string key in tempDP.variables.Keys)
            {
                //Debug.Log(key + "Key check 1");
            }

            tempDP.currentC = 0;

            //Debug.Log(tempDP.variables.Keys);

            //temp.GetComponent<MeshFilter>().mesh = mesh;
            //temp.GetComponent<MeshRenderer>().material = material;

            string[] lineData = lines[i].Trim().Split(","[0]);

            for (int j = 0; j < holder.rowTypes.Count && j < lineData.Length; j++)
            {
                switch (holder.rowTypes[j])
                {
                    case rowType.Bool:
                        bool replaceB;
                        bool.TryParse(lineData[j], out replaceB);
                        tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceB.ToString());
                        //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceB);
                        break;
                    case rowType.Int:
                        int replaceI;
                        int.TryParse(lineData[j], out replaceI);
                        tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceI.ToString());
                        //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceI);
                        break;
                    case rowType.Float:
                        float replaceF;
                        float.TryParse(lineData[j], out replaceF);
                        tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceF.ToString());
                        //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceF);
                        break;
                    case rowType.String:
                        tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(lineData[j]);
                        //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, lineData[j]);
                        break;
                }

                foreach (string key in tempDP.variables.Keys)
                {
                    //Debug.Log(key + "Key check 2");
                }

                if (holder.axisTypes[j] == axisType.X)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(replaceF * holder.axisScales[j], temp.transform.position.y, temp.transform.position.z);

                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                    }
                    else if (temp.transform.position.y / holder.axisScales[j] < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                    }
                }
                if (holder.axisTypes[j] == axisType.Y)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(temp.transform.position.x, replaceF * holder.axisScales[j], temp.transform.position.z);

                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                    }
                    else if (temp.transform.position.y / holder.axisScales[j] < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                    }

                }
                if (holder.axisTypes[j] == axisType.Z)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, replaceF * holder.axisScales[j]);

                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                    }
                    else if (temp.transform.position.y / holder.axisScales[j] < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                    }
                }
            }

            tempDP.displayBox = temp.GetComponentInChildren<TextMeshProUGUI>();

            foreach (string key in tempDP.variables.Keys)
            {
                //Debug.Log(key + "Key check 3");
            }

            EditorUtility.SetDirty(tempDP);
        }

        //FindObjectOfType<HeatMapShaderMath>().updatePointList();
    }

    void UpdateObjects()
    {
        string fileData = System.IO.File.ReadAllText(holder.path[0]);
        string[] lines = fileData.Split("\n"[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            GameObject temp = holder.objects[i - 1];
            //temp.name = i.ToString();
            //holder.objects.Add(temp);
            //temp.AddComponent<MeshRenderer>();
            //temp.AddComponent<MeshFilter>();
            //temp.AddComponent<DataPoint>();
            DataPoint tempDP = temp.GetComponent<DataPoint>();

            //temp.GetComponent<MeshFilter>().mesh = mesh;
            //temp.GetComponent<MeshRenderer>().material = material;

            string[] lineData = lines[i].Trim().Split(","[0]);

            for (int j = 0; j < holder.axisTypes.Count && j < lineData.Length; j++)
            {
                if (holder.axisTypes[j] == axisType.X)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(replaceF * holder.axisScales[j], temp.transform.position.y, temp.transform.position.z);
                    CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);

                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);
                    }
                    else if (temp.transform.position.y / holder.axisScales[j] < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);
                    }
                }
                if (holder.axisTypes[j] == axisType.Y)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(temp.transform.position.x, replaceF * holder.axisScales[j], temp.transform.position.z);
                    CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);

                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);
                    }
                    else if (temp.transform.position.y / holder.axisScales[j] < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);
                    }

                }
                if (holder.axisTypes[j] == axisType.Z)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, replaceF * holder.axisScales[j]);
                    CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);

                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);
                    }
                    else if (temp.transform.position.y / holder.axisScales[j] < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);
                    }
                }
                if (holder.axisTypes[j] == axisType.Color)
                {
                    if (holder.rowTypes[j] == rowType.Bool)
                    {
                        bool replaceB;
                        bool.TryParse(lineData[j], out replaceB);
                        if (Convert.ToInt16(replaceB) > holder.axisMinMax[j].y)
                        {
                            holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, Convert.ToInt16(replaceB));
                        }
                        else if (Convert.ToInt16(replaceB) < holder.axisMinMax[j].x)
                        {
                            holder.axisMinMax[j] = new Vector2(Convert.ToInt16(replaceB), holder.axisMinMax[j].y);
                        }

                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate(Convert.ToInt16(replaceB) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                    }
                    else if (holder.rowTypes[j] == rowType.Int || holder.rowTypes[j] == rowType.Float)
                    {
                        float replaceF;
                        float.TryParse(lineData[j], out replaceF);

                        if (replaceF > holder.axisMinMax[j].y)
                        {
                            holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        }
                        else if (replaceF < holder.axisMinMax[j].x)
                        {
                            holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        }


                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate(replaceF / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));

                        Debug.Log(holder.axisMinMax[j].y + " " + holder.axisMinMax[j].x);
                        //temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate(replaceF);
                        //Debug.Log(holder.axisGradients[j].Evaluate(replaceF) + " " + replaceF);
                        Debug.Log(holder.axisGradients[j].Evaluate((replaceF - holder.axisMinMax[j].x) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x)) + " " + (replaceF - holder.axisMinMax[j].x) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                        //Debug.Log(replaceF);
                    }
                }
                if (holder.axisTypes[j] == axisType.Size)
                {
                    if (holder.rowTypes[j] == rowType.Bool)
                    {
                        bool replaceB;
                        bool.TryParse(lineData[j], out replaceB);
                        if (Convert.ToInt16(replaceB) > holder.axisMinMax[j].y)
                        {
                            holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, Convert.ToInt16(replaceB));
                        }
                        else if (Convert.ToInt16(replaceB) < holder.axisMinMax[j].x)
                        {
                            holder.axisMinMax[j] = new Vector2(Convert.ToInt16(replaceB), holder.axisMinMax[j].y);
                        }

                        temp.transform.localScale = Vector3.one * Mathf.Lerp(1, 1 * holder.axisScales[j], Convert.ToInt16(replaceB) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                    }
                    else if (holder.rowTypes[j] == rowType.Int || holder.rowTypes[j] == rowType.Float)
                    {
                        float replaceF;
                        float.TryParse(lineData[j], out replaceF);
                        if (replaceF > holder.axisMinMax[j].y)
                        {
                            holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        }
                        else if (replaceF < holder.axisMinMax[j].x)
                        {
                            holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        }

                        temp.transform.localScale = Vector3.one * Mathf.Lerp(1, 1 * holder.axisScales[j], replaceF / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                    }
                }
            }

            //tempDP.displayBox = temp.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void ResetAxis()
    {
        foreach (GameObject temp in holder.objects)
        {
            if (!holder.axisTypes.Contains(axisType.X))
            {
                temp.transform.position = new Vector3(0, temp.transform.position.y, temp.transform.position.z);
            }
            if (!holder.axisTypes.Contains(axisType.Y))
            {
                temp.transform.position = new Vector3(temp.transform.position.x, 0, temp.transform.position.z);
            }
            if (!holder.axisTypes.Contains(axisType.Z))
            {
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, 0);
            }
            if (!holder.axisTypes.Contains(axisType.Color))
            {
                //temp.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            if (!holder.axisTypes.Contains(axisType.Size))
            {
                temp.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
        }
    }



    void CreateAxisRows(Vector2 minMaxLabels, Vector2 minMaxPlacement, axisType correspondingAxis)
    {
        GameObject x = null, y = null, z = null;
        foreach (GameObject temp in axisText)
        {
            if (temp.name == "AxisText X")
            {
                x = temp;
            }
            if (temp.name == "AxisText Y")
            {
                y = temp;
            }
            if (temp.name == "AxisText Z")
            {
                z = temp;
            }
        }

        if (correspondingAxis == axisType.X && x != null)
        {
            x.transform.position = new Vector3(minMaxPlacement.y, 0, 0);
            x.GetComponent<TextMeshProUGUI>().text = minMaxLabels.y.ToString();
            x.GetComponent<LineRenderer>().SetPosition(1, axisText[1].transform.position);
            //Debug.Log(axisText[1].gameObject + " X");
        }
        else if (correspondingAxis == axisType.Y && y != null)
        {
            y.gameObject.transform.position = new Vector3(0, minMaxPlacement.y, 0);
            y.GetComponent<TextMeshProUGUI>().text = minMaxLabels.y.ToString();
            y.GetComponent<LineRenderer>().SetPosition(1, axisText[2].transform.position);
            //Debug.Log(axisText[2].gameObject + " Y");
        }
        else if (correspondingAxis == axisType.Z && z != null)
        {
            z.gameObject.GetComponent<RectTransform>().position = new Vector3(0, 0, minMaxPlacement.y);
            z.GetComponent<TextMeshProUGUI>().text = minMaxLabels.y.ToString();
            z.GetComponent<LineRenderer>().SetPosition(1, axisText[3].transform.position);
            //Debug.Log(axisText[3].gameObject + " Z");
        }

        //Debug.Log(minMaxLabels + " " + minMaxPlacement);
    }

    void LoadRows()
    {
        holder.rowTypes.Clear();
        holder.axisTypes.Clear();
        holder.rowNames.Clear();

        holder.axisScales.Clear();
        holder.axisGradients.Clear();
        holder.axisMinMax.Clear();
        holder.connectedTypes.Clear();

        string fileData = System.IO.File.ReadAllText(holder.path[0]);
        string[] lines = fileData.Split("\n"[0]);

        string[] line1 = lines[0].Trim().Split(","[0]);
        string[] line2 = lines[1].Trim().Split(","[0]);
        for (int i = 0; i < line1.Length; i++)
        {
            holder.rowNames.Add(line1[i]);

            int resultI;
            bool resultB;
            float resultF;
            if (bool.TryParse(line2[i], out resultB))
            {
                holder.rowTypes.Add(rowType.Bool);
            }
            else if (int.TryParse(line2[i], out resultI))
            {
                holder.rowTypes.Add(rowType.Int);
            }
            else if (float.TryParse(line2[i], out resultF))
            {
                holder.rowTypes.Add(rowType.Float);
            }
            else
            {
                holder.rowTypes.Add(rowType.String);
            }

            holder.axisTypes.Add(axisType.None);
            holder.axisScales.Add(1);
            holder.axisGradients.Add(new Gradient());
            holder.axisMinMax.Add(new Vector2(100, -100));
            holder.connectedTypes.Add("");
        }
    }

    public void LoadSecondFile(int fileNum)
    {
        string fileData = System.IO.File.ReadAllText(holder.path[fileNum]);
        string[] lines = fileData.Split("\n"[0]);

        for (int i = 1; i < lines.Length && i < holder.objects.Count; i++)
        {
            string[] lineData = lines[i].Trim().Split(","[0]);

            int rowNum = 0;
            for (int k = 0; k < holder.axisTypes.Count; k++)
            {
                if (holder.axisTypes[k] == axisType.Connected)
                {
                    rowNum = k;
                    break;
                }
            }

            GameObject temp = null;
            if (holder.objects.Find(t => t.GetComponent<DataPoint>().variables[holder.rowNames[rowNum].Replace(" ", "")][0] == lineData[rowNum]))
            {
                Debug.Log("Object Found for " + lineData[rowNum]);
                temp = holder.objects.Find(t => t.GetComponent<DataPoint>().variables[holder.rowNames[rowNum].Replace(" ", "")][0] == lineData[rowNum]);
                //GameObject temp = holder.objects[i];
                DataPoint tempDP = temp.GetComponent<DataPoint>();

                EditorUtility.SetDirty(tempDP);

                for (int j = 0; j < holder.rowTypes.Count && j < lineData.Length; j++)
                {

                    switch (holder.rowTypes[j])
                    {
                        case rowType.Bool:
                            bool replaceB;
                            bool.TryParse(lineData[j], out replaceB);
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceB.ToString());
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceB);
                            break;
                        case rowType.Int:
                            int replaceI;
                            int.TryParse(lineData[j], out replaceI);
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceI.ToString());
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceI);
                            break;
                        case rowType.Float:
                            float replaceF;
                            float.TryParse(lineData[j], out replaceF);
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceF.ToString());
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceF);
                            break;
                        case rowType.String:
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(lineData[j]);
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, lineData[j]);
                            break;
                    }
                }

                foreach (string key in tempDP.variables.Keys)
                {
                    //Debug.Log(key + "Key check 4");
                }

                EditorUtility.SetDirty(tempDP);
            }
            else
            {
                Debug.Log("No Object Found for " + lineData[rowNum]);
                temp = Instantiate(placeHolder);
                temp.name = lineData[rowNum];
                holder.objects.Add(temp);
                temp.transform.parent = holder.gameObject.transform;
                //temp.AddComponent<MeshRenderer>();
                //temp.AddComponent<MeshFilter>();
                //temp.AddComponent<DataPoint>();
                DataPoint tempDP;
                temp.AddComponent<DataPoint>();
                tempDP = temp.GetComponent<DataPoint>();

                tempDP.displayBox = temp.GetComponentInChildren<TextMeshProUGUI>();

                EditorUtility.SetDirty(tempDP);

                for (int j = 0; j < holder.rowNames.Count; j++)
                {
                    //Debug.Log(j + " " + holder.rowNames[j]);
                    List<string> tempList = new List<string>();
                    tempDP.variables.Add(holder.rowNames[j].Replace(" ", ""), tempList);
                    Debug.Log("Adding Variable " + (holder.path.Count - 1));

                    for (int k = 0; k < holder.path.Count - 1; k++)
                    {
                        if (holder.axisTypes[j] == axisType.Connected)
                        {
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(lineData[rowNum]);
                        }
                        else
                        {
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add("-1");
                        }
                        Debug.Log("Adding Empty");
                    }
                }

                EditorUtility.SetDirty(tempDP);

                for (int j = 0; j < holder.rowTypes.Count && j < lineData.Length; j++)
                {

                    switch (holder.rowTypes[j])
                    {
                        case rowType.Bool:
                            bool replaceB;
                            bool.TryParse(lineData[j], out replaceB);
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceB.ToString());
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceB);
                            break;
                        case rowType.Int:
                            int replaceI;
                            int.TryParse(lineData[j], out replaceI);
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceI.ToString());
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceI);
                            break;
                        case rowType.Float:
                            float replaceF;
                            float.TryParse(lineData[j], out replaceF);
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(replaceF.ToString());
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, replaceF);
                            break;
                        case rowType.String:
                            tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(lineData[j]);
                            //tempDP.GetType().GetField(holder.rowNames[j].Replace(" ", "") + "[0]").SetValue(tempDP, lineData[j]);
                            break;
                    }
                }

                EditorUtility.SetDirty(tempDP);

                UpdateObjects();
            }
        }

        foreach (GameObject temp in holder.objects)
        {
            DataPoint tempDP = temp.GetComponent<DataPoint>();
            if (tempDP.variables[holder.rowNames[holder.axisTypes.IndexOf(axisType.Connected)].Replace(" ", "")].Count < holder.path.Count)
            {
                for (int j = 0; j < holder.rowNames.Count; j++)
                {
                    if (holder.axisTypes[j] == axisType.Connected)
                    {
                        tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]);
                    }
                    else
                    {
                        tempDP.variables[holder.rowNames[j].Replace(" ", "")].Add("-1");
                    }
                    Debug.Log("Adding Empty");
                }
            }
        }
    }
}