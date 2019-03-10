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
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace HeavyDutyInspector
{

	public enum CommentType { Error,
							  Info,
							  None,
							  Warning }

	[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class CommentAttribute : PropertyAttribute {
		
		public string comment
		{
			get;
			private set;
		}

	#if UNITY_EDITOR	
		public MessageType messageType
		{
			get;
			private set;
		}
	#endif

		/// <summary>
		/// Adds a comment before this variable.
		/// </summary>
		/// <param name='comment'>The comment to display.</param>
		/// <param name='messageType'>The icon to be displayed next to the comment, if any.</param>
		public CommentAttribute(string comment, CommentType messageType = CommentType.None, int order = 0)
		{
			this.comment = comment;
			this.order = order;

	#if UNITY_EDITOR
			switch(messageType)
			{
			case CommentType.Error:
				this.messageType = MessageType.Error;
				break;
			case CommentType.Info:
				this.messageType = MessageType.Info;
				break;
			case CommentType.None:
				this.messageType = MessageType.None;
				break;
			case CommentType.Warning:
				this.messageType = MessageType.Warning;
				break;
			default:
				break;
			}
	#endif
		}
	}

}
