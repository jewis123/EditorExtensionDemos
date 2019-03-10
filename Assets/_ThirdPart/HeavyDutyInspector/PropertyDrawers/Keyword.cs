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
using System.Linq;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[System.Serializable]
	public class Keyword : System.Object
	{

		[SerializeField]
		[HideInInspector]
		protected string _key;

		public Keyword()
		{
			_key = "";
		}

		protected Keyword(string key)
		{
			_key = key ?? "";
		}

		public static implicit operator string(Keyword word)
		{
			return word == null ? "" : word._key.Split('/').LastOrDefault();
		}

		public static implicit operator Keyword(string key)
		{
			return new Keyword(key);
		}

		public string category
		{
			get
			{
				if (_key.LastIndexOf('/') < 0)
					return "";
				else
					return _key.Substring(0, _key.LastIndexOf('/'));
			}
		}

		public string fullPath
		{
			get { return _key; }
		}

		public override string ToString()
		{
			return (string)this;
		}
	}

	public class Keywords : System.Object
	{

		private static KeywordsConfig _config;
		public static KeywordsConfig Config
		{
			get
			{
				if (_config == null)
				{
					_config = Resources.Load("Config/KeywordsConfig") as KeywordsConfig;
				}
				return _config;
			}
		}
	}


#if UNITY_EDITOR

	public static partial class EditorGUIEx
	{
		private static Stack<bool> changedStack = new Stack<bool>();

		public static void BeginChangeCheck()
		{
			changedStack.Push(false);
		}

		public static bool EndChangeCheck()
		{
			return changedStack.Pop();
		}

		private static void RecordChange()
		{
			if(changedStack.Count > 0)
			{
				changedStack.Pop();
				changedStack.Push(true);
			}
		}

		private static Texture2D olPlus
		{
			get;
			set;
		}

		private static Texture2D olMinus
		{
			get;
			set;
		}

		private static Texture2D olRefresh
		{
			get;
			set;
		}

		private static Dictionary<KeywordsConfig, List<string>> categories = new Dictionary<KeywordsConfig, List<string>>();
		private static Dictionary<KeywordsConfig, List<string>> keywords = new Dictionary<KeywordsConfig, List<string>>();


		static string newValue;

		static int currentCategory;

		public static bool IsAddingKeyword
		{
			get { return isAddingString; }
		}

		static bool isAddingString;

		static void Init(KeywordsConfig config)
		{
			if(olPlus == null)
				olPlus = (Texture2D)Resources.Load("OlPlusGreen");

			if(olMinus == null)
				olMinus = (Texture2D)Resources.Load("OLMinusRed");

			if(olRefresh == null)
				olRefresh = (Texture2D)Resources.Load("OLRefresh");

			if(!categories.ContainsKey(config) && !keywords.ContainsKey(config))
				PopulateLists(config);
		}

		static void PopulateLists(KeywordsConfig config)
		{
			if(categories.ContainsKey(config))
				categories[config].Clear();
			else
				categories.Add(config, new List<string>());

			if(keywords.ContainsKey(config))
				keywords[config].Clear();
			else
				keywords.Add(config, new List<string>());

			foreach(KeywordCategory category in config.keyWordCategories)
			{
				if(!string.IsNullOrEmpty(category.name))
					categories[config].Add(category.name);

				foreach(string keyword in category.keywords)
				{
					if(!string.IsNullOrEmpty(keyword))
						keywords[config].Add(category.name + (string.IsNullOrEmpty(category.name) ? "" : "/") + keyword);
				}
			}

			categories[config].Sort();
			categories[config].Insert(0, "None");

			keywords[config].Sort();
			keywords[config].Insert(0, "None");
		}

		public static string KeywordField(Rect position, string label, string keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			position = UnityEditor.EditorGUI.PrefixLabel(position, UnityEditor.EditorGUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			return KeywordField(position, (Keyword)keyword, keywordsConfig, selectionOnly).fullPath;
		}

		public static string KeywordField(Rect position, string keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			return KeywordField(position, (Keyword)keyword, keywordsConfig, selectionOnly).fullPath;
		}

		public static Keyword KeywordField(Rect position, string label, Keyword keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			position = UnityEditor.EditorGUI.PrefixLabel(position, UnityEditor.EditorGUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));

			return KeywordField(position, keyword, keywordsConfig, selectionOnly);
		}

		public static Keyword KeywordField(Rect position, Keyword keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			List<KeywordCategory> config = keywordsConfig.keyWordCategories;
			Init(keywordsConfig);

			int originalIndentLevel = UnityEditor.EditorGUI.indentLevel;
			UnityEditor.EditorGUI.indentLevel = 0;

			if(selectionOnly)
				position.width -= 16;
			else
				position.width -= 48;

			string temp = "";

			if(isAddingString)
			{
				position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

				currentCategory = UnityEditor.EditorGUI.Popup(position, currentCategory, categories[keywordsConfig].ToArray());

				position.y += position.height;

				UnityEditor.EditorGUI.BeginChangeCheck();

				temp = UnityEditor.EditorGUI.TextField(position, newValue);

				if(UnityEditor.EditorGUI.EndChangeCheck())
				{
					RecordChange();
					newValue = temp;
				}
			}
			else
			{
				temp = keyword.fullPath;

				if(temp == "")
					temp = "None";

				Color originalColor = GUI.color;
				int index = keywords[keywordsConfig].IndexOf(temp);

				if(index < 0)
				{
					index = keywords[keywordsConfig].Count;
					keywords[keywordsConfig].Add(temp + " (Missing)");
					GUI.color = Color.red;
				}

				UnityEditor.EditorGUI.BeginChangeCheck();

				index = UnityEditor.EditorGUI.Popup(position, index, keywords[keywordsConfig].ToArray());

				if(UnityEditor.EditorGUI.EndChangeCheck())
				{
					RecordChange();
					temp = keywords[keywordsConfig][index];
				}

				if(temp == "None")
					temp = "";

				GUI.color = originalColor;
			}

			position.y += 1;
			position.x += position.width;
			position.width = 16;

			if(!selectionOnly)
			{
				if(GUI.Button(position, olPlus, "Label"))
				{
					if(temp.Contains(" (Missing)"))
					{
						KeywordCategory tempCategory = (from c in config where c.name == keyword.category select c).FirstOrDefault();

						if(tempCategory == null)
						{
							config.Add(new KeywordCategory(keyword.category));
							config.Last().keywords.Add(keyword);
						}
						else
						{
							tempCategory.keywords.Add(keyword);
						}
						UnityEditor.EditorUtility.SetDirty(keywordsConfig);
						PopulateLists(keywordsConfig);
						Debug.Log("Missing");
					}
					else
					{
						if(isAddingString)
						{
							config[currentCategory].keywords.Add(newValue);
							UnityEditor.EditorUtility.SetDirty(keywordsConfig);

							keywords[keywordsConfig].Add(config[currentCategory].name + (currentCategory == 0 ? "" : "/") + newValue);

							config[currentCategory].keywords.Sort();
							keywords[keywordsConfig].RemoveAt(0);
							keywords[keywordsConfig].Sort();
							keywords[keywordsConfig].Insert(0, "None");

							temp = (Keyword)(config[currentCategory].name + (currentCategory == 0 ? "" : "/") + newValue);
						}

						isAddingString = !isAddingString;
					}
				}

				position.x += 16;

				if(GUI.Button(position, olMinus, "Label"))
				{
					if(isAddingString)
					{
						newValue = "";
						isAddingString = false;
					}
					else
					{
						if(UnityEditor.EditorUtility.DisplayDialog("Remove string?", string.Format("Are you sure you want to remove {0} from the string list?", temp), "Yes", "No"))
						{
							keywords[keywordsConfig].Remove(temp);

							if(temp.Contains('/'))
								(from c in config where c.name == temp.Substring(0, temp.LastIndexOf('/')) select c.keywords).ToList().FirstOrDefault().Remove(keyword);
							else
								config[0].keywords.Remove(keyword);

							UnityEditor.EditorUtility.SetDirty(keywordsConfig);
						}
					}
				}

				position.x += 16;
			}
			position.y -= 1;
			position.width += 2;
			position.height += 2;

			if(GUI.Button(position, olRefresh, "Label"))
			{
				PopulateLists(keywordsConfig);
			}

			if(temp != null && temp.Contains(" (Missing)"))
			{
				temp = temp.Replace(" (Missing)", "");
			}

			UnityEditor.EditorGUI.indentLevel = originalIndentLevel;

			return (Keyword)temp;
		}

	}

	public static partial class EditorGUILayoutEx
	{
		public static string KeywordField(string keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			return KeywordField((Keyword)keyword, keywordsConfig, selectionOnly).fullPath;
		}

		public static Keyword KeywordField(Keyword keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();
			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			Keyword temp = EditorGUIEx.KeywordField(position, keyword, keywordsConfig, selectionOnly);

			if(EditorGUIEx.IsAddingKeyword)
				UnityEditor.EditorGUILayout.LabelField("");

			return temp;
		}

		public static string KeywordField(string label, string keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			return KeywordField(label, (Keyword)keyword, keywordsConfig, selectionOnly).fullPath;
		}

		public static Keyword KeywordField(string label, Keyword keyword, KeywordsConfig keywordsConfig, bool selectionOnly = false)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();
			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			Keyword temp = EditorGUIEx.KeywordField(position, label, keyword, keywordsConfig, selectionOnly);

			if(EditorGUIEx.IsAddingKeyword)
				UnityEditor.EditorGUILayout.LabelField("");

			return temp;
		}
	}

#endif

}
