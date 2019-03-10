/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

//----------------------------------------------
//            Heavy-Duty Inspector
//      Copyright © 2014 - 2015  Illogika
//----------------------------------------------
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(TagListAttribute))]
	public class TagListDrawer : IllogikaDrawer {
			
		TagListAttribute tagListAttribute { get { return ((TagListAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			if(prop.serializedObject.targetObjects.Length > 1)
			{
				if(int.Parse(prop.propertyPath.Split('[')[1].Split(']')[0]) != 0)
					return -2.0f;
				else
					return base.GetPropertyHeight(prop, label) * 2;
			}

	    	return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			int index = int.Parse(prop.propertyPath.Split('[')[1].Split(']')[0]);

			IList list = null;
			try
			{
				list = (prop.serializedObject.targetObject as MonoBehaviour).GetType().GetField(prop.propertyPath.Split('.')[0]).GetValue(prop.serializedObject.targetObject) as IList;
			}
			catch
			{
				try{
					list = (prop.serializedObject.targetObject as ScriptableObject).GetType().GetField(prop.propertyPath.Split('.')[0]).GetValue(prop.serializedObject.targetObject) as IList;
				}
				catch{
					Debug.LogWarning(string.Format("The script has no property named {0} or {0} is not an IList",prop.propertyPath.Split('.')[0]));
				}
			}

			if(prop.serializedObject.targetObjects.Length > 1)
			{
				if(index == 0)
				{
					position.height = base.GetPropertyHeight(prop, label) * 2;
					EditorGUI.indentLevel = 1;
					position = EditorGUI.IndentedRect(position);
					EditorGUI.HelpBox(position, "Multi object editing is not supported.", MessageType.Warning);
				}
				return;
			}

			int originalIndentLevel = EditorGUI.indentLevel;

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID (FocusType.Passive), label);
			EditorGUI.indentLevel = 0;

			if(tagListAttribute.canDeleteFirstElement || index != 0)
				position.width -= 18;

			if(prop.stringValue == "")
				prop.stringValue = "Untagged";

			prop.stringValue = EditorGUI.TagField(position, prop.stringValue);

			position.x += position.width;
			position.width = 16;

			if((tagListAttribute.canDeleteFirstElement || index != 0) && GUI.Button(position, "", "OL Minus"))
			{
				list.RemoveAt(index);
			}


			EditorGUI.indentLevel = originalIndentLevel;
			EditorGUI.EndProperty();
		}
	}

}
