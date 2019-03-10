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
using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(ReorderableListAttribute))]
	public class ReorderableListDrawer : IllogikaDrawer {

		ReorderableListAttribute reorderableListAttribute { get { return ((ReorderableListAttribute)attribute); } }

		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			if(prop.serializedObject.targetObjects.Length > 1)
			{
				if(int.Parse(prop.propertyPath.Split('[')[1].Split(']')[0]) != 0)
					return -2.0f;
				else
					return base.GetPropertyHeight(prop, label) * 2;
			}

			if(prop.hasChildren && prop.isExpanded)
			{
				return EditorGUI.GetPropertyHeight(prop, label, true);
			}
			else
			{
				if(fieldInfo.FieldType.GetGenericArguments()[0].IsSubclassOf(typeof(Component)) && !reorderableListAttribute.useDefaultComponentDrawer)
				{
					return (base.GetPropertyHeight(prop, label) + 1) * 2;
				}
				else
				{
					return base.GetPropertyHeight(prop, label);
				}
			}
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			int index = int.Parse(prop.propertyPath.Split(']')[prop.propertyPath.Split(']').Length-2].Split('[').Last());

			if(!fieldInfo.FieldType.IsGenericType || fieldInfo.FieldType.IsArray)
			{
				Debug.LogWarning("The Reorderable List Attribute can only be used with Lists.");
			}

			IList list = null;

			list = GetReflectedFieldRecursively<IList>(prop);

			if(list == null)
				return;

			if(prop.serializedObject.targetObjects.Length > 1)
			{
				if(index == 0)
				{
					position.height = base.GetPropertyHeight(prop, label) * 2;
					EditorGUI.indentLevel = 1;
					position = EditorGUI.IndentedRect(position);
					EditorGUI.HelpBox(position, "Multi object editing is not supported.", MessageType.Warning);
				}
				return;
			}

			Rect basePosition = position;

			position.width -= 84;
			position.height = 16;

			if(prop.hasChildren && prop.isExpanded)
			{
				PropertyFieldIncludingSpecialAndFoldouts(prop, position, basePosition, label);
			}
			else if(fieldInfo.FieldType.GetGenericArguments()[0].IsSubclassOf(typeof(Component)) && !reorderableListAttribute.useDefaultComponentDrawer)
			{
				OnComponentGUI(basePosition, prop, label, "", null, "", false, 82);
			}
			else
			{
				EditorGUI.PropertyField(position, prop);
			}

			basePosition.x += basePosition.width - 82;
			basePosition.width = 25;
			basePosition.height = 16;

			if(index != 0)
			{
				if(GUI.Button(basePosition, reorderableListAttribute.arrowUp, "ButtonLeft"))
				{
					Undo.RecordObjects(prop.serializedObject.targetObjects, "Move Item Up In List");

					list.Insert(index - 1, list[index]);
					list.RemoveAt(index + 1);

					foreach(Object obj in prop.serializedObject.targetObjects)
					{
						EditorUtility.SetDirty(obj);
					}
				}
			}
			else
			{
				Color temp = GUI.color;
				GUI.color = Color.gray;
				GUI.Box(basePosition, reorderableListAttribute.arrowUp, "ButtonLeft");
				GUI.color = temp;
			}

			basePosition.x += 25;

			if(index != list.Count - 1)
			{
				if(GUI.Button(basePosition, reorderableListAttribute.arrowDown, "ButtonRight") && index != list.Count - 1)
				{
					Undo.RecordObjects(prop.serializedObject.targetObjects, "Move Item Down In List");

					list.Insert(index + 2, list[index]);
					list.RemoveAt(index);

					foreach(Object obj in prop.serializedObject.targetObjects)
					{
						EditorUtility.SetDirty(obj);
					}
				}
			}
			else
			{
				Color temp = GUI.color;
				GUI.color = Color.gray;
				GUI.Box(basePosition, reorderableListAttribute.arrowDown, "ButtonRight");
				GUI.color = temp;
			}

			basePosition.x += 26;
            basePosition.y += 2;
			basePosition.width = 16;

            if (GUI.Button(basePosition, reorderableListAttribute.olPlus, "Label"))
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Add Item In List");

				if(fieldInfo.FieldType.GetGenericArguments()[0].IsClass && !fieldInfo.FieldType.GetGenericArguments()[0].IsSubclassOf(typeof(MonoBehaviour)) && fieldInfo.FieldType.GetGenericArguments()[0].GetConstructor(new Type[0]) != null)
				{
					list.Insert(index + 1, System.Activator.CreateInstance(fieldInfo.FieldType.GetGenericArguments()[0]));
					foreach(FieldInfo field in fieldInfo.FieldType.GetGenericArguments()[0].GetFields())
					{
						field.SetValue(list[index + 1], field.GetValue(list[index]));
					}
				}
				else
					list.Insert(index + 1, list[index]);

				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			basePosition.x += 16;

            if (GUI.Button(basePosition, reorderableListAttribute.olMinus, "Label"))
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Remove Item In List");

				list.RemoveAt(index);

				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}
	}

}
