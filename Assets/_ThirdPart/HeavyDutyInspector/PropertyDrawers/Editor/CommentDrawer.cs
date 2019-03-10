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

namespace HeavyDutyInspector
{
	[CustomPropertyDrawer(typeof(CommentAttribute))]
	public class CommentDrawer : DecoratorDrawer {

		CommentAttribute commentAttribute { get { return ((CommentAttribute)attribute); } }

		public override float GetHeight()
		{
			return GetCommentHeight();
		}

		public float GetCommentHeight()
		{
			GUIStyle style = "HelpBox";
			return Mathf.Max(style.CalcHeight(new GUIContent(commentAttribute.comment), Screen.width - (commentAttribute.messageType != MessageType.None ? 53 : 35) ), EditorGUIUtility.singleLineHeight);
		}

		public override void OnGUI (Rect position)
		{
			Rect commentPosition = EditorGUI.IndentedRect (position);

			commentPosition.height = GetCommentHeight();
	
			DrawComment(commentPosition, commentAttribute.comment);
		}
		
		private void DrawComment(Rect position, string comment)
		{
			EditorGUI.HelpBox(position, comment, commentAttribute.messageType);
		}
	}

}
