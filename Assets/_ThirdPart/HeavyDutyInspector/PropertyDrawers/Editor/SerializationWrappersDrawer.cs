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
using Object = UnityEngine.Object;

namespace HeavyDutyInspector
{
#pragma warning disable 618
	[CustomPropertyDrawer(typeof(charS))] 
	public class CharSDrawer : IllogikaDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			charS temp = GetReflectedFieldRecursively<charS>(prop);

			if(temp == null)
				return;

			string val = temp;

			EditorGUI.BeginChangeCheck();



			val = EditorGUI.TextField(position, label, val);

			if(EditorGUI.EndChangeCheck())
			{
				temp = val.FirstOrDefault();

				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, temp);
				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}
	}

	[CustomPropertyDrawer(typeof(Int16S))]
	public class Int16SDrawer : IllogikaDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			Int16S temp = GetReflectedFieldRecursively<Int16S>(prop);

			if(temp == null)
				return;

			EditorGUI.BeginChangeCheck();

			int val = (int)temp;

			val = EditorGUI.IntField(position, label, val);

			if(EditorGUI.EndChangeCheck())
			{
				temp = (Int16)Mathf.Clamp(val, Int16.MinValue, Int16.MaxValue);

				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, temp);
				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}
	}


	[CustomPropertyDrawer(typeof(Int64S))]
	public class Int64SDrawer : IllogikaDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			Int64S temp = GetReflectedFieldRecursively<Int64S>(prop);

			if(temp == null)
				return;

			bool hasChanged = false;
			EditorGUI.BeginChangeCheck();

			string s = "";

			if(prop.hasMultipleDifferentValues)
			{
				s = "--";
			}
			else
			{
				s = ((Int64)temp).ToString();
			}

			s = EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			if(EditorGUI.EndChangeCheck())
			{
				try
				{
					temp = Int64.Parse(s) + (Int64)tempInt;
					hasChanged = true;
				}
				catch
				{
					if(string.IsNullOrEmpty(s))
					{
						temp = (Int64)tempInt;
						hasChanged = true;
					}
				}
			}

			if(hasChanged)
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, temp);
				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}

	}


	[CustomPropertyDrawer(typeof(UInt16S))]
	public class UInt16SDrawer : IllogikaDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			UInt16S temp = GetReflectedFieldRecursively<UInt16S>(prop);

			if(temp == null)
				return;

			EditorGUI.BeginChangeCheck();

			int val = (int)temp;

			val = EditorGUI.IntField(position, label, val);

			if(EditorGUI.EndChangeCheck())
			{
				temp = (UInt16)Mathf.Clamp(val, UInt16.MinValue, UInt16.MaxValue);

				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, temp);
				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}
	}


	[CustomPropertyDrawer(typeof(UInt32S))]
	public class UInt32SDrawer : IllogikaDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			UInt32S temp = GetReflectedFieldRecursively<UInt32S>(prop);

			if(temp == null)
				return;

			bool hasChanged = false;
			EditorGUI.BeginChangeCheck();

			string s = "";

			if(prop.hasMultipleDifferentValues)
			{
				s = "--";
			}
			else
			{
				s = ((UInt32)temp).ToString();
			}

			s = EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			if(EditorGUI.EndChangeCheck())
			{
				try
				{
					temp = UInt32.Parse(s) + (UInt32)tempInt;
					hasChanged = true;
				}
				catch
				{
					// Field value may be bigger or smaller than boundary values.
					try
					{
						UInt64 tempLong = UInt64.Parse(s);

						if(tempLong > UInt32.MaxValue)
							temp = UInt32.MaxValue;

						if(tempLong < UInt32.MinValue)
							temp = UInt32.MinValue;

						hasChanged = true;
					}
					catch
					{
						if(string.IsNullOrEmpty(s))
						{
							temp = (UInt32)tempInt;
							hasChanged = true;
						}

						// field had an invalid value. Ignore change this frame
					}
				}
			}

			if(hasChanged)
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, temp);
				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}
	}

	[CustomPropertyDrawer(typeof(UInt64S))]
	public class UInt64SDrawer : IllogikaDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			UInt64S temp = GetReflectedFieldRecursively<UInt64S>(prop);

			if(temp == null)
				return;

			bool hasChanged = false;
			EditorGUI.BeginChangeCheck();

			string s = "";

			if(prop.hasMultipleDifferentValues)
			{
				s = "--";
			}
			else
			{
				s = ((UInt64)temp).ToString();
			}

			s = EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			if(EditorGUI.EndChangeCheck())
			{
				try
				{
					temp = UInt64.Parse(s) + (UInt64)tempInt;
					hasChanged = true;
				}
				catch
				{
					if(string.IsNullOrEmpty(s))
					{
						temp = (UInt64)tempInt;
						hasChanged = true;
					}

					// field had an invalid value. Ignore change this frame
				}
			}

			if(hasChanged)
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Inspector");

				SetReflectedFieldRecursively(prop, temp);

				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			EditorGUI.EndProperty();
		}
	}
#pragma warning restore 618
}
