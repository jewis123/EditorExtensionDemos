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
using System.Collections;
using System.Collections.Generic;

namespace HeavyDutyInspector
{

	[CustomPropertyDrawer(typeof(ChangeCheckCallbackAttribute))]
	public class ChangeCheckCallbackDrawer : IllogikaDrawer {
			
		ChangeCheckCallbackAttribute changeCheckCallbackAttribute { get { return ((ChangeCheckCallbackAttribute)attribute); } }
		
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
	       return base.GetPropertyHeight(prop, label);
	    }
		
		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, prop);

			EditorGUI.BeginChangeCheck();

			EditorGUI.PropertyField(position, prop);

			if(EditorGUI.EndChangeCheck())
			{
				try{
					(prop.serializedObject.targetObject as MonoBehaviour).StartCoroutine(WaitForCallback(prop));
				}
				catch{
					Debug.LogError("ChangeCheckCallback can only work with MonoBehaviours");
				}
			}

			EditorGUI.EndProperty();
		}

		IEnumerator WaitForCallback(SerializedProperty prop)
		{
			yield return null;
			foreach(Object obj in prop.serializedObject.targetObjects)
			{
				MonoBehaviour go = obj as MonoBehaviour;
				if (go != null)
				{
					CallMethod(prop, go, changeCheckCallbackAttribute.callback);
				}
				else
				{
					ScriptableObject so = obj as ScriptableObject;
					if(so != null)
					{
						CallMethod(prop, so, changeCheckCallbackAttribute.callback);
					}
				}
			}
		}
	}

}
