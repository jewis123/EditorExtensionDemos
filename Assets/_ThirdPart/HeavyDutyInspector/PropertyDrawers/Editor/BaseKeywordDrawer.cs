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
using System.Collections.Generic;

namespace HeavyDutyInspector
{

public class BaseKeywordDrawer : IllogikaDrawer {

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

	protected KeywordsConfig scriptableConfig;
	protected List<KeywordCategory> config;

	private List<string> categories = new List<string> ();
	private List<string> keywords = new List<string>();

	string newValue;

	int currentCategory;

	protected void Init()
	{
		if(olPlus == null)
			olPlus = (Texture2D)Resources.Load("OlPlusGreen");

		if(olMinus == null)
			olMinus = (Texture2D)Resources.Load("OLMinusRed");

		PopulateLists();
	}

	protected void PopulateLists()
	{
		categories.Clear();
		keywords.Clear();

		foreach (KeywordCategory category in config)
		{
			if (!string.IsNullOrEmpty(category.name))
				categories.Add(category.name);

			foreach (string keyword in category.keywords)
			{
				if (!string.IsNullOrEmpty(keyword))
					keywords.Add(category.name + (string.IsNullOrEmpty(category.name) ? "" : "/") + keyword);
			}
		}

		categories.Sort();
		categories.Insert(0, "Uncategorized");

		keywords.Sort();
		keywords.Insert(0, "Empty String");
	}

	public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
	{
		return base.GetPropertyHeight(prop, label) * (isAddingString ? 2 : 1);
	}

	bool isAddingString;

	public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
	{
		string keyword = prop.FindPropertyRelative("_key").stringValue;

		EditorGUI.BeginProperty(position, label, prop);
		
		position = EditorGUI.PrefixLabel(position, EditorGUIUtility.GetControlID(FocusType.Passive), label);
		
		int originalIndentLevel = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		
		position.width -= 32;
		
		string temp;

		if(isAddingString)
		{
			position.height = base.GetPropertyHeight(prop, label);

			currentCategory = EditorGUI.Popup(position, currentCategory, categories.ToArray());

			position.y += position.height;

			EditorGUI.BeginChangeCheck();
			
			temp = EditorGUI.TextField(position, newValue);
			
			if(EditorGUI.EndChangeCheck())
			{
				newValue = temp;
			}
		}
		else
		{
			temp =  keyword;

			if(temp == "")
				temp = "Empty String";

			Color originalColor = GUI.color;
			int index = keywords.IndexOf(temp);
			
			if(index < 0)
			{
				index = keywords.Count;
				keywords.Add(temp + " (Missing)");
				GUI.color = Color.red;
			}
			
			
			EditorGUI.BeginChangeCheck();
			

			index = EditorGUI.Popup(position, index, keywords.ToArray());

			temp = keywords[index];

			if(temp == "Empty String")
				temp = "";

			GUI.color = originalColor;
			
			if(EditorGUI.EndChangeCheck())
			{
				prop.FindPropertyRelative("_key").stringValue = temp;
			}
		}

        position.y += 1;
		position.x += position.width;
		position.width = 16;
		
		if(GUI.Button(position, olPlus, "Label"))
		{
			if (temp.Contains(" (Missing)"))
			{
				KeywordCategory tempCategory = (from c in config where c.name == (keyword.LastIndexOf('/') < 0 ? "" : keyword.Substring(0, keyword.LastIndexOf('/'))) select c).FirstOrDefault();

				if(tempCategory == null)
				{
					config.Add(new KeywordCategory((keyword.LastIndexOf('/') < 0 ? "" : keyword.Substring(0, keyword.LastIndexOf('/')))));
					config.Last().keywords.Add(keyword);
				}
				else
				{
					tempCategory.keywords.Add(keyword);
				}
				EditorUtility.SetDirty(scriptableConfig);
				PopulateLists();
			}
			else
			{
				if (isAddingString)
				{
					config[currentCategory].keywords.Add(newValue);
					EditorUtility.SetDirty(scriptableConfig);

					keywords.Add(config[currentCategory].name + (currentCategory == 0 ? "" : "/") + newValue);

					config[currentCategory].keywords.Sort();
					keywords.RemoveAt(0);
					keywords.Sort();
					keywords.Insert(0, "Empty String");

					SetReflectedFieldRecursively(prop, (Keyword)(config[currentCategory].name + (currentCategory == 0 ? "" : "/") + newValue));

					EditorUtility.SetDirty(prop.serializedObject.targetObject);
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
				if(EditorUtility.DisplayDialog("Remove string?", string.Format("Are you sure you want to remove {0} from the string list?", temp), "Yes", "No"))
				{
					keywords.Remove(temp);

					if(temp.Contains('/'))
						(from c in config where c.name == temp.Substring(0, temp.LastIndexOf('/')) select c.keywords).ToList().FirstOrDefault().Remove(keyword);
					else
						config[0].keywords.Remove(keyword);

					EditorUtility.SetDirty(Keywords.Config);
				}
			}
		}

		EditorGUI.indentLevel = originalIndentLevel;
		
		EditorGUI.EndProperty();
	}
}

[CustomPropertyDrawer(typeof(Keyword))]
public class KeywordDrawer : BaseKeywordDrawer
{

	public KeywordDrawer()
	{
		scriptableConfig = Keywords.Config;
		config = scriptableConfig.keyWordCategories;

		base.Init();
	}
}

}
