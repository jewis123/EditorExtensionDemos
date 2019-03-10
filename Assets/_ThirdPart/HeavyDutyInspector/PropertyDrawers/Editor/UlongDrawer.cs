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
//         Copyright © 2014  Illogika
//----------------------------------------------
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(UInt64))]
	public class UlongDrawerDrawer : IllogikaDrawer {
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			UInt64 temp = BitConverter.ToUInt64(BitConverter.GetBytes(prop.longValue), 0);

			bool hasChanged = false;
			EditorGUI.BeginChangeCheck();

			string s = "";

			if(prop.hasMultipleDifferentValues)
			{
				s = "--";
			}
			else
			{
				s = ((UInt64)temp).ToString();
			}

			s = EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			if(EditorGUI.EndChangeCheck())
			{
				try
				{
					temp = UInt64.Parse(s) + (UInt64)tempInt;
					hasChanged = true;
				}
				catch
				{
					if(string.IsNullOrEmpty(s))
					{
						temp = (UInt64)tempInt;
						hasChanged = true;
					}

					// field had an invalid value. Ignore change this frame
				}
			}

			if(hasChanged)
			{
				prop.longValue = BitConverter.ToInt64(BitConverter.GetBytes(temp), 0);
			}

			EditorGUI.EndProperty();
		}
	}

}
