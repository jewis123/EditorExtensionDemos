using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BeginScrollViewExample : EditorWindow
{
    Vector2 scrollPos;
    string t = "This is a string inside a Scroll view!";

    [MenuItem("CustomWindow/BeginScrollViewExample")]
    static void Init()
    {
        BeginScrollViewExample window = (BeginScrollViewExample)EditorWindow.GetWindow(typeof(BeginScrollViewExample), true, "My Empty Window");
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
            t += " \nAnd this is more text!";
        EditorGUILayout.BeginHorizontal();
        scrollPos =
            EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(200), GUILayout.Height(100));
        GUILayout.Label(t);
        EditorGUILayout.EndScrollView();

        Rect scrollRect = GUILayoutUtility.GetLastRect();
        Debug.Log(scrollRect);

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Clear"))
            t = "";

        if (GUILayout.Button("Selection Test"))
        {
            GetAllSceneObjectsWithInactive();
        }
    }

    void GetAllSceneObjectsWithInactive()
    {
        var allGos = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        var previousSelection = Selection.objects;
        Selection.objects = allGos;
        var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab | SelectionMode.TopLevel);
        Selection.objects = previousSelection;
        foreach (var trans in selectedTransforms)
        {
            if (trans.name == "CustomPropertyDrawer")
            {
                Selection.objects = new UnityEngine.Object[1] { trans.gameObject };  //重置并选中
                break;
            }
        }
    }
}