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

namespace HeavyDutyInspector
{
	[System.Serializable]
	[Obsolete("Unity now support serialization of all primitive types.")]
	public class charS : System.Object
	{
		[SerializeField]
		private byte[] _bytes;

		public charS()
		{
			_bytes = BitConverter.GetBytes('\0');
		}

		public charS(char value)
		{
			_bytes = BitConverter.GetBytes(value);
		}

		public static implicit operator char(charS value)
		{
			try
			{
				return BitConverter.ToChar(value._bytes, 0);
			}
			catch { return '\0'; }
		}

		public static implicit operator charS(char value)
		{
			return new charS(value);
		}

		public static implicit operator string(charS value)
		{
			return new String(new char[] { value });
		}
	}

	[System.Serializable]
	[Obsolete("Unity now support serialization of all primitive types.")]
	public class UInt16S : System.Object
	{

		[SerializeField]
		private byte[] _bytes;

		[SerializeField]
		private bool _endianness;

		public UInt16S()
		{
			_bytes = BitConverter.GetBytes((UInt16)0);
			_endianness = BitConverter.IsLittleEndian;
		}

		public UInt16S(UInt16 value)
		{
			_bytes = BitConverter.GetBytes(value);
			_endianness = BitConverter.IsLittleEndian;
		}

		public static implicit operator UInt16(UInt16S value)
		{
			try 
			{
				return BitConverter.ToUInt16(BitConverter.IsLittleEndian == value._endianness ? value._bytes : value._bytes.Reverse().ToArray(), 0);
			}
			catch { return (UInt16)0; }
		}

		public static implicit operator UInt16S(UInt16 value)
		{
			return new UInt16S(value);
		}
	}

	[System.Serializable]
	[Obsolete("Unity now support serialization of all primitive types.")]
	public class UInt32S : System.Object
	{

		[SerializeField]
		private byte[] _bytes;

		[SerializeField]
		private bool _endianness;

		public UInt32S()
		{
			_bytes = BitConverter.GetBytes(0U);
			_endianness = BitConverter.IsLittleEndian;
		}

		public UInt32S(UInt32 value)
		{
			_bytes = BitConverter.GetBytes(value);
			_endianness = BitConverter.IsLittleEndian;
		}

		public static implicit operator UInt32(UInt32S value)
		{
			try
			{
				return BitConverter.ToUInt32(BitConverter.IsLittleEndian == value._endianness ? value._bytes : value._bytes.Reverse().ToArray(), 0);
			}
			catch { return 0U; }
		}

		public static implicit operator UInt32S(UInt32 value)
		{
			return new UInt32S(value);
		}
	}

	[System.Serializable]
	[Obsolete("Unity now support serialization of all primitive types.")]
	public class UInt64S : System.Object
	{

		[SerializeField]
		private byte[] _bytes;

		[SerializeField]
		private bool _endianness;

		public UInt64S()
		{
			_bytes = BitConverter.GetBytes(0UL);
			_endianness = BitConverter.IsLittleEndian;
		}

		public UInt64S(UInt64 value)
		{
			_bytes = BitConverter.GetBytes(value);
			_endianness = BitConverter.IsLittleEndian;
		}

		public static implicit operator UInt64(UInt64S value)
		{
			try
			{
				return BitConverter.ToUInt64(BitConverter.IsLittleEndian == value._endianness ? value._bytes : value._bytes.Reverse().ToArray(), 0);
			}
			catch { return 0UL; }
		}

		public static implicit operator UInt64S(UInt64 value)
		{
			return new UInt64S(value);
		}
	}

	[System.Serializable]
	[Obsolete("Unity now support serialization of all primitive types.")]
	public class Int16S : System.Object
	{

		[SerializeField]
		private byte[] _bytes;

		[SerializeField]
		private bool _endianness;

		public Int16S()
		{
			_bytes = BitConverter.GetBytes((Int16)0);
			_endianness = BitConverter.IsLittleEndian;
		}

		public Int16S(Int16 value)
		{
			_bytes = BitConverter.GetBytes(value);
			_endianness = BitConverter.IsLittleEndian;
		}

		public static implicit operator Int16(Int16S value)
		{
			try
			{
				return BitConverter.ToInt16(BitConverter.IsLittleEndian == value._endianness ? value._bytes : value._bytes.Reverse().ToArray(), 0);
			}
			catch { return (Int16)0; }
		}

		public static implicit operator Int16S(Int16 value)
		{
			return new Int16S(value);
		}
	}

	[System.Serializable]
	[Obsolete("Unity now support serialization of all primitive types.")]
	public class Int64S : System.Object
	{

		[SerializeField]
		private byte[] _bytes;

		[SerializeField]
		private bool _endianness;

		public Int64S()
		{
			_bytes = BitConverter.GetBytes(0L);
			_endianness = BitConverter.IsLittleEndian;
		}

		public Int64S(Int64 value)
		{
			_bytes = BitConverter.GetBytes(value);
			_endianness = BitConverter.IsLittleEndian;
		}

