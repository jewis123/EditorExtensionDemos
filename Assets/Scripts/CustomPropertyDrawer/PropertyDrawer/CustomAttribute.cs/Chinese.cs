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
        [LabelAttribute ("中文属性名")] //修改的Property的名字，即原来Inspector中显示的“TestInt”变成了“中文属性名”
        public int testInt;

        [Header("中文")]      //内置的Header标签只是在Property上方添加了label，注意和上面的区别
        public string Name;

        
}