using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(RangeAttribute))]
public class RangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 首先获取属性、因为其包含范围
        RangeAttribute range = attribute as RangeAttribute;

        // 将属性绘制成Slider.
        if (property.propertyType == SerializedPropertyType.Float)
            EditorGUI.Slider(position, property, range.min, range.max, label);
        else if (property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.IntSlider(position, property, Convert.ToInt32(range.min), Convert.ToInt32(range.max), label);
        else
            EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
    }
}