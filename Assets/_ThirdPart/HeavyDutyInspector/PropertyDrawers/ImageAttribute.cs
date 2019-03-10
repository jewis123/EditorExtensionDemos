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
using System;
using UnityEngine;
using System.Collections;

namespace HeavyDutyInspector
{

	public enum Alignment {
		Left,
		Center,
		Right
	}

	[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class ImageAttribute : PropertyAttribute {
		
		public string imagePath
		{
			get;
			private set;
		}

		public Alignment alignment
		{
			get;
			private set;
		}

		/// <summary>
		/// Adds the specified image in the inspector before the variable.
		/// </summary>
		/// <param name="imagePath">Path to the image. The path is relative to the project's Asset folder.</param>
		/// <param name="alignment">The image's alignment, either Left, Center or Right.</param>
		public ImageAttribute(string imagePath, Alignment alignment = Alignment.Center, int order = 0)
		{
			if(imagePath.ToLower().Substring(0, 7).Equals("assets/"))
				imagePath = imagePath.Substring(7);

			this.imagePath = imagePath;
			this.alignment = alignment;
			this.order = order;
		}
	}
}

	
