using UnityEditor;
using UnityEngine;

//给Ingredient指定PropertyDrawer
[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer:PropertyDrawer{
    //在给定的Rect区域中绘制属性
    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label){
        
        EditorGUI.BeginProperty(position,label,property);  

        //绘制标签
        position = EditorGUI.PrefixLabel(position,GUIUtility.GetControlID(FocusType.Passive),label);
        
        //Unity默认的每个属性字段都会占用一行，我们这里希望一条自定义Property占一行
        //要是实现这个要求我们分三步： 1. 取消缩进  2. 设置PropertyField 3.还原缩进
        
        //不要缩进子字段，只有取消了缩进Rect才不会混乱
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //计算要用到的属性显示rect   Rect(x,y,width,height)x,y是左顶点
        var amountRect = new Rect(position.x,position.y,30,position.height);
        var unitRect = new Rect(position.x + 35,position.y,50,position.height);
        var nameRect = new Rect(position.x + 90,position.y,position.width  -  90,position.height);

        //绘制字段 - 将GUIContent.none传递给每个字段，以便绘制它们而不是用标签
        //property.FindPropertyRelative(string)  用来查找传入特性的特定属性
        EditorGUI.PropertyField(amountRect,property.FindPropertyRelative("amount"),GUIContent.none);
        EditorGUI.PropertyField(unitRect,property.FindPropertyRelative("unit"),GUIContent.none);
        EditorGUI.PropertyField(nameRect,property.FindPropertyRelative("name"),GUIContent.none);


        //将缩进还原，好习惯
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
