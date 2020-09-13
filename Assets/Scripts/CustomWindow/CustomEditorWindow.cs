using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

/// <summary>
/// 普通的编辑器窗口，可与其他编辑器窗口合并
/// </summary>
/// 
enum ExampleFlagsEnum
{
    None = 0, // Custom name for "Nothing" option
    A = 1 << 0,
    B = 1 << 1,
    AB = A | B, // Combination of two flags
    C = 1 << 2,
    All = ~0, // Custom name for "Everything" option
}

public enum OPTIONS
{
    CUBE = 0,
    SPHERE = 1,
    PLANE = 2
}

public class CustomEditorWindow : EditorWindow {

    bool groupEnabled;
    float myFloat = 1.23f;
    AnimBool IsShowExtra;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;
    bool showOption1 = true, showOption2 = false;
    Bounds bounds;
    string selectItem = "";
    bool[] pos = new bool[3] { true, true, true };
    ExampleFlagsEnum m_Flags;

    OPTIONS op;

    bool bFoldOut = true;
    string status = "Select a GameObject";

    int flags = 0;
    string[] options = new string[] { "CanJump", "CanShoot", "CanSwim" };

    // 增加一个打开窗口的MenuItem
    [MenuItem ("CustomWindowTest/默认窗口")]
    private static void Init () {

        var window = GetWindow<CustomEditorWindow> ("Default Window");
        window.Show ();
    }

    void OnEnable () {
        IsShowExtra = new AnimBool (true);    //创建一个可动选项
        IsShowExtra.speed = 5;
        IsShowExtra.valueChanged.AddListener (Repaint);
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    //绘制窗口
    private void OnGUI () {
        //设置一个选项卡
        drawTabs();

        //根据选项卡显示GUI
        if (showOption1) {
            showOptionContent1();
        } 
        else if (showOption2) {
            showOptionContent2();
        }
    }

    void drawTabs()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Toggle(showOption1, "Option1", EditorStyles.toolbarButton) != showOption1)
        {
            showOption1 = true;
            showOption2 = false;
        }
        if (GUILayout.Toggle(showOption2, "Option2", EditorStyles.toolbarButton) != showOption2)
        {
            showOption1 = false;
            showOption2 = true;
        }
        EditorGUILayout.EndHorizontal();
    }

    void showOptionContent1()
    {
        GUILayout.Label("Base Setting", EditorStyles.boldLabel);
        m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);

        //实现一个渐隐效果的Toggle
        IsShowExtra.target = EditorGUILayout.ToggleLeft("显示一个额外的区域", IsShowExtra.target);
        //Extra block that can be toggled on and off.
        if (EditorGUILayout.BeginFadeGroup(IsShowExtra.faded))
        {
            //各种Field
            EditorGUI.indentLevel++;   //加一层缩进
            EditorGUILayout.PrefixLabel("ColorField");   //PrefixLabel
            m_Color = EditorGUILayout.ColorField(m_Color);

            EditorGUILayout.PrefixLabel("TextField");
            m_String = EditorGUILayout.TextField(m_String);

            EditorGUILayout.PrefixLabel("EnumFlagsField");
            m_Flags = (ExampleFlagsEnum)EditorGUILayout.EnumFlagsField(m_Flags);

            EditorGUILayout.PrefixLabel("MaskField");
            flags = EditorGUILayout.MaskField("Player Flags", flags, options);

            // BoundsField
            bounds = EditorGUILayout.BoundsField("BoundsField", bounds);  //??不理解这个是干嘛的
            EditorGUI.indentLevel--;  //还原缩进

            op = (OPTIONS)EditorGUILayout.EnumPopup("Primitive to create:", op);
        }
        EditorGUILayout.EndFadeGroup();

        //toggle group
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Setting", groupEnabled);
        pos[0] = EditorGUILayout.Toggle("x", pos[0]);
        pos[1] = EditorGUILayout.Toggle("y", pos[1]);
        pos[2] = EditorGUILayout.Toggle("z", pos[2]);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        //DropdownButton用法
        if (EditorGUILayout.DropdownButton(new GUIContent("Click and Drop"), FocusType.Keyboard))
        {
            var alls = new string[4] { "A", "B", "C", "D" };
            GenericMenu _menu = new GenericMenu();
            foreach (var item in alls)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                //添加菜单
                _menu.AddItem(new GUIContent(item), item == selectItem, OnClickDropdownButton, item);
            }
            _menu.ShowAsContext();//显示菜单
        }


        //折叠栏
        bFoldOut = EditorGUILayout.Foldout(bFoldOut, status);
        if (bFoldOut)
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                    EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
                status = Selection.activeTransform.name;
            }

        if (!Selection.activeTransform)
        {
            status = "Select a GameObject";
            bFoldOut = false;
        }
    }

    void showOptionContent2()
    {

    }

    public void OnClickDropdownButton(object obj)
    {
        selectItem = (string)obj;
        Debug.Log("Item 2 Selected");
    }
}