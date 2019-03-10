using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

/// <summary>
/// 普通的编辑器窗口，可与其他编辑器窗口合并
/// </summary>
public class CustomEditorWindow : EditorWindow {

    string WinTitle = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    //FadeGroup
    AnimBool IsShowExtra;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;

    bool showOption1 = true, showOption2 = false;

    // 增加一个打开窗口的MenuItem
    [MenuItem ("CustomWindowTest/默认窗口")]
    private static void Init () {

        var window = GetWindow<CustomEditorWindow> ("Default Window");
        window.Show ();
    }

    void OnEnable () {
        IsShowExtra = new AnimBool (true);
        IsShowExtra.speed = 5;
        IsShowExtra.valueChanged.AddListener (Repaint);
    }

    //绘制窗口
    private void OnGUI () {
        //设置一个选项卡
        EditorGUILayout.BeginHorizontal ();
        
        if (GUILayout.Toggle (showOption1, "Option1", EditorStyles.toolbarButton) != showOption1) {
            showOption1 = !showOption1;
            showOption2 = !showOption2;
        }
        if (GUILayout.Toggle (showOption2, "Option2", EditorStyles.toolbarButton) != showOption2) {
            showOption1 = !showOption1;
            showOption2 = !showOption2;
        }
        EditorGUILayout.EndHorizontal ();

        //根据选项卡显示GUI
        if (showOption1) {
            GUILayout.Label ("Base Setting", EditorStyles.boldLabel);
            WinTitle = EditorGUILayout.TextField ("Text Field", WinTitle);

            //实现一个渐隐效果的Toggle
            IsShowExtra.target = EditorGUILayout.ToggleLeft ("显示一个额外的区域", IsShowExtra.target);
            //Extra block that can be toggled on and off.
            if (EditorGUILayout.BeginFadeGroup (IsShowExtra.faded)) {
                EditorGUI.indentLevel++;   //加一层缩进
                EditorGUILayout.PrefixLabel ("Color");   //PrefixLabel
                m_Color = EditorGUILayout.ColorField (m_Color);
                EditorGUILayout.PrefixLabel ("Text");
                m_String = EditorGUILayout.TextField (m_String);
                EditorGUILayout.PrefixLabel ("Number");
                m_Number = EditorGUILayout.IntSlider (m_Number, 0, 10);
                EditorGUI.indentLevel--;  //还原缩进
            }
            EditorGUILayout.EndFadeGroup ();

            //设置一个Toggle区域
            groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Setting", groupEnabled);
            myBool = EditorGUILayout.Toggle ("Toggle", myBool);
            myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup ();
        } else if (showOption2) {

        }
    }
}