using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer (typeof (TimeAttribute))]
public class TimeDrawer : PropertyDrawer {

  TimeAttribute timeProperty { get { return ((TimeAttribute) attribute); } }


  //设置属性高度
  public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
    return EditorGUI.GetPropertyHeight (property) * 2;
  }

  public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
    //获取属性的数据类型
    if (property.propertyType == SerializedPropertyType.Integer) {
      property.intValue = EditorGUI.IntField (new Rect (position.x, position.y, position.width, position.height / 2), label, Mathf.Abs (property.intValue));
      //在属性下创建Label
      EditorGUI.LabelField (new Rect (position.x, position.y + position.height / 2, position.width, position.height / 2), "换算：", TimeFormat (property.intValue));
    } else {
      //在Inspector上创建Label给出提示
      EditorGUI.LabelField (position, label.text, "Use Time with an int.");
    }
  }

  private string TimeFormat (int seconds) {
    TimeAttribute time = attribute as TimeAttribute;
    if (time.DisplayHours) {
      return string.Format ("{0}:{1}:{2} (h:m:s)", seconds / (60 * 60), ((seconds % (60 * 60)) / 60).ToString ().PadLeft (2, '0'), (seconds % 60).ToString ().PadLeft (2, '0'));
    } else {
      return string.Format ("{0}:{1} (m:s)", (seconds / 60).ToString (), (seconds % 60).ToString ().PadLeft (2, '0'));
    }
  }
}