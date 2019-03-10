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
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace HeavyDutyInspector
{

	public class IllogikaDrawer : PropertyDrawer {

		private static readonly Color objectNullColor = Color.yellow;
		private static readonly Color objectSelfColor = Color.white;
		private static readonly Color objectOtherColor = Color.cyan;

		private bool doOnlyOnce;
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			if(prop.propertyType == SerializedPropertyType.Rect)
			{
				return base.GetPropertyHeight(prop, label) * 2;
			}
			
			return base.GetPropertyHeight(prop, label);
		}

		protected void WrongVariableTypeWarning(string attributeName, string variableType)
		{
			if(!doOnlyOnce)
			{
				Debug.LogError(string.Format("The {0}Attribute is designed to be applied to {1} only!", attributeName, variableType));
				doOnlyOnce = true;
			}
		}

		private static Dictionary<string, GameObject> targetObjects = new Dictionary<string, GameObject>();

		protected void OnComponentGUI (Rect position, SerializedProperty prop, GUIContent label, string fieldName, string[] requiredValues, string defaultObject, bool isPrefab, int rightOffset)
		{
			if(prop.propertyType != SerializedPropertyType.ObjectReference)
			{
				WrongVariableTypeWarning("ComponentSelection", "object references");
				return;
			}
			
			int originalIndentLevel = EditorGUI.indentLevel;

			GUIContent tempLabel = new GUIContent(label);
			tempLabel.tooltip = "1st Line : GameObject Reference. The GameObject on which to select a Component. This is not Serialized.\n\n2nd Line : The Selected Component. This is your actual Serialized variable.\n\n(GameObject reference is displayed in YELLOW color when the selected component is null, WHITE whe the selected component is on the same GameObject as this script and CYAN when the selected component is on another GameObject.)";
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), tempLabel);
			EditorGUI.indentLevel = 0;
			
			if(prop.hasMultipleDifferentValues)
			{
				position.height = GetPropertyHeight(prop, label);
				
				#if !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2
				if(fieldInfo.FieldType.IsArray || fieldInfo.FieldType.IsGenericType)
					position.x += 15;
				#endif
				EditorGUI.HelpBox(position, "Multi object editing is not supported for references with different values.", MessageType.Warning);
				return;
			}

			bool firstInit = false;
			if(!targetObjects.ContainsKey(prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath))
			{
				firstInit = true;
				targetObjects.Add(prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath, null);
			}
			
			// Set back the target game object if the drawed object has been deselected.
			if(prop.objectReferenceValue != null && (targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] == null || targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] != (prop.objectReferenceValue as Component).gameObject))
			{
				targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] = (prop.objectReferenceValue as Component).gameObject;
			}
			
			if(firstInit && targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] == null)
			{
				firstInit = false;
				try
				{
					targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] = string.IsNullOrEmpty(defaultObject) ? (prop.serializedObject.targetObject as MonoBehaviour).gameObject : isPrefab ? Resources.Load(defaultObject) as GameObject : GameObject.Find(defaultObject);
				}
				catch
				{
					targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] = string.IsNullOrEmpty(defaultObject) ? null : isPrefab ? Resources.Load(defaultObject) as GameObject : null;
				}
			}
			
			if((prop.serializedObject.targetObject as ScriptableObject) != null)
			{
				if(targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] != null && !AssetDatabase.Contains(targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath]))
				   targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] = null;
			}
			
			position.height /= 2;
			position.width -= rightOffset;
			
			EditorGUI.BeginChangeCheck();
			
			Color tempColor = GUI.color;
			try{
				GUI.color = targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] == (prop.serializedObject.targetObject as MonoBehaviour).gameObject ? objectSelfColor : objectOtherColor;
			}
			catch{
				GUI.color = objectOtherColor;
			}

			if(targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] == null)
				GUI.color = objectNullColor;

				targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] = EditorGUI.ObjectField(position, targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath], typeof(GameObject), true) as GameObject;
			
			GUI.color = tempColor;

			if(targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] == null)
			{
				prop.objectReferenceValue = null;
				return;
			}

			position.width += rightOffset;

			System.Type componentType;
			List<Component> components;
			if(fieldInfo.FieldType.IsArray)
				componentType = fieldInfo.FieldType.GetElementType();
			else if(fieldInfo.FieldType.IsGenericType)
				componentType = fieldInfo.FieldType.GetGenericArguments()[0];
			else
				componentType = fieldInfo.FieldType;

			components = targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath].GetComponents(componentType).ToList();

			if(EditorGUI.EndChangeCheck())
			{
				Undo.RecordObjects(prop.serializedObject.targetObjects, "Change Target Object");

				prop.objectReferenceValue = null;

				foreach(Object obj in prop.serializedObject.targetObjects)
				{
					EditorUtility.SetDirty(obj);
				}
			}

			if(components.Contains(prop.serializedObject.targetObject as Component))
				components.Remove(prop.serializedObject.targetObject as Component);
			
			List<string> componentsNames = new List<string>();
			Dictionary<System.Type, int> componentsNumbers = new Dictionary<System.Type, int>();
			Dictionary<string, int> namedMonoBehavioursNumbers = new Dictionary<string, int>();
			List<Component> markedForDeletion = new List<Component>();

			foreach(Component component in components)
			{
				if(!componentsNumbers.ContainsKey(component.GetType()))
				{
					componentsNumbers.Add(component.GetType(), 1);
				}
				
				if(component is NamedMonoBehaviour)
				{ 
					if(string.IsNullOrEmpty((component as NamedMonoBehaviour).scriptName))
					{
						if(string.IsNullOrEmpty(fieldName))
						{
							componentsNames.Add(component.GetType().ToString().Replace("UnityEngine.", "") + " " + componentsNumbers[component.GetType()]++.ToString());
						}
						else
						{
							System.Object val = GetFieldOrPropertyValue(component, fieldName);

							if(requiredValues != null && requiredValues.Length > 0)
							{
								if(requiredValues.Contains(val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")))
									componentsNames.Add(component.GetType().ToString().Replace("UnityEngine.", "") + " " + componentsNumbers[component.GetType()]++.ToString() + " (" + (val == null ? "null" : val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")) + ")");
								else
									markedForDeletion.Add(component);
							}
							else
							{
								componentsNames.Add(component.GetType().ToString().Replace("UnityEngine.", "") + " " + componentsNumbers[component.GetType()]++.ToString() + " (" + (val == null ? "null" : val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")) + ")");
							}
						}
					}
					else
					{
						if(namedMonoBehavioursNumbers.ContainsKey((component as NamedMonoBehaviour).scriptName))
						{
							(component as NamedMonoBehaviour).scriptName += (" " + namedMonoBehavioursNumbers[(component as NamedMonoBehaviour).scriptName]++);
						}
						else
						{
							namedMonoBehavioursNumbers.Add((component as NamedMonoBehaviour).scriptName, 2);
						}

						if(string.IsNullOrEmpty(fieldName))
						{
							componentsNames.Add((component as NamedMonoBehaviour).scriptName);
						}
						else
						{
							System.Object val = GetFieldOrPropertyValue(component, fieldName);
							if(requiredValues != null && requiredValues.Length > 0)
							{
								if(requiredValues.Contains(val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")))
									componentsNames.Add((component as NamedMonoBehaviour).scriptName + " (" + (val == null ? "null" : val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")) + ")");
								else
									markedForDeletion.Add(component);
							}
							else
							{
								componentsNames.Add((component as NamedMonoBehaviour).scriptName + " (" + (val == null ? "null" : val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")) + ")");
							}
						}
						
					}
				}
				else
				{
					if(string.IsNullOrEmpty(fieldName))
					{
						componentsNames.Add(component.GetType().ToString().Replace("UnityEngine.", "") + " " + componentsNumbers[component.GetType()]++.ToString());
					}
					else
					{
						System.Object val = GetFieldOrPropertyValue(component, fieldName);
						if(requiredValues != null && requiredValues.Length > 0)
						{
							if(requiredValues.Contains(val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")))
								componentsNames.Add(component.GetType().ToString().Replace("UnityEngine.", "") + " " + componentsNumbers[component.GetType()]++.ToString() + " (" + (val == null ? "null" : val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")) + ")");
							else
								markedForDeletion.Add(component);
						}
						else
						{
							componentsNames.Add(component.GetType().ToString().Replace("UnityEngine.", "") + " " + componentsNumbers[component.GetType()]++.ToString() + " (" + (val == null ? "null" : val.ToString().Replace(" (" + val.GetType().ToString() + ")", "")) + ")");
						}
					}
				}
			}

			foreach(Component component in markedForDeletion)
			{
				components.Remove(component);
			}

			if(components.Count == 0)
			{
				targetObjects[prop.serializedObject.targetObject.GetHashCode().ToString() + prop.propertyPath] = null;
				prop.objectReferenceValue = null;
				return;
			}

			components.Insert(0, null);
			componentsNames.Insert(0, "None (" + componentType.ToString().Replace("UnityEngine.", "") + ")");
			
			position.y += position.height;
			
			int index = 0;
			
			try
			{
				index = components.IndexOf(prop.objectReferenceValue as Component);
			}
			catch
			{
				prop.objectReferenceValue = null;
			}

            try
            {
				if(index != 0 && typeof(NamedMonoBehaviour).IsAssignableFrom(components[index].GetType()))
					GUI.backgroundColor = (components[index] as NamedMonoBehaviour).scriptNameColor;

                prop.objectReferenceValue = components[EditorGUI.Popup(position, index, componentsNames.ToArray())];

				GUI.backgroundColor = Color.white;
            }
            catch
            {
			}
			
			EditorGUI.indentLevel = originalIndentLevel;
		}
		
		protected System.Object GetFieldOrPropertyValue(Component component, string fieldName)
		{
			if(component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) != null)
				return component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(component);
			else if(component.GetType().GetProperty(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) != null)
				return component.GetType().GetProperty(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(component, null);
			else
				Debug.LogError(string.Format("{0} does not contain a field or property named {1}!", component, fieldName));

			return "";
		}

		protected virtual void OnNamedMonoBehaviourGUI(Rect position, SerializedProperty property, GUIContent label, System.Type scriptType, int rightOffset)
		{
			position.height /= 2;
			
			int originalIndentLevel = EditorGUI.indentLevel;

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID (FocusType.Passive), label);
			EditorGUI.indentLevel = 0;

			position.width -= rightOffset;

			if(property.hasMultipleDifferentValues)
			{
				EditorGUI.BeginChangeCheck();
				
				Object temp = EditorGUI.ObjectField(position, Resources.Load("-"), scriptType, true);

				if(EditorGUI.EndChangeCheck())
					property.objectReferenceValue = temp;

				position.width += rightOffset;

				position.y += position.height;
				Color color = GUI.color;
				GUI.color = new Color(1, 0.5f, 0);
				EditorGUI.LabelField(position, "[Multiple Values]");
				GUI.color = color;
			}
			else
			{
				property.objectReferenceValue = EditorGUI.ObjectField(position,(NamedMonoBehaviour)property.objectReferenceValue, scriptType, true);

				position.width += rightOffset;

				position.y += position.height;
				
				if(property.objectReferenceValue != null)
				{
					NamedMonoBehaviour monoBehaviour = (NamedMonoBehaviour)property.objectReferenceValue;
					Color color = GUI.color;
					GUI.color = monoBehaviour.scriptNameColor;
					EditorGUI.LabelField(position, string.Format("[{0}]", !string.IsNullOrEmpty(monoBehaviour.scriptName) ? monoBehaviour.scriptName : monoBehaviour.GetType().ToString() ) );
					GUI.color = color;
				}
			}
			
			EditorGUI.indentLevel = originalIndentLevel;
		}

		public void PropertyFieldIncludingSpecialAndFoldouts(SerializedProperty prop, Rect position, Rect basePosition, GUIContent label)
		{
			if(prop.hasChildren)
			{
				prop.isExpanded = EditorGUI.PropertyField(position, prop);

				if(prop.isExpanded)
				{
					EditorGUI.indentLevel += 1;
					Rect childPosition = basePosition;
					int skipNext = 0;
					int skipDepth = 10;
					childPosition.height = EditorGUIUtility.singleLineHeight;
					foreach(SerializedProperty childProp in prop)
					{
						EditorGUI.indentLevel = childProp.depth;

						if(skipNext > 0)
						{
							--skipNext;
							continue;
						}

						if(childProp.depth > skipDepth)
						{
							continue;
						}
						else if (skipDepth < 10)
						{
							skipDepth = 10;
						}

						childPosition.y += childPosition.height + 2;
						childPosition.height = EditorGUI.GetPropertyHeight(childProp, label, false);

						EditorGUI.PropertyField(childPosition, childProp);

						if(childProp.hasChildren && (!childProp.isExpanded || childProp.name == "data"))
						{
							skipDepth = childProp.depth;
						}

						if(childProp.propertyType == SerializedPropertyType.Vector2)
							skipNext = 2;
						if(childProp.propertyType == SerializedPropertyType.Vector3)
							skipNext = 3;

						if(childProp.propertyType == SerializedPropertyType.Vector4 || childProp.propertyType == SerializedPropertyType.Rect)
							skipNext = 4;
					}
				}
			}
			else
			{
				EditorGUI.PropertyField(position, prop);
			}
		}

		public float GetPropertyHeightIncludingSpecialAndFoldouts(SerializedProperty prop, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(prop, label, true);
		}

		public float GetCommentHeight(string comment, MessageType messageType)
		{
			GUIStyle style = "HelpBox";
			return Mathf.Max(style.CalcHeight(new GUIContent(comment), Screen.width - (messageType != MessageType.None ? 53 : 35)), EditorGUIUtility.singleLineHeight);
		}

		public static object CallMethod(SerializedProperty prop, MonoBehaviour go, string methodName)
		{
			MethodInfo function = GetMethodRecursively(go.GetType(), methodName);
			if(function == null)
			{
				System.Object targetObject = null;
				function = GetMethodFromField(prop, methodName, out targetObject);

				if(function == null)
				{
					Debug.LogError(string.Format("Function {0} not found in class {1}, its base classes or the current field's class.", methodName, go.GetType().ToString()));
				}
				else
				{
					return function.Invoke(targetObject, null);
				}
			}
			else
			{
				return function.Invoke(go, null);
			}
			return null;
		}

		public static object CallMethod(SerializedProperty prop, ScriptableObject so, string methodName)
		{
			MethodInfo function = GetMethodRecursively(so.GetType(), methodName);
			if(function == null)
			{
				System.Object targetObject = null;
				function = GetMethodFromField(prop, methodName, out targetObject);

				if(function == null)
				{
					Debug.LogError(string.Format("Function {0} not found in class {1}, its base classes or the current field's class.", methodName, so.GetType().ToString()));
				}
				else
				{
					return function.Invoke(targetObject, null);
				}
			}
			else
			{
				return function.Invoke(so, null);
			}
			return null;
		}

		public static MethodInfo GetMethodRecursively(System.Type type, string methodName)
		{
			MethodInfo function = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, new System.Type[0], null );
			if (function == null)
			{
				if(type.BaseType != null)
					return GetMethodRecursively(type.BaseType, methodName);
				else
					return null;
			}
			return function;
		}

		public static MethodInfo GetMethodFromField(SerializedProperty prop, string methodName, out System.Object targetObject)
		{
			GetReflectedFieldInfoRecursively(prop, out targetObject);

			if(targetObject != null)
			{
				return targetObject.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, new System.Type[0], null );
			}
			return null;
		}

		public static T GetReflectedFieldRecursively<T>(SerializedProperty prop, string fieldName = "")
		{
			System.Object targetObject = null;
			FieldInfo info = GetReflectedFieldInfoRecursively(prop, out targetObject, fieldName);

			T temp = default(T);
			try
			{
				temp = (T)(info.GetValue (targetObject));
			}
			catch
			{
				try
				{
					try
					{
						temp = (T)(info.GetValue(targetObject) as IList)[int.Parse(prop.propertyPath.Substring(prop.propertyPath.LastIndexOf("data[") + 5).Split(']')[0])];
					}
					catch
					{
						temp = (T)((System.Object[])info.GetValue(targetObject))[int.Parse(prop.propertyPath.Substring(prop.propertyPath.LastIndexOf("data[") + 5).Split(']')[0])];
					}
				}
				catch
				{
					// Debug.LogWarning(string.Format("The script has no property named {0}.", prop.name));
				}
			}
			
			return temp;
		}

		public static FieldInfo GetReflectedFieldInfoRecursively(SerializedProperty prop, out System.Object targetObject, string fieldName = "")
		{
			string fullpath = prop.propertyPath;

			string propName = prop.name;

			if(!string.IsNullOrEmpty(fieldName))
			{
				fullpath = fullpath.Replace(prop.name, fieldName);
				propName = fieldName;
			}

			List<string> pathList = fullpath.Split ('.').ToList();

			if(propName == "data")
			{
				// This is a list, we must find the real name by getting the string before "Array"

				propName = pathList[pathList.LastIndexOf("Array")-1];
			}

			pathList.Remove("Array");

			targetObject = prop.serializedObject.targetObject;

			if((prop.serializedObject.targetObject as MonoBehaviour) != null)
			{
				FieldInfo temp = (targetObject as MonoBehaviour).GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				if(temp != null)
				{
					return temp;
				}

				return GetReflectedFieldInfoRecursively(prop.serializedObject.targetObject, (prop.serializedObject.targetObject as MonoBehaviour).GetType().GetField( ( prop.propertyPath.Split ('.').Length == 1 && !string.IsNullOrEmpty(propName) ? propName : prop.propertyPath.Split ('.') [0]), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathList, out targetObject);
			}

			if((prop.serializedObject.targetObject as ScriptableObject) != null)
			{
				FieldInfo temp = (targetObject as ScriptableObject).GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				if(temp != null)
				{
					return temp;
				}

				return GetReflectedFieldInfoRecursively(prop.serializedObject.targetObject, (prop.serializedObject.targetObject as ScriptableObject).GetType().GetField(( prop.propertyPath.Split ('.').Length == 1 && !string.IsNullOrEmpty(propName) ? propName : prop.propertyPath.Split ('.') [0]), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathList, out targetObject);
			}

			return null;
		}

		static FieldInfo GetReflectedFieldInfoRecursively(System.Object targetObject, FieldInfo field, string propName, List<string> pathNames, out System.Object outObject)
		{
			if (pathNames.Count > 1 && pathNames[0] != propName)
			{
				pathNames.RemoveAt(0);

				if(pathNames[0].Contains("data["))
				{
					// The next field is hidden inside a list
					int pathIndex = int.Parse(pathNames[0].Replace("data[", "").Replace("]",""));

					pathNames.RemoveAt(0);

					try
					{
						try
						{
							// If index is greater than length, the list's size is being increased this frame, return null to prevent an error and try again next frame.
							if((field.GetValue(targetObject) as IList).Count <= pathIndex)
							{
								outObject = null;
								return null;
							}
						}
						catch
						{
							if(((System.Object[])field.GetValue(targetObject)).Length <= pathIndex)
							{
								outObject = null;
								return null;
							}
						}
					}
					catch
					{
						// If it breaks here, the value is null. Return for this frame to prevent an error and try again next frame.
						outObject = null;
						return null;
					}

					try
					{
						return GetReflectedFieldInfoRecursively((field.GetValue(targetObject) as IList)[pathIndex], field.FieldType.GetGenericArguments()[0].GetField(pathNames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathNames, out outObject);
					}
					catch
					{
						return GetReflectedFieldInfoRecursively(((System.Object[])field.GetValue(targetObject))[pathIndex], field.FieldType.GetElementType().GetField(pathNames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathNames, out outObject);
					}
				}
				return GetReflectedFieldInfoRecursively(field.GetValue(targetObject), field.FieldType.GetField(pathNames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathNames, out outObject);
			}
			else
			{
				outObject = targetObject;
				return field;
			}
		}

		public static void SetReflectedFieldRecursively<T>(SerializedProperty prop, T value) where T : class
		{
			List<string> pathList = prop.propertyPath.Split ('.').ToList();

			string propName = prop.name;
			
			if(propName == "data")
			{
				// This is a list, we must find the real name by getting the string before "Array"
				
				propName = pathList[pathList.LastIndexOf("Array")-1];
			}

			pathList.Remove("Array");

			if((prop.serializedObject.targetObject as MonoBehaviour) != null)
			{
				foreach (System.Object targetObject in prop.serializedObject.targetObjects)
				{
					SetReflectedFieldRecursively(targetObject, (prop.serializedObject.targetObject as MonoBehaviour).GetType().GetField(prop.propertyPath.Split('.')[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathList, value, prop.propertyPath);
				}
			}
			
			if((prop.serializedObject.targetObject as ScriptableObject) != null)
			{
				foreach (System.Object targetObject in prop.serializedObject.targetObjects)
				{
					SetReflectedFieldRecursively(targetObject, (prop.serializedObject.targetObject as ScriptableObject).GetType().GetField(prop.propertyPath.Split('.')[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathList, value, prop.propertyPath);
				}
			}
		}
		
		static void SetReflectedFieldRecursively<T>(System.Object targetObject, FieldInfo field, string propName, List<string> pathNames, T value, string fullName) where T : class
		{
			if (pathNames.Count >= 1 && pathNames[0] != propName)
			{
				pathNames.RemoveAt(0);

				if(pathNames[0].Contains("data["))
				{
					// The next field is hidden inside a list
					int pathIndex = int.Parse(pathNames[0].Replace("data[", "").Replace("]",""));
					
					pathNames.RemoveAt(0);

					try
					{
						SetReflectedFieldRecursively((field.GetValue(targetObject) as IList)[pathIndex], field.FieldType.GetGenericArguments()[0].GetField(pathNames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathNames, value, fullName);
					}
					catch
					{
						SetReflectedFieldRecursively(((System.Object[])field.GetValue(targetObject))[pathIndex], field.FieldType.GetElementType().GetField(pathNames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathNames, value, fullName);
					}
				}
				SetReflectedFieldRecursively(field.GetValue(targetObject), field.FieldType.GetField(pathNames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public), propName, pathNames, value, fullName);
			}
			else
			{
				try
				{
					field.SetValue(targetObject, value);
				}
				catch
				{
					try
					{
						if(field != null)
						{
							try
							{
								(field.GetValue(targetObject) as IList)[int.Parse(fullName.Substring(fullName.LastIndexOf("data[") + 5).Split(']')[0])] = value;
							}
							catch
							{
								((System.Object[])field.GetValue(targetObject))[int.Parse(fullName.Substring(fullName.LastIndexOf("data[") + 5).Split(']')[0])] = value;
							}
						}
					}
					catch
					{
						 //Debug.LogWarning(string.Format("The script has no property named {0}.", propName));
					}
				}
			}
		}
	}
}