		public static implicit operator Int64(Int64S value)
		{
			try
			{
				return BitConverter.ToInt64(BitConverter.IsLittleEndian == value._endianness ? value._bytes : value._bytes.Reverse().ToArray(), 0);
			}
			catch { return 0L; }
		}

		public static implicit operator Int64S(Int64 value)
		{
			return new Int64S(value);
		}
	}

#if UNITY_EDITOR

	public static partial class EditorGUIEx
	{
		public static char CharField(Rect position, char value)
		{
			return UnityEditor.EditorGUI.TextField(position, new String(new char[] { value })).FirstOrDefault();
		}

		public static char CharField(Rect position, string label, char value)
		{
			return UnityEditor.EditorGUI.TextField(position, label, new String(new char[] { value })).FirstOrDefault();
		}

		public static Int16 IntField(Rect position, Int16 value)
		{
			return (Int16)Mathf.Clamp(UnityEditor.EditorGUI.IntField(position, (int)value), Int16.MinValue, Int16.MaxValue);
		}

		public static Int16 IntField(Rect position, string label, Int16 value)
		{
			return (Int16)Mathf.Clamp(UnityEditor.EditorGUI.IntField(position, label, (int)value), Int16.MinValue, Int16.MaxValue);
		}

		public static Int64 IntField(Rect position, Int64 value)
		{
			string s = value.ToString();

			s = UnityEditor.EditorGUI.TextField(position, s);

			try
			{
				return Int64.Parse(s);
			}
			catch
			{
				if(string.IsNullOrEmpty(s))
				{
					return 0L;
				}
				return value;
			}
		}

		public static Int64 IntField(Rect position, string label, Int64 value)
		{
			string s = value.ToString();

			s = UnityEditor.EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = UnityEditor.EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			try
			{
				return Int64.Parse(s) + (Int64)tempInt;
			}
			catch
			{
				if(string.IsNullOrEmpty(s))
				{
					return (Int64)tempInt;
				}
				return value;
			}
		}

		public static UInt16 IntField(Rect position, UInt16 value)
		{
			return (UInt16)Mathf.Clamp(UnityEditor.EditorGUI.IntField(position, (int)value), UInt16.MinValue, UInt16.MaxValue);
		}

		public static UInt16 IntField(Rect position, string label, UInt16 value)
		{
			return (UInt16)Mathf.Clamp(UnityEditor.EditorGUI.IntField(position, label, (int)value), UInt16.MinValue, UInt16.MaxValue);
		}

		public static UInt32 IntField(Rect position, UInt32 value)
		{
			string s = value.ToString();

			s = UnityEditor.EditorGUI.TextField(position, s);

			try
			{
				return UInt32.Parse(s);
			}
			catch
			{
				if(string.IsNullOrEmpty(s))
				{
					return 0U;
				}
				return value;
			}
		}

		public static UInt32 IntField(Rect position, string label, UInt32 value)
		{
			string s = value.ToString();

			s = UnityEditor.EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = UnityEditor.EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			try
			{
				return UInt32.Parse(s) + (UInt32)tempInt;
			}
			catch
			{
				if(string.IsNullOrEmpty(s))
				{
					return (UInt32)tempInt;
				}
				return value;
			}
		}

		public static UInt64 IntField(Rect position, UInt64 value)
		{
			string s = value.ToString();

			s = UnityEditor.EditorGUI.TextField(position, s);

			try
			{
				return UInt64.Parse(s);
			}
			catch
			{
				if(string.IsNullOrEmpty(s))
				{
					return 0UL;
				}
				return value;
			}
		}

		public static UInt64 IntField(Rect position, string label, UInt64 value)
		{
			string s = value.ToString();

			s = UnityEditor.EditorGUI.TextField(position, label, s);

			GUI.color = Color.clear;
			int tempInt = UnityEditor.EditorGUI.IntField(position, label, 0);
			GUI.color = Color.white;

			try
			{
				return UInt64.Parse(s) + (UInt64)tempInt;
			}
			catch
			{
				if(string.IsNullOrEmpty(s))
				{
					return (UInt64)tempInt;
				}
				return value;
			}
		}
	}

	public static partial class EditorGUILayoutEx
	{
		public static char CharField(char value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.CharField(position, value);
		}

		public static char CharField(string label, char value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.CharField(position, label, value);
		}

		public static Int16 IntField(Int16 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, value);
		}

		public static Int16 IntField(string label, Int16 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, label, value);
		}

		public static Int64 IntField(Int64 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, value);
		}

		public static Int64 IntField(string label, Int64 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, label, value);
		}

		public static UInt16 IntField(UInt16 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, value);
		}

		public static UInt16 IntField(string label, UInt16 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, label, value);
		}

		public static UInt32 IntField(UInt32 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, value);
		}

		public static UInt32 IntField(string label, UInt32 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, label, value);
		}

		public static UInt64 IntField(UInt64 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, value);
		}

		public static UInt64 IntField(string label, UInt64 value)
		{
			UnityEditor.EditorGUILayout.LabelField("");
			Rect position = GUILayoutUtility.GetLastRect();

			position.height = UnityEditor.EditorGUIUtility.singleLineHeight;

			return EditorGUIEx.IntField(position, label, value);
		}
	}

#endif

}
