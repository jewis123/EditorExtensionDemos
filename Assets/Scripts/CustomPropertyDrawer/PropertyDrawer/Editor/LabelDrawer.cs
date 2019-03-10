using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelAttribute),false)]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.text = (attribute as LabelAttribute).label;  //PropertyDrawer 持有一个attribute getter
        EditorGUI.PropertyField(position, property, label);  //用属性替代原先的label
    }
}