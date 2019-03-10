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

	[CustomPropertyDrawer(typeof(NMBColorAttribute))]
	public class NMBColorDrawer : PropertyDrawer {
			
		NMBColorAttribute nmbColorAttribute { get { return ((NMBColorAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return 0;
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			position.y -= base.GetPropertyHeight(prop, label);
			position.y -= 2;
			position.height = base.GetPropertyHeight(prop, label);

			position.x += position.width;
			position.x -= 50;

			position.width = 50;

			if(prop.hasMultipleDifferentValues)
			{
				EditorGUI.BeginChangeCheck();

				Color temp = EditorGUI.ColorField(position, Color.white);
				Color tempGuiColor = GUI.color;
				GUI.color = Color.grey;
				GUI.Label(position, "-");
				GUI.color = tempGuiColor;

				if(EditorGUI.EndChangeCheck())
					prop.colorValue = temp;
			}
			else
			{
				prop.colorValue = EditorGUI.ColorField(position, prop.colorValue);
			}

			EditorGUI.EndProperty();
		}
	}

}
