

//----------------------------------------------
//            Heavy-Duty Inspector
//      Copyright © 2013 - 2015  Illogika
//----------------------------------------------

using UnityEngine;
using System.Linq;

namespace HeavyDutyInspector
{

	public enum PathOptions {
		RelativeToAssets,
		RelativeToResources,
		FilenameOnly
	}

	public class AssetPathAttribute : PropertyAttribute {

		public UnityEngine.Object obj
		{
			get; set;
		}

		public System.Type type
		{
			get;
			private set;
		}

		public PathOptions pathOptions
		{
			get;
			private set;
		}

		public int folderDepth
		{
			get;
			private set;
		}

		public string actualAssetVariable
		{
			get;
			private set;
		}

		/// <summary>
		/// Displays a strings as a reference to get the asset's path without risking typing errors.
		/// </summary>
		/// <param name="type">The asset's type.</param>
		/// <param name="pathOptions">The way your path should be formatted. Relative to the Assets folder, relative to a Resources folder and with no extension, or just the filename.</param>
		public AssetPathAttribute(System.Type type, PathOptions pathOptions)
		{
			folderDepth = -1;
			this.pathOptions = pathOptions;
			this.type = type;
		}

		/// <summary>
		/// Displays a strings as a reference to get the asset's path without risking typing errors. Will accept any asset.
		/// </summary>
		/// <param name="pathOptions">The way your path should be formatted. Relative to the Assets folder, relative to a Resources folder and with no extension, or just the filename.</param>
		public AssetPathAttribute(PathOptions pathOptions)
		{
			folderDepth = -1;
			this.pathOptions = pathOptions;
			this.type = typeof(UnityEngine.Object);
		}

		/// <summary>
		/// Displays a strings as a reference to get the asset's path without risking typing errors.
		/// </summary>
		/// <param name="type">The asset's type.</param>
		/// <param name="folderDepth">How many folders to keep in the assetPath starting with the containing folder.</param>
		public AssetPathAttribute(System.Type type, int folderDepth)
		{
			this.folderDepth = folderDepth;

			if(folderDepth == 0)
				this.pathOptions = PathOptions.FilenameOnly;
			else
				this.pathOptions = PathOptions.RelativeToAssets;

			this.type = type;
		}

		/// <summary>
		/// Displays a strings as a reference to get the asset's path without risking typing errors. Will accept any asset.
		/// </summary>
		/// <param name="folderDepth">How many folders to keep in the assetPath starting with the containing folder.</param>
		public AssetPathAttribute(int folderDepth)
		{
			this.folderDepth = folderDepth;

			if(folderDepth == 0)
				this.pathOptions = PathOptions.FilenameOnly;
			else
				this.pathOptions = PathOptions.RelativeToAssets;

			this.type = typeof(UnityEngine.Object);
		}
	}
	
#if UNITY_EDITOR
	public static partial class EditorGUIEx
	{
		public static string AssetPath(Rect position, string label, string assetPath, PathOptions pathOptions)
		{
			return AssetPath(position, label, assetPath, typeof(System.Object), pathOptions);
		}

		public static string AssetPath(Rect position, string label, string assetPath, System.Type type, PathOptions pathOptions)
		{
			position = UnityEditor.EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));
			UnityEditor.EditorGUI.indentLevel = 0;

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			UnityEditor.EditorGUI.BeginChangeCheck();

			UnityEngine.Object obj = SelectObject(assetPath, pathOptions, type);

			obj = UnityEditor.EditorGUI.ObjectField(position, obj, type, false);
			string temp = UnityEditor.AssetDatabase.GetAssetPath(obj);

			if(UnityEditor.EditorGUI.EndChangeCheck())
			{
				temp = FormatString(temp, pathOptions);
			}

			position.y += UnityEditor.EditorGUIUtility.singleLineHeight;
			;

			UnityEditor.EditorGUI.SelectableLabel(position, temp);

			return temp;
		}

		static UnityEngine.Object SelectObject(string path, PathOptions pathOptions, System.Type type)
		{
			switch(pathOptions)
			{
				case PathOptions.RelativeToAssets:
					return UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/" + path, type);
				case PathOptions.RelativeToResources:
					return Resources.Load(path);
				case PathOptions.FilenameOnly:
					string fullPath = (from p in UnityEditor.AssetDatabase.GetAllAssetPaths() where System.IO.Path.GetFileName(p).Equals(path) select p).FirstOrDefault();
					return UnityEditor.AssetDatabase.LoadAssetAtPath(fullPath, type);
			}
			return null;
		}

		static string FormatString(string path, PathOptions pathOptions)
		{
			switch(pathOptions)
			{
				case PathOptions.RelativeToAssets:
					path = path.Substring(path.IndexOf("Assets/") + 7);
					break;
				case PathOptions.RelativeToResources:
					if(path.Contains("Resources/"))
						path = path.Substring(path.IndexOf("Resources/") + 10).Replace(System.IO.Path.GetExtension(path), "");
					else
						Debug.LogWarning("Selected asset is not in a Resources folder");
					break;
				case PathOptions.FilenameOnly:
					path = System.IO.Path.GetFileName(path);
					break;
			}

			return path;
		}
	}

	public static partial class EditorGUILayoutEx
	{
		public static string AssetPath(string label, string assetPath, PathOptions pathOptions)
		{
			return AssetPath(label, assetPath, typeof(System.Object), pathOptions);
		}

		public static string AssetPath(string label, string assetPath, System.Type type, PathOptions pathOptions)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();
			UnityEditor.EditorGUILayout.LabelField("");

			return EditorGUIEx.AssetPath(position, label, assetPath, type, pathOptions);
		}
	}

#endif

}
