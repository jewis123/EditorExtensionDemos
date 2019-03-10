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
using System.Reflection;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(ButtonAttribute))]
	public class ButtonDrawer : IllogikaDrawer {
				
		ButtonAttribute buttonAttribute { get { return ((ButtonAttribute)attribute); } }
		
		bool ShowVariable(SerializedProperty prop)
		{
			bool showVariable = !buttonAttribute.hideVariable;				
			return showVariable;
		}
			
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			float baseHeight = base.GetPropertyHeight(prop, label);
			return ShowVariable(prop) ? baseHeight * 2 : baseHeight;
	    }

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			bool showVariable = ShowVariable(prop);
				
			if (showVariable)
				position.height /= 2;

			if(GUI.Button(EditorGUI.IndentedRect(position), buttonAttribute.buttonText))
			{
				foreach(Object obj in prop.serializedObject.targetObjects)  //搜索特性依附的所有的目标对象，检查是否有buttonFunction
				{
					MonoBehaviour go = obj as MonoBehaviour;
					if (go != null)
					{
						CallMethod(prop, go, buttonAttribute.buttonFunction);
					}
					else
					{
						ScriptableObject so = obj as ScriptableObject;
						if(so != null)
						{
							CallMethod(prop, so, buttonAttribute.buttonFunction);
						}
					}
				}
			}
				
			if (showVariable)
				position.y += position.height;
				
			if(showVariable)
			{
				EditorGUI.PropertyField(position, prop);	
			}

			EditorGUI.EndProperty();
		}
	}

}
