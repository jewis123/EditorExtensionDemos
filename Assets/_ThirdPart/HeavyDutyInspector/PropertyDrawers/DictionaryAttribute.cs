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

namespace HeavyDutyInspector
{

	public class DictionaryAttribute : PropertyAttribute {

		public string valuesListName
		{
			get;
			private set;
		}

		public KeywordsConfig keywordConfig
		{
			get;
			private set;
		}

		/// <summary>
		/// Displays two lists as a single dictionary and synchronizes them together.
		/// You need to implement ISerializationCallbackReceiver to create your actual dictionary when your asset is loaded, before its Awake function.
		/// </summary>
		/// <param name="valuesListName">Name of the list containing the values for the dictionary.</param>
		public DictionaryAttribute(string valuesListName)
		{
			keywordConfig = null;
			this.valuesListName = valuesListName;
		}

		/// <summary>
		/// Displays two lists as a single dictionary and synchronizes them together.
		/// This overload is for use when your list of Keys is a list of keywords.
		/// You need to implement ISerializationCallbackReceiver to create your actual dictionary when your asset is loaded, before its Awake function.
		/// </summary>
		/// <param name="valuesListName">Name of the list containing the values for the dictionary.</param>
		/// <param name="keywordsConfigFile">Path of your KeywordConfig file relative to a Resources folder.</param>
		public DictionaryAttribute(string valuesListName, string keywordsConfigFile)
		{
			keywordConfig = Resources.Load(keywordsConfigFile) as KeywordsConfig;
			this.valuesListName = valuesListName;
		}
	}	
}
	
