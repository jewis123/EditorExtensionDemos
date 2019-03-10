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
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(HideConditionalAttribute))]
	public class HideConditionalDrawer : IllogikaDrawer {
			
		HideConditionalAttribute hideConditionalAttribute { get { return ((HideConditionalAttribute)attribute); } }

		public bool isVisible(SerializedProperty prop)
		{
			switch(hideConditionalAttribute.conditionType)
			{
			case HideConditionalAttribute.ConditionType.IsNotNull:
				return GetReflectedFieldRecursively<System.Object>(prop, hideConditionalAttribute.variableName) != null;
			case HideConditionalAttribute.ConditionType.IntOrEnum:
				return hideConditionalAttribute.enumValues.Contains(GetReflectedFieldRecursively<int>(prop, hideConditionalAttribute.variableName));
			case HideConditionalAttribute.ConditionType.FloatRange:
				if(hideConditionalAttribute.minValue < hideConditionalAttribute.maxValue)
				{
					Debug.LogError("Min value has to be lower than max value");
					return false;
				}
				else
				{
					return GetReflectedFieldRecursively<float>(prop, hideConditionalAttribute.variableName) >= hideConditionalAttribute.minValue && GetReflectedFieldRecursively<float>(prop, hideConditionalAttribute.variableName) <= hideConditionalAttribute.maxValue;
				}
			case HideConditionalAttribute.ConditionType.Bool:
				return GetReflectedFieldRecursively<bool>(prop, hideConditionalAttribute.variableName) == hideConditionalAttribute.boolValue;
			default:
				break;
			}
			return false;
		}

		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			if(isVisible(prop))
			{
				if(string.IsNullOrEmpty(hideConditionalAttribute.comment))
					return EditorGUI.GetPropertyHeight(prop, label, true);
				else
					return GetCommentHeight(hideConditionalAttribute.comment, hideConditionalAttribute.messageType) + EditorGUI.GetPropertyHeight(prop, label, true);
			}

			return -2.0f;
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			if(isVisible(prop))
			{
				if(!string.IsNullOrEmpty(hideConditionalAttribute.comment))
				{
					position.height = GetCommentHeight(hideConditionalAttribute.comment, hideConditionalAttribute.messageType);

					EditorGUI.HelpBox(position, hideConditionalAttribute.comment, hideConditionalAttribute.messageType);

					position.y += position.height + 2f;
				}

				position.height = base.GetPropertyHeight(prop, label);

				PropertyFieldIncludingSpecialAndFoldouts(prop, position, position, label);
			}

			EditorGUI.EndProperty();
		}
	}
}
