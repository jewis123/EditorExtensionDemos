using UnityEngine;
using UnityEditor;

public class test : MonoBehaviour
{
    public GameObject prefab;
    private void OnEnable()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Prefabs/Cube.prefab");
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        GameObject ins = Instantiate(prefab);
    }
}
