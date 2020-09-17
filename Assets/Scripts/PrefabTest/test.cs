using UnityEngine;
using UnityEditor;

public class test : EditorWindow
{
    private Object prefab_give;
    private GameObject parent;

    [MenuItem("CustomWindow/Prefab Test")]
    private static void ShowWindow()
    {
        var window = GetWindow<test>();
        window.titleContent = new GUIContent("test");
        window.Show();
    }

    private void OnGUI()
    {
        prefab_give = EditorGUILayout.ObjectField("预置", prefab_give, typeof(GameObject), false);
        parent = (GameObject)EditorGUILayout.ObjectField("父节点", parent, typeof(GameObject), true);

        if (GUILayout.Button("点击创建"))
        {
            if (prefab_give && parent)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Prefabs/Cube.prefab");
                GameObject new_obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                GameObject instantiateObj = (GameObject)Instantiate(prefab_give);

                new_obj.transform.SetParent(parent.transform);
                instantiateObj.transform.SetParent(parent.transform);
            }
        }
    }
}
