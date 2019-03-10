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
//      Copyright © 2013 - 2015  Illogika
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(LayerAttribute))]
	public class LayerDrawer : IllogikaDrawer {
			
		LayerAttribute layerAttribute { get { return ((LayerAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			if(prop.propertyType != SerializedPropertyType.Integer)
			{
				WrongVariableTypeWarning("Layer", "integers");
				return;
			}

			int originalIndentLevel = EditorGUI.indentLevel;

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID (FocusType.Passive), label);
			EditorGUI.indentLevel = 0;

			if(prop.hasMultipleDifferentValues)
			{
				EditorGUI.BeginChangeCheck();

				int temp = EditorGUI.LayerField(position, 3);

				if(EditorGUI.EndChangeCheck())
					prop.intValue = temp;
			}
			else
			{
				prop.intValue = EditorGUI.LayerField(position, prop.intValue);
			}

			EditorGUI.indentLevel = originalIndentLevel;
			EditorGUI.EndProperty();
		}
	}

}
