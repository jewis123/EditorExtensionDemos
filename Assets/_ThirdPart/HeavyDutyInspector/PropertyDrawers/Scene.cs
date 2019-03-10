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

namespace HeavyDutyInspector
{

	[System.Serializable]
	public class Scene : System.Object
	{

		[SerializeField]
		[HideInInspector]
		private string _name;

		public Scene()
		{
			_name = "";
		}

		private Scene(string name)
		{
			_name = name;
		}

		public static implicit operator string(Scene scene)
		{
			return scene._name.Split('/').Last().Replace(".unity", "");
		}

		public static implicit operator Scene(string path)
		{
			return new Scene(path);
		}

		public string fullPath
		{
			get { return _name; }
		}
	}

}
