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
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HeavyDutyInspector {

	[CustomPropertyDrawer (typeof (BackgroundAttribute))]
	public class BackgroundDrawer : DecoratorDrawer {

		BackgroundAttribute backgroundAttribute { get { return ((BackgroundAttribute) attribute); } }

		public override float GetHeight () {
			return 0;
		}

		public override void OnGUI (Rect position) {
			int originalIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			position.x = 0;
			position.width = Screen.width;
			position.height = base.GetHeight ();

			Color temp = GUI.color;

			GUI.color = backgroundAttribute.color;
			EditorGUI.HelpBox (position, "", MessageType.None);

			GUI.color = temp;

			EditorGUI.indentLevel = originalIndentLevel;
		}
	}

}