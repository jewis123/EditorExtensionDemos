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
using System.Linq;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HeavyDutyInspector
{

	public class HideConditionalAttribute : PropertyAttribute {

		public enum ConditionType
		{
			IsNotNull,
			Bool,
			IntOrEnum,
			FloatRange
		}

		public ConditionType conditionType
		{
			get;
			private set;
		}

		public string variableName
		{
			get;
			private set;
		}

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

		public bool boolValue
		{
			get;
			private set;
		}

		public List<int> enumValues
		{
			get;
			private set;
		}

		public float minValue
		{
			get;
			private set;
		}

		public float maxValue
		{
			get;
			private set;
		}

		public bool isNotNull
		{
			get;
			private set;
		}

		/// <summary>
		/// Hides this variable until the value of another variable is not null.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated.</param>
		public HideConditionalAttribute(string conditionVariableName)
		{
			conditionType = ConditionType.IsNotNull;
			variableName = conditionVariableName;
		}

		/// <summary>
		/// Hides this variable until a condition is met.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated.</param>
		/// <param name="visibleState">The state the condition variable has to be in for this variable to be shown in the Inspector.</param>
		public HideConditionalAttribute(string conditionVariableName, bool visibleState)
		{
			conditionType = ConditionType.Bool;
			variableName = conditionVariableName;
			boolValue = visibleState;
		}

		/// <summary>
		/// Hides this variable until a condition is met.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated. Can be an int or an enum.</param>
		/// <param name="visibleStates">The states the condition variable can be in for this variable to be shown in the Inspector. This can also be used for Enums with an underlying integer type.</param>
		public HideConditionalAttribute(string conditionVariableName, params int[] visibleState)
		{
			conditionType = ConditionType.IntOrEnum;
			variableName = conditionVariableName;
			enumValues = new List<int>();
			enumValues = visibleState.ToList();
		}

		/// <summary>
		/// Hides this variable until a condition is met.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated.</param>
		/// <param name="minValue">The minimum value the condition variable can contain for this variable to be shown in the Inspector. Inclusive.</param>
		/// <param name="maxValue">The maximum value this variable can contain for this variable to be shown in the Inspector. Inclusive.</param>
		public HideConditionalAttribute(string conditionVariableName, float minValue, float maxValue)
		{
			conditionType = ConditionType.FloatRange;
			variableName = conditionVariableName;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		/// <summary>
		/// Hides this variable until the value of another variable is not null. Also displays a Comment over the variable if it is visible.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated.</param>
		/// <param name='comment'>The comment to display.</param>
		/// <param name='messageType'>The icon to be displayed next to the comment, if any.</param>
		public HideConditionalAttribute(string conditionVariableName, string comment, CommentType messageType)
		{
			conditionType = ConditionType.IsNotNull;
			variableName = conditionVariableName;
			this.comment = comment;
			SetMessageType(messageType);
		}

		/// <summary>
		/// Hides this variable until a condition is met. Also displays a Comment over the variable if it is visible.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated.</param>
		/// <param name='comment'>The comment to display.</param>
		/// <param name='messageType'>The icon to be displayed next to the comment, if any.</param>
		/// <param name="visibleState">The state the condition variable has to be in for this variable to be shown in the Inspector.</param>
		public HideConditionalAttribute(string conditionVariableName, string comment, CommentType messageType, bool visibleState)
		{
			conditionType = ConditionType.Bool;
			variableName = conditionVariableName;
			this.comment = comment;
			SetMessageType(messageType);
			boolValue = visibleState;
		}

		/// <summary>
		/// Hides this variable until a condition is met. Also displays a Comment over the variable if it is visible.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated. Can be an int or an enum.</param>
		/// <param name='comment'>The comment to display.</param>
		/// <param name='messageType'>The icon to be displayed next to the comment, if any.</param>
		/// <param name="visibleStates">The states the condition variable can be in for this variable to be shown in the Inspector. This can also be used for Enums with an underlying integer type.</param>
		public HideConditionalAttribute(string conditionVariableName, string comment, CommentType messageType, params int[] visibleState)
		{
			conditionType = ConditionType.IntOrEnum;
			variableName = conditionVariableName;
			this.comment = comment;
			SetMessageType(messageType);
			enumValues = new List<int>();
			enumValues = visibleState.ToList();
		}

		/// <summary>
		/// Hides this variable until a condition is met. Also displays a Comment over the variable if it is visible.
		/// </summary>
		/// <param name="conditionVariableName">The name of the variable whose value will be evaluated.</param>
		/// <param name='comment'>The comment to display.</param>
		/// <param name='messageType'>The icon to be displayed next to the comment, if any.</param>
		/// <param name="minValue">The minimum value the condition variable can contain for this variable to be shown in the Inspector. Inclusive.</param>
		/// <param name="maxValue">The maximum value this variable can contain for this variable to be shown in the Inspector. Inclusive.</param>
		public HideConditionalAttribute(string conditionVariableName, string comment, CommentType messageType, float minValue, float maxValue)
		{
			conditionType = ConditionType.FloatRange;
			variableName = conditionVariableName;
			this.comment = comment;
			SetMessageType(messageType);
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		private void SetMessageType(CommentType commentType)
		{
#if UNITY_EDITOR
			switch(commentType)
			{
				case CommentType.Error:
					messageType = MessageType.Error;
					break;
				case CommentType.Info:
					messageType = MessageType.Info;
					break;
				case CommentType.None:
					messageType = MessageType.None;
					break;
				case CommentType.Warning:
					messageType = MessageType.Warning;
					break;
				default:
					break;
			}
#endif
		}
	}

}
