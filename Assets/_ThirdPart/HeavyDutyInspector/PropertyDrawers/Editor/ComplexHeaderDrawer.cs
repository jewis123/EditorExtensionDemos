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

	[CustomPropertyDrawer(typeof(ComplexHeaderAttribute))]
	public class ComplexHeaderDrawer : DecoratorDrawer {
			
		ComplexHeaderAttribute complexHeaderAttribute { get { return ((ComplexHeaderAttribute)attribute); } }
		
		public override float GetHeight ()
		{
	       return base.GetHeight();
	    }
		
		public override void OnGUI (Rect position)
		{
			int originalIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			Rect background = position;

			background.x = 0;
			background.width = Screen.width;

			GUIStyle headerStyle = GUI.skin.label;
			headerStyle.fontStyle = FontStyle.Bold;

			float textWidth = headerStyle.CalcSize(new GUIContent(complexHeaderAttribute.text)).x;

			Color temp = GUI.color;
			if(complexHeaderAttribute.style == Style.Box)
			{
				GUI.color = complexHeaderAttribute.backgroundColor;
				EditorGUI.HelpBox(background, "", MessageType.None);
			}
			else if(complexHeaderAttribute.style == Style.Line)
			{
				GUI.color = complexHeaderAttribute.backgroundColor;
				background.y += background.height / 2;
				background.height = 1;
				background.width /= 2;
				background.width = Mathf.Max(background.width - textWidth/2, 0);
				GUI.Box(background, "");
				background.x += textWidth + background.width + 5;
				GUI.Box(background, "");
			}

			GUI.color = complexHeaderAttribute.textColor;
			if(complexHeaderAttribute.textAlignment == Alignment.Left)
			{
				EditorGUI.LabelField(position, complexHeaderAttribute.text, headerStyle);
			}
			else if(complexHeaderAttribute.textAlignment == Alignment.Center)
			{
				position.x += Mathf.Max((position.width - textWidth)/2, 0);
				position.width = Mathf.Max(position.width, textWidth);
				EditorGUI.LabelField(position, complexHeaderAttribute.text, headerStyle);
			}
			else if(complexHeaderAttribute.textAlignment == Alignment.Right)
			{
				position.x += Mathf.Max(position.width - textWidth, 0);
				position.width = Mathf.Max(position.width, textWidth);
				EditorGUI.LabelField(position, complexHeaderAttribute.text, headerStyle);
			}

			GUI.color = temp;

			EditorGUI.indentLevel = originalIndentLevel;
		}
	}

}
