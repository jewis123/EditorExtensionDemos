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

	public class ImageButtonAttribute : PropertyAttribute {

		public string imagePath
		{
			get;
			private set;
		}
		
		public string buttonFunction
		{
			get;
			private set;
		}
		
		public bool hideVariable
		{
			get;
			private set;
		}
		
		/// <summary>
		/// Displays a button before the affected variable. In versions of Unity older than 4.3, the variable can only be displayed if it is of type bool, int, float, string, Color, Object reference, Rect, Vector2 or Vector3. Other variable types will have the variable hidden by default. In Unity 4.3 or higher, variables of any type can be displayed.
		/// </summary>
		/// <param name="buttonText">Text displayed on the button.</param>
		/// <param name="buttonFunction">The name of the function to be called</param>
		/// <param name="hideVariable">If set to <c>true</c> hides the variable.</param>
		public ImageButtonAttribute(string imagePath, string buttonFunction, bool hideVariable = false)
		{
			if(imagePath.ToLower().Substring(0, 7).Equals("assets/"))
				imagePath = imagePath.Substring(7);

			this.imagePath = imagePath;
			this.buttonFunction = buttonFunction;
			this.hideVariable = hideVariable;
		}
	}

}
