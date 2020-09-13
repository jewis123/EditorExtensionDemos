using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShowNode))]
public class ShowNodEditor : Editor
{
    private void OnInspectorUpdate() {
        Debug.Log(22);
        GameObject obj = target as GameObject;
        ShowNode com = obj.GetComponent<ShowNode>(); 
        if(com.LastPos!=obj.transform.position){
            com.LastPos = obj.transform.position;
            com.OnMove(com.LastPos);
        }
    }
}