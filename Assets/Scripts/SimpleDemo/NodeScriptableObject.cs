using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AllNodes", menuName = "NodesScriptObject")]
public class NodeScriptableObject : ScriptableObject
{
    public List<Vector3> pos = new List<Vector3>();
    
}
