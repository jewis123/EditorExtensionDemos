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

	public class BackgroundAttribute : PropertyAttribute {

		public Color color
		{
			get;
			private set;
		}

		/// <summary>
		/// Add a solid color background to the variable it is applied to.
		/// </summary>
		/// <param name="color">Color.</param>
		public BackgroundAttribute(ColorEnum color)
		{
			this.color = ColorEx.GetColorByEnum(color);

			// Always display last to make sure it is applied to the variable, not another DecoratorDrawer
			order = int.MaxValue;
		}

		/// <summary>
		/// Add a solid color background to the variable it is applied to.
		/// </summary>
		/// <param name="r">The color's red component.</param>
		/// <param name="g">The color's  green component.</param>
		/// <param name="b">The color's  blue component.</param>
		public BackgroundAttribute(float r, float g, float b)
		{
			this.color = new Color(r, g, b);
		}
	}
	
}
	
