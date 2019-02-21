using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelAttribute),false)]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.text = (attribute as LabelAttribute).label;
        EditorGUI.PropertyField(position, property, label);
    }
}