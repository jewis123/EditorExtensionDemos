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
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(Scene))]
	public class SceneDrawer : IllogikaDrawer {

		protected List<string> allScenes = new List<string>();

		protected bool filterApplied = false;

		protected string filter;

		public SceneDrawer()
		{
			string[] allPaths = AssetDatabase.GetAllAssetPaths();

			allScenes = (from p in allPaths where p.EndsWith(".unity") select p.Replace("Assets/", "")).ToList();

			allScenes.Insert(0, "None");
		}

		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			position = EditorGUI.PrefixLabel(position, EditorGUIUtility.GetControlID(FocusType.Passive), label);

			int originalIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			string scene = prop.FindPropertyRelative("_name").stringValue;

			string temp = scene;

			if (string.IsNullOrEmpty(temp))
				temp = "None";

			Color originalColor = GUI.color;
			int index = allScenes.IndexOf(temp);

			if (index < 0)
			{
				index = allScenes.Count;
				allScenes.Add(temp + " (Missing)");
				GUI.color = Color.red;
			}

			EditorGUI.BeginChangeCheck();

			index = EditorGUI.Popup(position, index, allScenes.ToArray());

			temp = allScenes[index];

			if (temp == "None")
				temp = "";

			if(EditorGUI.EndChangeCheck())
			{
				prop.FindPropertyRelative("_name").stringValue = temp.Replace(" (Missing)", "");

				EditorUtility.SetDirty(prop.serializedObject.targetObject);

				if (!string.IsNullOrEmpty(temp) && !temp.Contains(" (Missing)"))
				{
					bool hasScene = false;
					foreach (EditorBuildSettingsScene buildSettingsScene in EditorBuildSettings.scenes)
					{
						if (buildSettingsScene.path == "Assets/" + filter + temp)
						{
							hasScene = true;
							break;
						}
					}

					if (!hasScene)
					{
						if (EditorUtility.DisplayDialog("Add to Build Settings?", "The scene you selected is not in the Build settings! Do you want to add it?", "Yes", "No"))
						{
							List<EditorBuildSettingsScene> addingScenes = EditorBuildSettings.scenes.ToList();
							addingScenes.Add(new EditorBuildSettingsScene("Assets/" + filter + temp, true));
							EditorBuildSettings.scenes = addingScenes.ToArray();
						}
					}
				}
			}

			GUI.color = originalColor;

			EditorGUI.indentLevel = originalIndentLevel;

			EditorGUI.EndProperty();
		}
	}

	[CustomPropertyDrawer(typeof(SceneAttribute))]
	public class SceneAttributeDrawer : SceneDrawer {

		SceneAttribute sceneAttribute { get { return ((SceneAttribute)attribute); } }

		

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			if(!filterApplied)
			{
				filter = sceneAttribute.BasePath + (sceneAttribute.BasePath.EndsWith("/") ? "" : "/");

				allScenes = (from s in allScenes where s.StartsWith(sceneAttribute.BasePath) select s.Substring( sceneAttribute.BasePath.Length + (sceneAttribute.BasePath.EndsWith("/") ? 0 : 1))).ToList();

				allScenes.Insert(0, "None");

				filterApplied = true;
			}

			base.OnGUI(position, prop, label);

		}
	}

}
