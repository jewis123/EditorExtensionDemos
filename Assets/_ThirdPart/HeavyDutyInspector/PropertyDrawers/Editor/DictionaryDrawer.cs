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
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(DictionaryAttribute))]
	public class DictionaryDrawer : IllogikaDrawer {
			
		DictionaryAttribute dictionaryAttribute { get { return ((DictionaryAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			return base.GetPropertyHeight(prop, label) * (int.Parse(prop.propertyPath.Split(']')[prop.propertyPath.Split(']').Length-2].Split('[').Last()) == 0 ? 2 : 1);
	    }

		private static Texture2D olMinus
		{
			get;
			set;
		}

		private List<string> keywords;

		protected void PopulateList()
		{
			keywords = new List<string>();

			foreach(KeywordCategory category in dictionaryAttribute.keywordConfig.keyWordCategories)
			{
				foreach(string keyword in category.keywords)
				{
					if(!string.IsNullOrEmpty(keyword))
						keywords.Add(category.name + (string.IsNullOrEmpty(category.name) ? "" : "/") + keyword);
				}
			}

			keywords.Sort();
			keywords.Insert(0, "Empty String");
		}

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			if(olMinus == null)
				olMinus = (Texture2D)Resources.Load("OLMinusRed");

			if(keywords == null && dictionaryAttribute.keywordConfig != null)
				PopulateList();

			EditorGUI.BeginProperty(position, label, prop);

			int index = int.Parse(prop.propertyPath.Split(']')[prop.propertyPath.Split(']').Length-2].Split('[').Last());

			if(!fieldInfo.FieldType.IsGenericType || fieldInfo.FieldType.IsArray)
			{
				Debug.LogWarning("The Dictionary Attribute can only be used with Lists.");
			}
			
			IList list1 = null;

			list1 = GetReflectedFieldRecursively<IList>(prop);

			if(list1 == null)
				return;

			IList list2 = null;

			SerializedProperty serializedValues = prop.serializedObject.FindProperty(dictionaryAttribute.valuesListName);
			list2 = GetReflectedFieldRecursively<IList>(serializedValues);
			
			if(list2 == null)
				return;

			System.Type type1 = list1.GetType().GetGenericArguments()[0];

			System.Type type2 = list2.GetType().GetGenericArguments()[0];

			while(list2.Count < list1.Count)
			{
				if(!type2.IsSubclassOf(typeof(Component)) && !typeof(GameObject).IsAssignableFrom(type2) && type2.GetConstructor(Type.EmptyTypes) != null)
				{
					list2.Add(System.Activator.CreateInstance(type2));
				}
				else if(type2.IsPrimitive)
				{
					if(type2 == typeof(bool))
						list2.Add(false);
					else if(type2 == typeof(char))
						list2.Add('\0');
					else
						list2.Add(0);
				}
				else if(type2.IsClass)
				{
					list2.Add(null);
				}
			}

			while(list2.Count > list1.Count)
			{
				list2.RemoveAt(list2.Count - 1);
			}

			position.width -= 16;
			position.width /= 2;

			if(index >= serializedValues.arraySize)
				return;

			if(index == 0)
			{
				position.height /= 2;
				Rect labelPos = position;
				position.y += position.height;
				EditorGUI.LabelField(labelPos, "Key");
				labelPos.x += labelPos.width;
				EditorGUI.LabelField(labelPos, "Value");
			}

			Color originalColor = GUI.color;

			if(typeof(Keyword).IsAssignableFrom(type1) && dictionaryAttribute.keywordConfig != null)
			{
				string temp = prop.FindPropertyRelative("_key").stringValue;

				if(temp == "")
					temp = "Empty String";

				int i = keywords.IndexOf(temp);

				if(i < 0)
				{
					i = keywords.Count;
					keywords.Add(temp + " (Missing)");
					GUI.color = Color.yellow;
				}

				if(!IsUniqueKey(list1, list1[index] as Keyword, index))
					GUI.color = Color.red;

				EditorGUI.BeginChangeCheck();

				i = EditorGUI.Popup(position, i, keywords.ToArray());

				temp = keywords[i];

				if(temp == "Empty String")
					temp = "";

				if(EditorGUI.EndChangeCheck())
				{
					prop.FindPropertyRelative("_key").stringValue = temp;
				}
			}
			else
			{
				if(typeof(string).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<string>(list1, (string)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(char).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<char>(list1, (char)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(short).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<short>(list1, (short)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(int).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<int>(list1, (int)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(long).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<long>(list1, (long)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(float).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<float>(list1, (float)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(double).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<double>(list1, (double)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(ushort).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<ushort>(list1, (ushort)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(uint).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<uint>(list1, (uint)list1[index], index))
						GUI.color = Color.red;
				}
				else if(typeof(ulong).IsAssignableFrom(type1))
				{
					if(!IsUniqueKey<ulong>(list1, (ulong)list1[index], index))
						GUI.color = Color.red;
				}
				else
				{
					if(!IsUniqueKey(list1, list1[index], index))
						GUI.color = Color.red;
				}

				EditorGUI.PropertyField(position, prop, new GUIContent(""));
			}

			GUI.color = originalColor;

			position.x += position.width;
			EditorGUI.PropertyField(position, serializedValues.GetArrayElementAtIndex(index), new GUIContent(""));

			position.x += position.width + 4;
			position.width = 16;

			if(GUI.Button(position, olMinus, "Label"))
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Remove Item In Dictionary");

				list1.RemoveAt(index);
				list2.RemoveAt(index);
			}
			
			EditorGUI.EndProperty();
		}

		private bool IsUniqueKey(IList list, System.Object current, int index)
		{
			for(int i = 0; i < list.Count; ++i)
			{
				if(i == index)
					continue;

				if(list[i] == current)
					return false;
			}
			return true;
		}

		private bool IsUniqueKey<T>(IList list, T current, int index)
		{
			for(int i = 0; i < list.Count; ++i)
			{
				if(i == index)
					continue;

				if(((T)list[i]).Equals(current))
					return false;
			}
			return true;
		}

		private bool IsUniqueKey(IList list, Keyword current, int index)
		{
			for(int i = 0; i < list.Count; ++i)
			{
				if(i == index)
					continue;

				if((string)((Keyword)list[i]) == current)
					return false;
			}
			return true;
		}
	}
}
