using UnityEditor;
using UnityEngine;

/// <summary>
/// 弹窗，浮动窗口，不可以和其他窗口合并
/// </summary>
public class PopUpWindow : EditorWindow {
    [MenuItem ("CustomWindowTest/弹窗")]
    static void Init () {
        //获取指定类型窗口
        var window = GetWindow<PopUpWindow> ();

        //设置弹窗位置
        window.position = new Rect (Screen.width / 2, Screen.height / 2, 250, 150);

        //显示弹窗
        window.ShowPopup ();
    }

    void OnGUI () {
        EditorGUILayout.LabelField ("This is an example of EditorWindow.ShowPopup", EditorStyles.wordWrappedLabel);
        GUILayout.Space (70);
        if (GUILayout.Button ("Agree!"))
            this.Close ();
    }

    private void Awake () {
        Debug.Log ("Custom Window Awake");
    }
    private void OnEnable () {
        Debug.Log ("Custom Window OnEnable");
    }
    private void OnDisable () {
        Debug.Log ("Custom Window OnDisable");
    }
}

/// <summary>
/// 失去焦点后自动关闭窗口，浮动窗口
/// </summary>
public class DropDownWindow : EditorWindow {
    [MenuItem ("CustomWindowTest/对焦弹窗")]
    static void Initialize () {
        var window = GetWindow<DropDownWindow> (true, "DropDown Window"); //GetWindow参数列表中utility传入true是就是浮动窗口
        window.ShowAsDropDown (new Rect (10, 10, 200, 150), new Vector2 (500, 250));
    }
}

/// <summary>
/// 辅助窗口，避免小窗口混乱
/// TODO:没明白具体是怎么实现窗口重用的
/// </summary>
public class AuxWindow : EditorWindow {
    [MenuItem ("CustomWindowTest/辅助窗口")]
    static void Init () {
        var window = GetWindow<AuxWindow> ("AUX Window");
        window.ShowAuxWindow ();
    }
}

public class Notification : EditorWindow {
    [MenuItem ("CustomWindowTest/通知信息")]
    static void Init () {
        Notification.GetWindow (typeof (Notification));
    }

    private void OnGUI () {
        if (GUILayout.Button ("显示通知")) {
            this.ShowNotification (new GUIContent ("这是个Notification"));
        }
        if(GUILayout.Button("对焦Defualt Window")){
            EditorWindow.FocusWindowIfItsOpen(typeof(CustomEditorWindow));  //实现焦点窗口的切换
        }
        
    }
}