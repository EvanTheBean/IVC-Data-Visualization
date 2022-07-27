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
public class Annotator : EditorWindow
{

    public SerializedObject _objectSO = null;
    public ReorderableList _listRE = null;

    public GameObject lastSelected;

    Material _outline;

    [MenuItem("Tools/Annotation")]
    static void Init()
    {
        Annotator window = EditorWindow.GetWindow<Annotator>();
        window.Show();
        window._outline = Resources.Load("M_Outline") as Material;
    }

    private void OnGUI()
    {
        if(Selection.count >= 1)
        {
            if (Selection.activeGameObject.GetComponent<DataPoint>() != null && Selection.objects.Length == 1)
            {
                DataPoint point = Selection.activeGameObject.GetComponent<DataPoint>();

                if (ContainsName(point.GetComponent<MeshRenderer>().materials.ToList(), _outline) != -1)
                {
                    if (GUILayout.Button("UnStar"))
                    {
                        List<Material> newM = point.GetComponent<MeshRenderer>().materials.ToList();
                        newM.RemoveAt(ContainsName(point.GetComponent<MeshRenderer>().materials.ToList(), _outline));
                        point.GetComponent<MeshRenderer>().materials = newM.ToArray();
                    }
                }
                else if (ContainsName(point.GetComponent<MeshRenderer>().materials.ToList(), _outline) == -1)
                {

                    if (GUILayout.Button("Star"))
                    {
                        List<Material> newM = point.GetComponent<MeshRenderer>().materials.ToList();
                        newM.Add(_outline);
                        point.GetComponent<MeshRenderer>().materials = newM.ToArray();
                    }
                }

                _objectSO = new SerializedObject(point);
                _listRE = new ReorderableList(_objectSO, _objectSO.FindProperty("annotations"), true, true, true, true);

                _listRE.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Annotations for " + point.name);
                _listRE.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    rect.y += 2f;
                    rect.height = EditorGUIUtility.singleLineHeight;
                    GUIContent label = new GUIContent($"Annotation {index + 1}");
                    EditorGUI.PropertyField(rect, _listRE.serializedProperty.GetArrayElementAtIndex(index), label);

                };
                _listRE.onAddCallback = (rect) =>
                {
                    _listRE.serializedProperty.arraySize++;
                    var newElement = _listRE.serializedProperty.GetArrayElementAtIndex(_listRE.serializedProperty.arraySize - 1);
                    newElement.stringValue = "";
                };

                //GUILayout.Label("Annotations", EditorStyles.boldLabel);
                _objectSO.Update();
                _listRE.DoList(new Rect(new Vector2(10, 20), Vector2.one * 500f));
                _objectSO.ApplyModifiedProperties();
            }
            else
            {
                EditorGUILayout.LabelField("Please select ONE data point");
            }
        }
        else
        {
            EditorGUILayout.LabelField("Nothing selected");
        }
    }

    private void Update()
    {
        if(lastSelected != Selection.activeGameObject)
        {
            Repaint();
        }
        lastSelected = Selection.activeGameObject;
    }

    int ContainsName(List<Material> mats, Material mat)
    {
        foreach (Material temp in mats)
        {
            if(temp.name.Contains(mat.name))
            {
                return mats.IndexOf(temp);
            }
        }

        return -1;
    }
}
