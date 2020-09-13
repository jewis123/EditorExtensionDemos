using System;
using UnityEngine;
using UnityEditor;

public class ShowNode : MonoBehaviour
{

    private int index;
    private Vector3 lastpos = Vector3.zero;
    public Action<int,Vector3> onMove = null;

    public int Index{get => index ; set => index = value;}
    public Vector3 LastPos{get => lastpos ; set => lastpos = value;}

    public void OnMove(Vector3 lastpos){
        Debug.Log(11);
        if (onMove != null) {onMove(Index, lastpos);}
        OnDrawGizmos();
    }

    private void OnDrawGizmos() {
        Vector3 pos= transform.position;
        Quaternion rotation = transform.rotation;
        Vector3 scale = transform.localScale;
        Handles.TransformHandle(ref pos, ref rotation, ref scale);
        transform.position = pos;
        transform.rotation = rotation;
        transform.localScale = scale;
    }
}
