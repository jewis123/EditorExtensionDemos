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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace HeavyDutyInspector
{

	public enum ColorEnum
	{
		Black,
		Blue,
		Cyan,
		Gray,
		Green,
		Grey,
		Magenta,
		Red,
		White,
		Yellow,
	}

	public static class ColorEx
	{

		public static Color GetColorByEnum(ColorEnum color)
		{
			switch (color)
			{
				case ColorEnum.Black:
					return Color.black;
				case ColorEnum.Blue:
					return Color.blue;
				case ColorEnum.Cyan:
					return Color.cyan;
				case ColorEnum.Gray:
					return Color.gray;
				case ColorEnum.Green:
					return Color.green;
				case ColorEnum.Grey:
					return Color.grey;
				case ColorEnum.Magenta:
					return Color.magenta;
				case ColorEnum.Red:
					return Color.red;
				case ColorEnum.White:
					return Color.white;
				case ColorEnum.Yellow:
					return Color.yellow;
				default:
					return Color.clear;
			}
		}

	}

}
