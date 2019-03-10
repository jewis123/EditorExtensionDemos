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
#if !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && NEW_VECTOR_DRAWER

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(Vector3))]
	public class Vector3Drawer : IllogikaDrawer {
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{;
			EditorGUI.BeginProperty(position, label, prop);

			position = EditorGUI.PrefixLabel(position, EditorGUIUtility.GetControlID(FocusType.Passive), label);

			int originalIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			Rect labelPos = position;

			labelPos.width = 12;

			position.width /= 3;
			position.width -= 12;
			position.x += 12;

			Vector3 temp = prop.vector3Value;
			EditorGUI.BeginChangeCheck();

			GUIContent xLabel = new GUIContent("X");
			GUIContent yLabel = new GUIContent("Y");
			GUIContent zLabel = new GUIContent("Z");

			EditorGUI.PrefixLabel(labelPos, xLabel);
			labelPos.x += position.width + 12;

			GUI.backgroundColor = Color.red;
			temp.x = EditorGUI.FloatField(position, temp.x);
			GUI.backgroundColor = Color.white;

			EditorGUI.PrefixLabel(labelPos, yLabel);
			labelPos.x += position.width + 12;

			position.x += position.width + 12;

			GUI.backgroundColor = Color.green;
			temp.y = EditorGUI.FloatField(position, temp.y);
			GUI.backgroundColor = Color.white;

			EditorGUI.PrefixLabel(labelPos, zLabel);
			labelPos.x += position.width + 12;

			position.x += position.width + 12;

			GUI.backgroundColor = Color.blue;
			temp.z = EditorGUI.FloatField(position, temp.z);
			GUI.backgroundColor = Color.white;

			if(EditorGUI.EndChangeCheck())
			{
				prop.vector3Value = temp;
			}

			EditorGUI.indentLevel = originalIndentLevel;

			EditorGUI.EndProperty();
		}
	}

}

#endif
