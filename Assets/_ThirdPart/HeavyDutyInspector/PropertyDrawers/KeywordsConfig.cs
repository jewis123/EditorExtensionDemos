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

	public class KeywordsConfig : ScriptableObject
	{

		public List<KeywordCategory> keyWordCategories = new List<KeywordCategory>();

	}

	[System.Serializable]
	public class KeywordCategory : System.Object
	{
		public string name;

		[System.NonSerialized]
		public bool expanded;

		public List<string> keywords = new List<string>();

		public KeywordCategory()
		{
			name = "";
		}

		public KeywordCategory(string name)
		{
			this.name = name;
		}
	}

}
