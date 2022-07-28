using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System;
using System.Linq;
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
    MultiPointControll mpc;
    public List<GameObject> axisText = new List<GameObject>();

    int holderNum;
    List<Holder> allHolders = new List<Holder>();

    int AxisShowNum;

    GradientUsageAttribute gua = new GradientUsageAttribute(false);

    ChartType prevchartType;

    [MenuItem("Tools/Graphing")]
    static void Init()
    {
        Loader window = EditorWindow.GetWindow<Loader>();

        window.placeHolder = Resources.Load("Prefabs/placeholder") as GameObject;

        window.Show();
        window.dataRead = false;
        window.holder = GameObject.FindObjectOfType<Holder>();
        window.mpc = GameObject.FindObjectOfType<MultiPointControll>();

        if(window.holder == null)
        {
            window.dataRead = false;
            window.dataLoaded = false;
        }

        if (window.holder.rowNames.Count > 0)
        {
            window.dataRead = true;
        }
        if (window.holder.objects.Count > 0)
        {
            window.dataLoaded = true;
        }
        window.
        allHolders = GameObject.FindObjectsOfType<Holder>().ToList();

        //window.placingText = Resources.Load("Prefabs/AxisText") as GameObject;
        //window.axisText = new List<GameObject>(GameObject.FindGameObjectsWithTag("axisText"));
        //Debug.Log("I am being a bitch");
        Gradients g = new Gradients();
        window.gua = new GradientUsageAttribute(false);
    }

    private void OnGUI()
    {
        EditorUtility.SetDirty(holder);
        if (holder.dataRead)
        {
            GUI.changed = false;

            prevchartType = holder.chartType;
            holder.chartType = (ChartType)EditorGUILayout.EnumPopup("Chart Type: ", holder.chartType);
            GUILayout.Space(10);

            for (int i = 0; i < holder.rowNames.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(holder.rowNames[i], new GUILayoutOption[] { GUILayout.Width(100) });
                holder.rowTypes[i] = (rowType)EditorGUILayout.EnumPopup(holder.rowTypes[i]);
                if(holder.rowTypes[i] == rowType.String)
                {
                    EditorGUIUtility.labelWidth = 80;
                    holder.catagorical[i] = EditorGUILayout.Toggle("Catagorical: ", holder.catagorical[i], GUILayout.ExpandWidth(false));
                    if(holder.catagorical[i])
                    {
                        GetCatagories(i);
                        EditorGUIUtility.labelWidth = 5;
                        EditorGUILayout.LabelField(holder.catagories[i].getCount().ToString(),GUILayout.ExpandWidth(false));
                    }
                }
                EditorGUIUtility.labelWidth = 0;
                GUIContent label = new GUIContent("");
                AxisShowNum = i;
                holder.axisTypes[i] = (axisType)EditorGUILayout.EnumPopup(label, holder.axisTypes[i], ShowAxisType, true);
                if (holder.axisTypes[i] == axisType.X || holder.axisTypes[i] == axisType.Y || holder.axisTypes[i] == axisType.Z || holder.axisTypes[i] == axisType.Width || holder.axisTypes[i] == axisType.Length || holder.axisTypes[i] == axisType.Height)
                {
                    EditorGUIUtility.labelWidth = 40;
                    holder.axisScales[i] = EditorGUILayout.FloatField("Scale",holder.axisScales[i]);
                    if (holder.axisTypes[i] == axisType.X || holder.axisTypes[i] == axisType.Y || holder.axisTypes[i] == axisType.Z)
                    {
                        EditorGUIUtility.labelWidth = 40;
                        holder.offsets[i] = EditorGUILayout.FloatField("Offset: ", holder.offsets[i]);
                        EditorGUIUtility.labelWidth = 0;
                        if (GUILayout.Button("Mean"))
                        {
                            GetMinMax(i);
                            holder.offsets[i] = -Mathf.Lerp(holder.axisMinMax[i].x, holder.axisMinMax[i].y, 0.5f);
                        }
                    }
                    if(holder.axisTypes[i] == axisType.Width || holder.axisTypes[i] == axisType.Height || holder.axisTypes[i] == axisType.Length)
                    {
                        EditorGUIUtility.labelWidth = 60;
                        holder.centered[i] = EditorGUILayout.Toggle("Centered: ", holder.centered[i]);
                    }
                    EditorGUIUtility.labelWidth = 0;
                    if (holder.chartType == ChartType.Line)
                    {
                        EditorGUIUtility.labelWidth = 40;
                        if (holder.axisTypes[i] == axisType.X)
                        {
                            holder.xLines = EditorGUILayout.Toggle("Lines: ", holder.xLines);
                            if (holder.xLines)
                            {
                                holder.zLines = false;
                                holder.yLines = false;
                            }
                        }
                        if (holder.axisTypes[i] == axisType.Y)
                        {
                            holder.yLines = EditorGUILayout.Toggle("Lines: ", holder.yLines);
                            if(holder.yLines)
                            {
                                holder.zLines = false;
                            }
                        }
                        if (holder.axisTypes[i] == axisType.Z)
                        {
                            holder.zLines = EditorGUILayout.Toggle("Lines: ", holder.zLines);
                        }
                        EditorGUIUtility.labelWidth = 0;
                    }
                }
                else if (holder.axisTypes[i] == axisType.Size)
                {
                    holder.axisScales[i] = EditorGUILayout.Slider(holder.axisScales[i], 0.1f, 5f);
                    EditorGUIUtility.labelWidth = 40;
                    holder.offsets[i] = EditorGUILayout.FloatField("Offset: ", holder.offsets[i]);
                    EditorGUIUtility.labelWidth = 0;
                }
                else if (holder.axisTypes[i] == axisType.Color)
                {
                    holder.gTypes[i] = (gradientTypes)EditorGUILayout.EnumPopup(holder.gTypes[i]);
                    if(holder.gTypes[i] == gradientTypes.sequential)
                    {
                        holder.sequentialGradientsNames[i] = (sequentialGradientsNames)EditorGUILayout.EnumPopup(holder.sequentialGradientsNames[i]);
                        Gradients g = new Gradients();
                        g.Reset();
                        holder.axisGradients[i] = Gradients.sequentialGradients[(int)holder.sequentialGradientsNames[i]];
                    }
                    else if (holder.gTypes[i] == gradientTypes.diverging)
                    {
                        holder.divergingGradientsNames[i] = (divergingGradientsNames)EditorGUILayout.EnumPopup(holder.divergingGradientsNames[i]);
                        Gradients g = new Gradients();
                        g.Reset();
                        holder.axisGradients[i] = Gradients.divergingGradients[(int)holder.divergingGradientsNames[i]];
                    }
                    else if (holder.gTypes[i] == gradientTypes.catagorical)
                    {
                        holder.catagoricalGradientsNames[i] = (catagoricalGradientsNames)EditorGUILayout.EnumPopup(holder.catagoricalGradientsNames[i]);
                        Gradients g = new Gradients();
                        g.Reset();
                        holder.axisGradients[i] = Gradients.catagoricalGradients[(int)holder.catagoricalGradientsNames[i]];
                    }
                    holder.axisGradients[i] = EditorGUILayout.GradientField(holder.axisGradients[i]);

                    EditorGUIUtility.labelWidth = 40;
                    holder.Dalpha = EditorGUILayout.Slider("Alpha: ", holder.Dalpha, 0f, 1f);
                    holder.offsets[i] = EditorGUILayout.FloatField("Offset: ", holder.offsets[i]);
                    EditorGUIUtility.labelWidth = 0;
                }
                else if (holder.axisTypes[i] == axisType.Connected)
                {
                    holder.connectedTypes[i] = (ConnectedTypes)EditorGUILayout.EnumPopup(holder.connectedTypes[i]);
                }
                else if (holder.axisTypes[i] == axisType.Positional)
                {
                    EditorGUIUtility.labelWidth = 40;
                    holder.axisScales[i] = EditorGUILayout.Slider("Scale", holder.axisScales[i], 0.1f, 5f);
                    EditorGUIUtility.labelWidth = 60;
                    holder.randomness[i] = EditorGUILayout.Slider("Randomness", holder.randomness[i], 0.1f, 5f);
                    EditorGUIUtility.labelWidth = 40;
                    holder.offsets[i] = EditorGUILayout.FloatField("Offset: ", holder.offsets[i]);
                    EditorGUIUtility.labelWidth = 0;
                }
                EditorGUILayout.EndHorizontal();
            }

            //mesh = (Mesh)EditorGUILayout.ObjectField("Shape (for now)", mesh, typeof(Mesh), false);
            //material = (Material)EditorGUILayout.ObjectField("Material (for now)", material, typeof(Material), false);

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
            EditorGUILayout.BeginHorizontal();
            if(!holder.axisTypes.Contains(axisType.Size))
            {
                EditorGUIUtility.labelWidth = 100;
                holder.overScale = EditorGUILayout.FloatField("Overall Scale: ", holder.overScale);
                if (holder.overScale < 0)
                {
                    holder.overScale = -holder.overScale;
                }
            }
            if(!holder.axisTypes.Contains(axisType.Color))
            {
                EditorGUIUtility.labelWidth = 125;
                holder.defaultColor = EditorGUILayout.ColorField("Default Color: ", holder.defaultColor);
                holder.Dalpha = holder.defaultColor.a;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            string label2;
            if (holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Y) && holder.axisTypes.Contains(axisType.Z))
            {
                EditorGUIUtility.labelWidth = 120;
                label2 = "add plane of best fit: ";
            }
            else
            {
                EditorGUIUtility.labelWidth = 110;
                label2 = "add line of best fit: ";
            }
            holder.bestFit = EditorGUILayout.Toggle(label2, holder.bestFit);
            if(holder.bestFit)
            {
                BestFit best = new BestFit();
                if(holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Y) && holder.axisTypes.Contains(axisType.Z))
                {
                    best.FitIndexed3D(holder);
                    holder.GetComponent<LineRenderer>().enabled = false;
                    holder.plane.GetComponent<MeshRenderer>().enabled = true;
                }
                else if(holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Y))
                {
                    //XY line
                    holder.GetComponent<LineRenderer>().enabled = true;
                    holder.plane.GetComponent<MeshRenderer>().enabled = false;
                    best.FitIndexed(true, true, false, holder);
                }
                else if (holder.axisTypes.Contains(axisType.Z) && holder.axisTypes.Contains(axisType.Y))
                {
                    holder.GetComponent<LineRenderer>().enabled = true;
                    holder.plane.GetComponent<MeshRenderer>().enabled = false;
                    best.FitIndexed(false, true, true, holder);
                    //YZ line
                }
                else if (holder.axisTypes.Contains(axisType.Z) && holder.axisTypes.Contains(axisType.X))
                {
                    holder.GetComponent<LineRenderer>().enabled = true;
                    holder.plane.GetComponent<MeshRenderer>().enabled = false;
                    best.FitIndexed(true, false, true, holder);
                    //XZ line
                }
            }
            else
            {
                holder.GetComponent<LineRenderer>().enabled = false;
                holder.plane.GetComponent<MeshRenderer>().enabled = false;
            }
            EditorGUILayout.Space(15);
            EditorGUIUtility.labelWidth = 40;
            EditorGUILayout.LabelField("Add line at = n : ");
            EditorGUIUtility.labelWidth = 15;
            holder.xN = EditorGUILayout.Toggle("x: ", holder.xN);
            if(holder.xN)
            {
                holder.Xn = EditorGUILayout.FloatField(holder.Xn);
                LineRenderer lr = holder.XN.GetComponent<LineRenderer>();
                int pos = holder.axisTypes.IndexOf(axisType.X);
                lr.positionCount = 3;
                if(holder.axisTypes.Contains(axisType.Y))
                {
                    lr.SetPosition(0, new Vector3(holder.Xn * holder.axisScales[pos] + holder.offsets[pos], holder.axisMinMax[holder.axisTypes.IndexOf(axisType.Y)].y, 0));
                }
                else
                {
                    lr.SetPosition(0, new Vector3(holder.Xn * holder.axisScales[pos], 0, 0));
                }
                lr.SetPosition(1, new Vector3(holder.Xn * holder.axisScales[pos] + holder.offsets[pos], 0, 0));
                if (holder.axisTypes.Contains(axisType.Z))
                {
                    lr.SetPosition(2, new Vector3(holder.Xn * holder.axisScales[pos] + holder.offsets[pos], 0,holder.axisMinMax[holder.axisTypes.IndexOf(axisType.Z)].y));
                }
                else
                {
                    lr.SetPosition(2, new Vector3(holder.Xn * holder.axisScales[pos] + holder.offsets[pos], 0, 0));
                }
                lr.enabled = true;
            }
            else
            {
                LineRenderer lr = holder.XN.GetComponent<LineRenderer>();
                lr.enabled = false;
            }
            holder.yN = EditorGUILayout.Toggle("y: ", holder.yN);
            if (holder.yN)
            {
                holder.Yn = EditorGUILayout.FloatField(holder.Yn);
                LineRenderer lr = holder.YN.GetComponent<LineRenderer>();
                int pos = holder.axisTypes.IndexOf(axisType.Y);
                lr.positionCount = 3;
                if (holder.axisTypes.Contains(axisType.X))
                {
                    lr.SetPosition(0, new Vector3(holder.axisMinMax[holder.axisTypes.IndexOf(axisType.X)].y,holder.Yn * holder.axisScales[pos] + holder.offsets[pos], 0));
                }
                else
                {
                    lr.SetPosition(0, new Vector3(0,holder.Yn * holder.axisScales[pos] + holder.offsets[pos], 0));
                }
                lr.SetPosition(1, new Vector3(0,holder.Yn * holder.axisScales[pos] + holder.offsets[pos], 0));
                if (holder.axisTypes.Contains(axisType.Z))
                {
                    lr.SetPosition(2, new Vector3(0,holder.Yn * holder.axisScales[pos] + holder.offsets[pos], holder.axisMinMax[holder.axisTypes.IndexOf(axisType.Z)].y));
                }
                else
                {
                    lr.SetPosition(2, new Vector3(0,holder.Yn * holder.axisScales[pos] + holder.offsets[pos], 0));
                }
                lr.enabled = true;
            }
            else
            {
                LineRenderer lr = holder.YN.GetComponent<LineRenderer>();
                lr.enabled = false;
            }
            holder.zN = EditorGUILayout.Toggle("z: ", holder.zN);
            if (holder.zN)
            {
                LineRenderer lr = holder.ZN.GetComponent<LineRenderer>();
                holder.Zn = EditorGUILayout.FloatField(holder.Zn);
                int pos = holder.axisTypes.IndexOf(axisType.Z);
                lr.positionCount = 3;
                if (holder.axisTypes.Contains(axisType.X))
                {
                    lr.SetPosition(0, new Vector3(holder.axisMinMax[holder.axisTypes.IndexOf(axisType.X)].y,0, holder.Zn * holder.axisScales[pos] + holder.offsets[pos]));
                }
                else
                {
                    lr.SetPosition(0, new Vector3(0, 0,holder.Zn * holder.axisScales[pos] + holder.offsets[pos]));
                }
                lr.SetPosition(1, new Vector3(0,0,holder.Zn * holder.axisScales[pos] + holder.offsets[pos]));
                if (holder.axisTypes.Contains(axisType.Y))
                {
                    lr.SetPosition(2, new Vector3(0, holder.axisMinMax[holder.axisTypes.IndexOf(axisType.Y)].y, holder.Zn * holder.axisScales[pos] + holder.offsets[pos]));
                }
                else
                {
                    lr.SetPosition(2, new Vector3(0,0, holder.Zn * holder.axisScales[pos] + holder.offsets[pos]));
                }
                lr.enabled = true;
            }
            else
            {
                LineRenderer lr = holder.ZN.GetComponent<LineRenderer>();
                lr.enabled = false;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            EditorGUIUtility.labelWidth = 0;
            //placeHolder = (GameObject)EditorGUILayout.ObjectField("Object", placeHolder, typeof(GameObject), false);
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
                    else
                    {
                        holder.path.RemoveAt(holder.path.Count - 1);
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
            //placeHolder = (GameObject)EditorGUILayout.ObjectField("Object", placeHolder, typeof(GameObject), false);
            if(placeHolder != null)
            {
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
                        holder.dataRead = true;
                        //Debug.Log("Creating All");
                        CreateAll();
                        holder.dataLoaded = true;
                    }
                }
            }
        }


        GUILayout.Space(50);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("<"))
        {
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(holder);
            holderNum--;
            if (holderNum < 0)
            {
                holderNum = allHolders.Count - 1;
            }
            else if (holderNum > allHolders.Count - 1)
            {
                holderNum = 0;
            }
            holder = allHolders[holderNum];
            Selection.activeGameObject = holder.gameObject;
        }
        holder.name = EditorGUILayout.TextField(holder.name);
        holder.gameObject.active = EditorGUILayout.Toggle(holder.gameObject.active);
        if (GUILayout.Button(">"))
        {
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(holder);
            holderNum++;
            if (holderNum < 0)
            {
                holderNum = allHolders.Count - 1;
            }
            else if (holderNum > allHolders.Count - 1)
            {
                holderNum = 0;
            }
            holder = allHolders[holderNum];
            Selection.activeGameObject = holder.gameObject;
        }

        GUILayout.Space(10);

        if (allHolders.Count > 1)
        {
            if (GUILayout.Button("-"))
            {
                allHolders.Remove(holder);
                DestroyImmediate(holder.gameObject);
                holderNum--;
                //allHolders = GameObject.FindObjectsOfType<Holder>();
                if (holderNum < 0)
                {
                    holderNum = 0;
                }
                holder = allHolders[holderNum];
                Selection.activeGameObject = holder.gameObject;
            }
        }
        if (GUILayout.Button("+"))
        {
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(holder);
            GameObject temp = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/HOLDER")) as GameObject;
            temp.name = "HOLDER " + (allHolders.Count + 1);
            holder = temp.GetComponent<Holder>();
            allHolders.Add(holder);
            Selection.activeGameObject = holder.gameObject;
        }

        GUILayout.EndHorizontal();


        if (GUILayout.Button("Reset Holder"))
        {
            foreach (GameObject @object in holder.objects)
            {
                DestroyImmediate(@object);
            }

            holder.objects.RemoveAll((o) => o == null);

            holder.path.Add(null);

            holder.dataLoaded = false;
            holder.dataRead = false;

            holder.Reset();

            placeHolder = Resources.Load("Prefabs/placeholder") as GameObject;
        }

        if (GUI.changed)
        {
            ResetAxis();
            UpdateObjects();
        }

        //neededDifference = EditorGUILayout.FloatField(neededDifference);

        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(holder);
        /*
        if(Selection.activeGameObject.GetComponent<Holder>() != null)
        {
            holder = Selection.activeGameObject.GetComponent<Holder>();
        }
        */
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
        //holder.objects.Clear();
        //DataPoint[] currentData = GameObject.FindObjectsOfType<DataPoint>();

        //foreach (DataPoint point in currentData)
        //{
        //    holder.objects.Add(point.gameObject);
        //}

        //Debug.Log(holder.objects.Count);

        foreach (GameObject @object in holder.objects)
        {
            DestroyImmediate(@object);
        }

        holder.objects.RemoveAll((o) => o == null);


        string fileData = System.IO.File.ReadAllText(holder.path[0]);
        string[] lines = fileData.Split("\n"[0]);

        for (int i = 1; i < lines.Length - 1; i++)
        {
            GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab(placeHolder as GameObject);
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
                List<string> kList = new List<string>();
                tempDP.variables.Add(holder.rowNames[j].Replace(" ", ""), kList);
                //Debug.Log(holder.rowNames[j].Replace(" ", ""));
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
                    temp.transform.position = new Vector3(replaceF * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);

                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                    }
                    */
                }
                if (holder.axisTypes[j] == axisType.Y)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(temp.transform.position.x, replaceF * holder.axisScales[j] + holder.offsets[j], temp.transform.position.z);

                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                    }
                    */

                }
                if (holder.axisTypes[j] == axisType.Z)
                {
                    float replaceF;
                    float.TryParse(lineData[j], out replaceF);
                    temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, replaceF * holder.axisScales[j] + holder.offsets[j]);

                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                    }
                    */
                }
            }

            List<string> tempList = new List<string>();
            tempDP.variables.Add("Annotations", tempList);
            tempDP.displayBox = temp.GetComponentInChildren<TextMeshProUGUI>();

            foreach (string key in tempDP.variables.Keys)
            {
                //Debug.Log(key + "Key check 3");
            }

            EditorUtility.SetDirty(tempDP);
            //UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(tempDP);
        }

        //FindObjectOfType<HeatMapShaderMath>().updatePointList();
    }

    void UpdateObjects()
    {
        bool changeMesh = false;
        Mesh mesh = null;
        if(holder.chartType == ChartType.Bar && prevchartType != ChartType.Bar)
        {
            changeMesh = true;
            GameObject temp = Resources.Load("Models/cube") as GameObject;
            mesh = temp.GetComponent<MeshFilter>().sharedMesh;
            Debug.Log(mesh.name);
        }
        if((holder.chartType ==  ChartType.Scatter || holder.chartType == ChartType.Line) && prevchartType == ChartType.Bar)
        {
            changeMesh = true;
            GameObject temp = Resources.Load("Models/sphereDefault") as GameObject;
            mesh = temp.GetComponent<MeshFilter>().sharedMesh;
        }

        //string fileData = System.IO.File.ReadAllText(holder.path[0]);
        //string[] lines = fileData.Split("\n"[0]);

        for (int i = 0; i < holder.objects.Count; i++)
        {
            GameObject temp = holder.objects[i];
            //temp.name = i.ToString();
            //holder.objects.Add(temp);
            //temp.AddComponent<MeshRenderer>();
            //temp.AddComponent<MeshFilter>();
            //temp.AddComponent<DataPoint>();
            DataPoint tempDP = temp.GetComponent<DataPoint>();

            if(changeMesh)
            {
                temp.GetComponent<MeshFilter>().mesh = mesh;
            }

            //temp.GetComponent<MeshFilter>().mesh = mesh;
            //temp.GetComponent<MeshRenderer>().material = material;

            for (int j = 0; j < holder.axisTypes.Count; j++)
            {
                if (holder.axisTypes[j] == axisType.X)
                {
                    float replaceF;
                    if(float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        temp.transform.position = new Vector3(replaceF * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);
                    }
                    if (holder.catagorical[j])
                    {
                        temp.transform.position = new Vector3(holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.position.y, temp.transform.position.z);
                    }

                    GetMinMax(j);

                    CreateAxisRows(new Vector2((holder.axisMinMax[j].x + holder.offsets[j]) / holder.axisScales[j], (holder.axisMinMax[j].y + holder.offsets[j]) / holder.axisScales[j]), new Vector2(holder.offsets[j], holder.axisMinMax[j].y), axisType.X);
                    //Debug.Log(holder.axisMinMax[j].y);

                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        Debug.Log("Bigger X");
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j] + holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        Debug.Log("SmallerX");
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j] + holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.X);
                    }
                    */
                }
                if (holder.axisTypes[j] == axisType.Y)
                {
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        temp.transform.position = new Vector3(temp.transform.position.x, replaceF * holder.axisScales[j] + holder.offsets[j], temp.transform.position.z);
                    }
                    if (holder.catagorical[j])
                    {
                        temp.transform.position = new Vector3(temp.transform.position.x, holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.position.z);
                    }

                    GetMinMax(j);

                    //CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j] + holder.offsets[j]), axisType.Y);
                    CreateAxisRows(new Vector2((holder.axisMinMax[j].x + holder.offsets[j]) / holder.axisScales[j], (holder.axisMinMax[j].y + holder.offsets[j]) / holder.axisScales[j]), new Vector2(holder.offsets[j], holder.axisMinMax[j].y), axisType.Y);
                    //Debug.Log(holder.axisMinMax[j].y);
                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        Debug.Log("Bigger Y");
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        Debug.Log("SmallerY");
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Y);
                    }
                    */

                }
                if (holder.axisTypes[j] == axisType.Z)
                {
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, replaceF * holder.axisScales[j] + holder.offsets[j]);
                    }
                    if (holder.catagorical[j])
                    {
                        temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j]);
                    }

                    GetMinMax(j);

                    //CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.offsets[j], holder.axisMinMax[j].y * holder.axisScales[j] + holder.offsets[j]), axisType.Z);
                    CreateAxisRows(new Vector2((holder.axisMinMax[j].x + holder.offsets[j]) / holder.axisScales[j], (holder.axisMinMax[j].y + holder.offsets[j]) / holder.axisScales[j]), new Vector2(holder.offsets[j], holder.axisMinMax[j].y), axisType.Z);
                    //Debug.Log(holder.axisMinMax[j].y);
                    /*
                    if (replaceF > holder.axisMinMax[j].y)
                    {
                        Debug.Log("GreaterZ");
                        holder.axisMinMax[j] = new Vector2(holder.axisMinMax[j].x, replaceF);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);
                    }
                    else if (temp.transform.position.y < holder.axisMinMax[j].x)
                    {
                        Debug.Log("SmallerZ");
                        holder.axisMinMax[j] = new Vector2(replaceF, holder.axisMinMax[j].y);
                        CreateAxisRows(holder.axisMinMax[j], new Vector2(holder.axisMinMax[j].x * holder.axisScales[j], holder.axisMinMax[j].y * holder.axisScales[j]), axisType.Z);
                    }
                    */
                }
                if(holder.axisTypes[j] == axisType.Height)
                {
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        temp.transform.localScale = new Vector3(temp.transform.localScale.x, replaceF * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.z);
                        if(!holder.axisTypes.Contains(axisType.Y)  && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.localScale.y / 2f, temp.transform.position.z);
                        }
                    }
                    if (holder.catagorical[j])
                    {
                        temp.transform.localScale = new Vector3(temp.transform.localScale.x,holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.z);
                        if (!holder.axisTypes.Contains(axisType.Y) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.localScale.y / 2f, temp.transform.position.z);
                        }
                    }
                }
                if (holder.axisTypes[j] == axisType.Width)
                {
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        temp.transform.localScale = new Vector3( replaceF * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.y, temp.transform.localScale.z);
                        if (!holder.axisTypes.Contains(axisType.X) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.localScale.x / 2f, temp.transform.position.y, temp.transform.position.z);
                        }
                    }
                    if (holder.catagorical[j])
                    {
                        temp.transform.localScale = new Vector3(holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j], temp.transform.localScale.y, temp.transform.localScale.z);
                        if (!holder.axisTypes.Contains(axisType.X) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.localScale.x / 2f, temp.transform.position.y, temp.transform.position.z);
                        }
                    }
                }
                if (holder.axisTypes[j] == axisType.Length)
                {
                    float replaceF;
                    if (float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF))
                    {
                        temp.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y,replaceF * holder.axisScales[j] + holder.offsets[j]);
                        if (!holder.axisTypes.Contains(axisType.Z) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.localScale.z / 2f);
                        }
                    }
                    if (holder.catagorical[j])
                    {
                        temp.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j]);
                        if (!holder.axisTypes.Contains(axisType.Z) && !holder.centered[j])
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.localScale.z / 2f);
                        }
                    }
                }
                if (holder.axisTypes[j] == axisType.Color)
                {
                    GetMinMax(j);

                    if (holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]);
                        float count = holder.catagories[j].getCount();
                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate((num + holder.offsets[j] )/ count);
                        //Debug.Log((num + (float)holder.offsets[j]) / count);
                    }
                    else if (holder.rowTypes[j] == rowType.Bool)
                    {
                        bool replaceB;
                        bool.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceB);

                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate((Convert.ToInt16(replaceB) - holder.axisMinMax[j].x) + holder.offsets[j] / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                    }
                    else if (holder.rowTypes[j] == rowType.Int || holder.rowTypes[j] == rowType.Float)
                    {
                        float replaceF;
                        float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF);


                        temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate(((replaceF - holder.axisMinMax[j].x) + holder.offsets[j]) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                        //Debug.Log(holder.axisMinMax[j]);
                        //Debug.Log(((replaceF - holder.axisMinMax[j].x) + holder.offsets[j]) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));

                        //Debug.Log(holder.axisMinMax[j].y + " " + holder.axisMinMax[j].x);
                        //temp.GetComponent<MeshRenderer>().material.color = holder.axisGradients[j].Evaluate(replaceF);
                        //Debug.Log(holder.axisGradients[j].Evaluate(replaceF) + " " + replaceF);
                        //Debug.Log(holder.axisGradients[j].Evaluate((replaceF - holder.axisMinMax[j].x) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x)) + " " + (replaceF - holder.axisMinMax[j].x) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x));
                        //Debug.Log(replaceF);
                    }
                    Color c = temp.GetComponent<MeshRenderer>().material.color;
                    c.a = holder.Dalpha;
                    temp.GetComponent<MeshRenderer>().material.color = c;
                }
                if (holder.axisTypes[j] == axisType.Size)
                {
                    GetMinMax(j);
                    if (holder.offsets[j] == 0)
                    {
                        holder.offsets[j] = 1f;
                    }
                    if(holder.catagorical[j])
                    {
                        float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]);
                        float count = holder.catagories[j].getCount();
                        temp.transform.localScale = Vector3.one * (Mathf.Lerp(1, holder.axisScales[j], (num) / count) + holder.offsets[j]);
                    }
                    else  if (holder.rowTypes[j] == rowType.Bool)
                    {
                        bool replaceB;
                        bool.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceB);

                        temp.transform.localScale = Vector3.one * Mathf.Lerp(1, 1 * holder.axisScales[j], Convert.ToInt16(replaceB) / (holder.axisMinMax[j].y - holder.axisMinMax[j].x)) * holder.offsets[j];
                    }
                    else if (holder.rowTypes[j] == rowType.Int || holder.rowTypes[j] == rowType.Float)
                    {
                        float replaceF;
                        float.TryParse(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0], out replaceF);
                        //Debug.Log(replaceF + " " + holder.axisMinMax[j]);
                        temp.transform.localScale = Vector3.one * Mathf.Lerp(1, holder.axisScales[j], replaceF / (holder.axisMinMax[j].y - holder.axisMinMax[j].x)) * holder.offsets[j];
                    }
                }
                if (holder.axisTypes[j] == axisType.Positional)
                {
                    float num = holder.catagories[j].IndexOf(tempDP.variables[holder.rowNames[j].Replace(" ", "")][0]) * holder.axisScales[j] + holder.offsets[j];
                    if (!holder.axisTypes.Contains(axisType.X))
                    {
                        temp.transform.position = new Vector3(num + UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j], temp.transform.position.y, temp.transform.position.z);
                    }
                    if (!holder.axisTypes.Contains(axisType.Y))
                    {
                        if (holder.axisTypes.Contains(axisType.X) && holder.axisTypes.Contains(axisType.Z))
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, num + UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j], temp.transform.position.z);
                        }
                        else
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j], temp.transform.position.z);
                        }
                    }
                    if (!holder.axisTypes.Contains(axisType.Z))
                    {
                        if(holder.axisTypes.Contains(axisType.X))
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, num + UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j]);
                        }
                        else
                        {
                            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, UnityEngine.Random.Range(0, 0.5f) * holder.randomness[j]);
                        }
                    }

                }
            }


            //tempDP.displayBox = temp.GetComponentInChildren<TextMeshProUGUI>();
        }

        for (int i = 0; i < holder.axisTypes.Count; i++)
        {
            if(holder.axisTypes[i] == axisType.Connected)
            {
                ConnectedTypes type = holder.connectedTypes[i];
                if (type == ConnectedTypes.Time)
                {
                    mpc.TimeBased(true);
                    mpc.ControlLines(false);
                }
                else if (type == ConnectedTypes.VisibleLines)
                {
                    mpc.TimeBased(false);
                    mpc.ControlLines(true);
                }
                else if (type == ConnectedTypes.TimeLines)
                {
                    mpc.TimeBased(true);
                    mpc.ControlLines(true);
                }
                else if (type == ConnectedTypes.Visible)
                {
                    mpc.TimeBased(false);
                    mpc.ControlLines(false);
                }
            }
            if (holder.axisTypes[i] == axisType.Lines)
            {
                List<GameObject> connected = new List<GameObject>(holder.objects);
                connected = connected.OrderBy(x => x.GetComponent<DataPoint>().variables[holder.rowNames[i].Replace(" ", "")][x.GetComponent<DataPoint>().currentC]).ToList();
                LineRenderer lr = holder.lineGraph.GetComponent<LineRenderer>();
                lr.positionCount = connected.Count;
                for (int j = 0; j < lr.positionCount; j ++)
                {
                    lr.SetPosition(j, connected[j].transform.position);
                }
            }
            if (!holder.axisTypes.Contains(axisType.Lines) && holder.chartType == ChartType.Line)
            {
                List<GameObject> connected = new List<GameObject>(holder.objects);
                if(!holder.xLines && !holder.yLines && !holder.zLines)
                {
                    connected = connected.OrderBy(x => x.transform.position.magnitude).ToList();
                }
                else if(holder.yLines)
                {
                    connected = connected.OrderBy(x => x.transform.position.y).ToList();
                }
                else if (holder.xLines)
                {
                    connected = connected.OrderBy(x => x.transform.position.x).ToList();
                }
                else if (holder.zLines)
                {
                    connected = connected.OrderBy(x => x.transform.position.z).ToList();
                }
                LineRenderer lr = holder.lineGraph.GetComponent<LineRenderer>();
                lr.positionCount = connected.Count;
                for (int j = 0; j < lr.positionCount; j++)
                {
                    lr.SetPosition(j, connected[j].transform.position);
                    //Debug.Log(connected[j].transform.position.magnitude + " " + connected[j].name);
                }
            }
        }

        if(holder.chartType == ChartType.Line)
        {
            holder.lineGraph.GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            holder.lineGraph.GetComponent<LineRenderer>().enabled = false;
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
                temp.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            if (!holder.axisTypes.Contains(axisType.Size))
            {
                if(!holder.axisTypes.Contains(axisType.Height))
                {
                    temp.transform.localScale = new Vector3(temp.transform.localScale.x, holder.overScale, temp.transform.localScale.z);
                }
                if (!holder.axisTypes.Contains(axisType.Width))
                {
                    temp.transform.localScale = new Vector3(holder.overScale, temp.transform.localScale.y, temp.transform.localScale.z);
                }
                if (!holder.axisTypes.Contains(axisType.Length))
                {
                    temp.transform.localScale = new Vector3(temp.transform.localScale.x, temp.transform.localScale.y, holder.overScale);
                }
            }
            if(!holder.axisTypes.Contains(axisType.Color))
            {
                temp.GetComponent<MeshRenderer>().material.color = holder.defaultColor;
            }
        }
    }



    void CreateAxisRows(Vector2 minMaxLabels, Vector2 minMaxPlacement, axisType correspondingAxis)
    {
        GameObject x = holder.AxisX, y = holder.AxisY, z = holder.AxisZ, parent = null;

        parent = x.transform.parent.gameObject;

        if (correspondingAxis == axisType.X && x != null)
        {
            x.transform.position = new Vector3(minMaxPlacement.y, 0, 0);
            x.GetComponent<TextMeshProUGUI>().text = minMaxLabels.y.ToString();
            x.GetComponent<LineRenderer>().SetPosition(1,x.transform.position);
            x.GetComponent<LineRenderer>().SetPosition(0, new Vector3(minMaxPlacement.x,0,0));
            //Debug.Log(axisText[1].gameObject + " X");
        }
        else if (correspondingAxis == axisType.Y && y != null)
        {
            y.gameObject.transform.position = new Vector3(0, minMaxPlacement.y, 0);
            y.GetComponent<TextMeshProUGUI>().text = minMaxLabels.y.ToString();
            y.GetComponent<LineRenderer>().SetPosition(1, y.transform.position);
            y.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0,minMaxPlacement.x,0));
            //Debug.Log(axisText[2].gameObject + " Y");
        }
        else if (correspondingAxis == axisType.Z && z != null)
        {
            z.gameObject.GetComponent<RectTransform>().position = new Vector3(0, 0, minMaxPlacement.y);
            z.GetComponent<TextMeshProUGUI>().text = minMaxLabels.y.ToString();
            z.GetComponent<LineRenderer>().SetPosition(1, z.transform.position);
            z.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, minMaxPlacement.x));
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

        holder.divergingGradientsNames.Clear();
        holder.catagoricalGradientsNames.Clear();
        holder.sequentialGradientsNames.Clear();
        holder.gTypes.Clear();
        holder.catagorical.Clear();
        holder.offsets.Clear();

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
            holder.connectedTypes.Add(ConnectedTypes.Time);
            holder.divergingGradientsNames.Add(divergingGradientsNames.seismic);
            holder.catagoricalGradientsNames.Add(catagoricalGradientsNames.pastels);
            holder.sequentialGradientsNames.Add(sequentialGradientsNames.viridis);
            holder.catagorical.Add(false);
            holder.gTypes.Add(gradientTypes.sequential);
            holder.offsets.Add(0f);
            holder.isCatagorical.Add(false);
            ListWrapper temp = new ListWrapper();
            holder.catagories.Add(temp);
            holder.randomness.Add(1);
            holder.centered.Add(false);
            //Debug.Log("Adding " + temp);
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
                //Debug.Log("Object Found for " + lineData[rowNum]);
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
                //Debug.Log("No Object Found for " + lineData[rowNum]);
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
                    //Debug.Log("Adding Variable " + (holder.path.Count - 1));

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
                        //Debug.Log("Adding Empty");
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

                //UpdateObjects();
            }

            //UpdateObjects();
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
                    //Debug.Log("Adding Empty");
                }
            }
        }

        UpdateObjects();
    }

    void GetMinMax(int location)
    {
        bool first = true;
        foreach(GameObject temp in holder.objects)
        {
            if(first)
            {
                first = false;
                if(holder.axisTypes[location] == axisType.X)
                {
                    holder.axisMinMax[location] = new Vector2(temp.transform.position.x -holder.offsets[location], temp.transform.position.x - holder.offsets[location]);
                }
                if (holder.axisTypes[location] == axisType.Y)
                {
                    holder.axisMinMax[location] = new Vector2(temp.transform.position.y - holder.offsets[location], temp.transform.position.y - holder.offsets[location]);
                }
                if (holder.axisTypes[location] == axisType.Z)
                {
                    holder.axisMinMax[location] = new Vector2(temp.transform.position.z - holder.offsets[location], temp.transform.position.z - holder.offsets[location]);
                }
                if (holder.axisTypes[location] == axisType.Color)
                {
                    if(holder.rowTypes[location]!= rowType.String)
                    {
                        if (holder.rowTypes[location] == rowType.Bool)
                        {
                            holder.axisMinMax[location] = new Vector2(bool.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) ? 1 : 0, bool.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) ? 1 : 0);
                        }
                        else
                        {
                            holder.axisMinMax[location] = new Vector2(float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]), float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]));
                        }
                    }
                }
                if(holder.axisTypes[location] == axisType.Size)
                {
                    if (holder.rowTypes[location] != rowType.String)
                        holder.axisMinMax[location] = new Vector2(float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) - holder.offsets[location], float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) - holder.offsets[location]);
                }
            }
            else
            {
                if (holder.axisTypes[location] == axisType.X)
                {
                    if(temp.transform.position.x - holder.offsets[location] > holder.axisMinMax[location].y)
                    {
                        holder.axisMinMax[location] = new Vector2(holder.axisMinMax[location].x, temp.transform.position.x - holder.offsets[location]);
                    }
                    else if(temp.transform.position.x - holder.offsets[location] < holder.axisMinMax[location].x)
                    {
                        holder.axisMinMax[location] = new Vector2(temp.transform.position.x - holder.offsets[location], holder.axisMinMax[location].y);
                    }
                }
                if (holder.axisTypes[location] == axisType.Y)
                {
                    if (temp.transform.position.y - holder.offsets[location] > holder.axisMinMax[location].y)
                    {
                        holder.axisMinMax[location] = new Vector2(holder.axisMinMax[location].x, temp.transform.position.y - holder.offsets[location]);
                    }
                    else if (temp.transform.position.y - holder.offsets[location] < holder.axisMinMax[location].x)
                    {
                        holder.axisMinMax[location] = new Vector2(temp.transform.position.y - holder.offsets[location], holder.axisMinMax[location].y);
                    }
                }
                if (holder.axisTypes[location] == axisType.Z)
                {
                    if (temp.transform.position.z - holder.offsets[location] > holder.axisMinMax[location].y)
                    {
                        holder.axisMinMax[location] = new Vector2(holder.axisMinMax[location].x, temp.transform.position.z - holder.offsets[location]);
                    }
                    else if (temp.transform.position.z - holder.offsets[location] < holder.axisMinMax[location].x)
                    {
                        holder.axisMinMax[location] = new Vector2(temp.transform.position.z - holder.offsets[location], holder.axisMinMax[location].y);
                    }
                }
                if (holder.axisTypes[location] == axisType.Color)
                {
                    if (holder.rowTypes[location] != rowType.String)
                    {
                        float num = 0;
                        if (holder.rowTypes[location] == rowType.Bool)
                        {
                            num = bool.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) ? 1f : 0f;
                        }
                        else
                        {
                            num = float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]);
                        }
                        if (num > holder.axisMinMax[location].y)
                        {
                            holder.axisMinMax[location] = new Vector2(holder.axisMinMax[location].x, num);
                        }
                        else if (num < holder.axisMinMax[location].x)
                        {
                            holder.axisMinMax[location] = new Vector2(num, holder.axisMinMax[location].y);
                        }
                    }
                }
                if(holder.axisTypes[location] == axisType.Size)
                {
                    if (holder.rowTypes[location] != rowType.String)
                    {
                        if (float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) - holder.offsets[location] > holder.axisMinMax[location].y)
                        {
                            holder.axisMinMax[location] = new Vector2(holder.axisMinMax[location].x, float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) - holder.offsets[location]);
                        }
                        else if (float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) - holder.offsets[location] < holder.axisMinMax[location].x)
                        {
                            holder.axisMinMax[location] = new Vector2(float.Parse(temp.GetComponent<DataPoint>().variables[holder.rowNames[location].Replace(" ", "")][0]) - holder.offsets[location], holder.axisMinMax[location].y);
                        }
                    }
                }
            }
        }
    }

    void GetCatagories(int location)
    {
        //Debug.Log(holder.catagories.Count);
        foreach (GameObject temp in holder.objects)
        {
            DataPoint tempDp = temp.GetComponent<DataPoint>();
            //for(int i = 0; i < tempDp.variables[holder.rowNames[location].Replace(" ", "")].Count; i ++)
            //{
            //Debug.Log(holder.catagories[location]);
                if (!holder.catagories[location].Contains(tempDp.variables[holder.rowNames[location].Replace(" ", "")][0]))
                {
                    holder.catagories[location].Add(tempDp.variables[holder.rowNames[location].Replace(" ", "")][0]);
                }
            //}
        }
    }

    // Basic fitting algorithm. See ApprQuery.h for the various Fit(...)
    // functions that you can call.

    bool ShowAxisType(Enum value)
    {
        if (holder.rowTypes[AxisShowNum] == rowType.String && !holder.catagorical[AxisShowNum])
        {
            switch (value)
            {
                case axisType.Connected:
                case axisType.None:
                    return true;
                default:
                    return false;
            }
        }

        if (holder.catagorical[AxisShowNum] && holder.rowTypes[AxisShowNum] == rowType.String)
        {
            switch (value)
            {
                case axisType.Positional:
                    return true;
                default:
                    break;
            }
        }



        if (holder.chartType == ChartType.Scatter)
        {
            switch (value)
            {
                case axisType.Lines:
                case axisType.Width:
                case axisType.Height:
                case axisType.Length:
                case axisType.ShowOnClick:
                case axisType.Positional:
                    return false;
                default:
                    return true;
            }
        }
        if (holder.chartType == ChartType.Line)
        {
            switch (value)
            {
                case axisType.Width:
                case axisType.Height:
                case axisType.Length:
                case axisType.ShowOnClick:
                case axisType.Positional:
                    return false;
                default:
                    return true;
            }
        }
        if (holder.chartType == ChartType.Bar)
        {
            switch (value)
            {
                case axisType.Lines:
                case axisType.ShowOnClick:
                case axisType.Positional:
                case axisType.Size:
                    return false;
                default:
                    return true;
            }
        }

        return true;
    }
}