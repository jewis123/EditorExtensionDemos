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
using System;
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	public class ChangeCheckCallbackAttribute : PropertyAttribute {

		public string callback
		{
			get;
			private set;
		}

		/// <summary>
		/// Calls a function in your script when the value of the variable changes.
		/// </summary>
		/// <param name="callbackName">The name of the function to call.</param>
		public ChangeCheckCallbackAttribute(string callbackName)
		{
			this.callback = callbackName;
		}
	}

}
