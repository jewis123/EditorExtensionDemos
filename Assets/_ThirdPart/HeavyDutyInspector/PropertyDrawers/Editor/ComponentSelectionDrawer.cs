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
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(ComponentSelectionAttribute))]
	public class ComponentSelectionDrawer : IllogikaDrawer {

		ComponentSelectionAttribute componentSelectionAttribute { get { return ((ComponentSelectionAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label) * 2;
	    }

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			OnComponentGUI(position, prop, label, componentSelectionAttribute.fieldName, componentSelectionAttribute.requiredValues, componentSelectionAttribute.defaultObject, componentSelectionAttribute.isPrefab, 0);

			EditorGUI.EndProperty();
		}
	}

}
