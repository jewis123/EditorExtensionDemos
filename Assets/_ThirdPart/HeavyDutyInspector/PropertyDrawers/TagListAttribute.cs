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

	public class TagListAttribute : PropertyAttribute {
		
		public bool canDeleteFirstElement
		{
			get;
			private set;
		}

		/// <summary>
		/// Use with variables of type List<string>. Displays strings in a list using the tag drop down menu and adds the ability to delete tags from the list.
		/// </summary>
		/// <param name="canDeleteFirstElement">If set to <c>false</c> the first element in the list won't have the delete button.</param>
		public TagListAttribute(bool canDeleteFirstElement = true)
		{
			this.canDeleteFirstElement = canDeleteFirstElement;
		}
	}

}
