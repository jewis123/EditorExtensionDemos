using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DemoWindow : EditorWindow
{

    public NodeScriptableObject nso;
    public GameObject prefab;
    bool bInited = false;
    private List<GameObject> objs = new List<GameObject>();

   [MenuItem("CustomWindow/ShowWindow")]
   private static void ShowWindow()
    {
        EditorWindow window = GetWindow<DemoWindow>();
        window.Show();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        DrawContent();
    }

    private void OnDestroy() {
        foreach(var obj in objs){
            DestroyImmediate(obj);
        }
    }

    private void DrawContent()
    {
        EditorGUILayout.BeginVertical();
        for(int i = 0; i < nso.pos.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            nso.pos[i] = EditorGUILayout.Vector3Field(string.Format("{0}{1}","ndoe",i),nso.pos[i]);
            if(EditorGUI.EndChangeCheck() & objs.Count > 0){
                objs[i].transform.position = nso.pos[i];
            }
            EditorGUILayout.BeginHorizontal();
            
            if(GUILayout.Button("remove")){
                RemoveItem(i);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
        }
        if (GUILayout.Button("add"))
        {
            AddItem();
        }
        else if (GUILayout.Button("Init All Nodess"))
        {
            InitAllNodes();
        }
        EditorGUILayout.EndVertical();
    }

    private void AddItem()
    {
        nso.pos.Add(Vector3.zero);
        int index = nso.pos.Count - 1 ;
        if (objs.Count > 0) { 
            Create(index);
        }
    }

    private void RemoveItem(int index)
    {
        nso.pos.RemoveAt(index);
        if(objs.Count > 0)
        {
            DestroyImmediate(objs[index]);
            objs.RemoveAt(index);
        }
    }

    private void Create(int index){
        GameObject obj = Instantiate(prefab, nso.pos[index], Quaternion.identity);
        ShowNode com = obj.GetComponent<ShowNode>();
        com.Index = index;
        com.onMove = OnMove;
        objs.Add(obj);
    }

    private void InitAllNodes()
    {
        if(bInited){
            return ;
        }
        for (int i = 0; i < nso.pos.Count; i++)
        {
            Create(i);
        }
        bInited = true;
    }

    private void OnMove(int index, Vector3 pos){
        nso.pos[index] = pos;
    }
}
