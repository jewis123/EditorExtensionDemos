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
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(EnumMaskAttribute))]
	public class EnumMaskDrawer : IllogikaDrawer {
			
		EnumMaskAttribute enumMaskAttribute { get { return ((EnumMaskAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			position = EditorGUI.PrefixLabel(position, EditorGUIUtility.GetControlID(FocusType.Passive), label);

			int originalIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			System.Enum propEnum = GetReflectedFieldRecursively<System.Enum>(prop);

			if (propEnum == null)
				return;

			EditorGUI.BeginChangeCheck();

			propEnum = EditorGUI.EnumFlagsField(position, propEnum);

			if(EditorGUI.EndChangeCheck())
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, propEnum);

				EditorUtility.SetDirty(prop.serializedObject.targetObject);
			}

			EditorGUI.indentLevel = originalIndentLevel;

			EditorGUI.EndProperty();
		}
	}

}
