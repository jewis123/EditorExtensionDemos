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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HeavyDutyInspector {

	[CustomPropertyDrawer (typeof (AssetPathAttribute))]
	public class AssetPathDrawer : IllogikaDrawer {

		AssetPathAttribute assetPathAttribute { get { return ((AssetPathAttribute) attribute); } }

		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label) {
			return base.GetPropertyHeight (prop, label) * 2;
		}

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label) {
			EditorGUI.BeginProperty (position, label, prop);

			if (prop.propertyType != SerializedPropertyType.String) {
				WrongVariableTypeWarning ("AssetPath", "strings");
				return;
			}

			int originalIndentLevel = EditorGUI.indentLevel;

			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
			EditorGUI.indentLevel = 0;

			position.height /= 2;

			if (prop.hasMultipleDifferentValues) {
				EditorGUI.BeginChangeCheck ();

				Object temp = EditorGUI.ObjectField (position, Resources.Load ("-"), assetPathAttribute.type, false);

				if (EditorGUI.EndChangeCheck ()) {
					assetPathAttribute.obj = temp;
					prop.stringValue = FormatString (AssetDatabase.GetAssetPath (temp));
				}
			} else {
				EditorGUI.BeginChangeCheck ();

				Object obj = SelectObject (prop.stringValue);

				obj = EditorGUI.ObjectField (position, obj, assetPathAttribute.type, false);
				string temp = AssetDatabase.GetAssetPath (obj);

				if (EditorGUI.EndChangeCheck ()) {
					prop.stringValue = temp;
					prop.stringValue = FormatString (prop.stringValue);
				}

				position.y += base.GetPropertyHeight (prop, label);  //计算新位置

				EditorGUI.SelectableLabel (position, prop.stringValue);//创建一个可选的Lable用来显示路径
			}

			EditorGUI.indentLevel = originalIndentLevel;
			EditorGUI.EndProperty ();
		}

		Object SelectObject (string path) {
			if (assetPathAttribute.folderDepth > 0) {
				path = path.Replace ('\\', '/');
				string fullPath = (from p in AssetDatabase.GetAllAssetPaths () where p.Replace ('\\', '/').Contains (path) select p).FirstOrDefault ();

				if (string.IsNullOrEmpty (fullPath))
					return null;
				else
					return AssetDatabase.LoadAssetAtPath (fullPath, assetPathAttribute.type);
			}

			switch (assetPathAttribute.pathOptions) {
				case PathOptions.RelativeToAssets:
					return AssetDatabase.LoadAssetAtPath ("Assets/" + path, assetPathAttribute.type);
				case PathOptions.RelativeToResources:
					return Resources.Load (path);
				case PathOptions.FilenameOnly:
					string fullPath = (from p in AssetDatabase.GetAllAssetPaths () where Path.GetFileName (p).Equals (path) select p).FirstOrDefault ();
					return AssetDatabase.LoadAssetAtPath (fullPath, assetPathAttribute.type);
			}
			return null;
		}

		string FormatString (string path) {
			switch (assetPathAttribute.pathOptions) {
				case PathOptions.RelativeToAssets:
					path = path.Substring (path.IndexOf ("Assets/") + 7);
					break;
				case PathOptions.RelativeToResources:
					if (path.Contains ("Resources/"))
						path = path.Substring (path.IndexOf ("Resources/") + 10).Replace (Path.GetExtension (path), "");
					else
						Debug.LogWarning ("Selected asset is not in a Resources folder");
					break;
				case PathOptions.FilenameOnly:
					path = Path.GetFileName (path);
					break;
			}

			if (assetPathAttribute.folderDepth > 0) {
				string[] pathParts = path.Split ('/', '\\');
				int i = pathParts.Length - assetPathAttribute.folderDepth;
				if (i < 1)
					i = 1;

				if (pathParts.Length < 2)
					return path;

				path = pathParts[i - 1];
				for (; i < pathParts.Length; ++i) {
					path = Path.Combine (path, pathParts[i]);
				}
			}

			return path.Replace ('\\', '/');
		}
	}
}