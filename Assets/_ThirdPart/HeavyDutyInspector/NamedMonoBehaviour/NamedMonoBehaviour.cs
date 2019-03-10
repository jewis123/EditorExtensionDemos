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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using HeavyDutyInspector;

[System.Serializable]
public abstract class NamedMonoBehaviour : MonoBehaviour {

	public NamedMonoBehaviour() : base()
	{
		typeName = GetType().ToString();
	}

#pragma warning disable 414
	[SerializeField]
	[HideInInspector]
	private string typeName;
#pragma warning restore 414
	
	[NMBName]
	public string	scriptName;

	[NMBColor]
	public Color	scriptNameColor = Color.white;

	protected void InitDictionary<T, U>(List<T> keys, List<U> values, Dictionary<T, U> dictionary)
	{
		try
		{
			dictionary = new Dictionary<T, U>();
			for(int i = 0; i < keys.Count; ++i)
			{
				if(!dictionary.ContainsKey(keys[i]))
					dictionary.Add(keys[i], values[i]);
			}
		}
		catch { }
	} 
}
