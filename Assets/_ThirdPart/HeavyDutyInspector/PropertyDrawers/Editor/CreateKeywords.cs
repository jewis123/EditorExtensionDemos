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

namespace HeavyDutyInspector
{

	public static class CreateKeywords
	{
		[MenuItem("Assets/ScriptableObjects/Create New Keywords")]
		public static void CreateKeywordsConfig()
		{
			KeywordsConfig config = ScriptableObject.CreateInstance<KeywordsConfig>();

			if (!System.IO.Directory.Exists(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets/Resources/Config/")))
				System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets/Resources/Config/"));

			AssetDatabase.CreateAsset(config, "Assets/Resources/Config/KeywordsConfig.asset");
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();
			Selection.activeObject = config;
		}
	}

}
