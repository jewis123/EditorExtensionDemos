using UnityEditor;
using UnityEngine;
public class LabelAttribute : PropertyAttribute
{
    public string label;
    public LabelAttribute(string label)
    {
        this.label = label;
    }
}
public class Chinese : MonoBehaviour {
        [LabelAttribute ("中文属性名")]
        public int testInt;
}