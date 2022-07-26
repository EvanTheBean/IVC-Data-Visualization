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

    [MenuItem("Tools/Annotation")]
    static void Init()
    {
        Annotator window = EditorWindow.GetWindow<Annotator>();
        window.Show();
    }

    private void OnGUI()
    {
        if(Selection.activeGameObject.GetComponent<DataPoint>() != null && Selection.objects.Length == 1)
        {
            DataPoint point = Selection.activeGameObject.GetComponent<DataPoint>();
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
               _listRE.list.Add("");
            };

            GUILayout.Label("Annotations", EditorStyles.boldLabel);
            _objectSO.Update();
            _listRE.DoList(new Rect(new Vector2(0, 50), Vector2.one * 500f));
            _objectSO.ApplyModifiedProperties();
        }
    }
}
