using UnityEngine;
using UnityEditor;

public class WrappedWindow : EditorWindow {

    // The position of the window
    public Rect windowRect1 = new Rect(100, 100, 200, 200);
    static bool window1 = true;

    // Scroll position
    public Vector2 scrollPos = Vector2.zero;

    [MenuItem("CustomWindow/Wrapped Window With Scrollview")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(WrappedWindow));
        window1 = true;
    }

    // Add menu item to show this demo.
    [MenuItem("CustomWindow/Wrapped Window")]
    static void Init1()
    {
        EditorWindow.GetWindow(typeof(WrappedWindow));
        window1 = false;
    }       
    void OnGUI()
    {
        if (window1)
            DrawWindowWithScrollView();
        else
            DrawWindow();
    }

    void DrawWindowWithScrollView(){
        // Set up a scroll view
        scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos, new Rect(0, 0, 1000, 1000));

        // Same code as before - make a window. Only now, it's INSIDE the scrollview
        BeginWindows();
        windowRect1 = GUILayout.Window(1, windowRect1, DoWindow, "Hi There");
        EndWindows();
        // Close the scroll view
        GUI.EndScrollView();
    }

    void DrawWindow(){
        GUILayout.BeginVertical();
        for (int i = 0; i < 10; i++)
        {
            GUILayout.Label("UPPER AREA");
        }
        Rect rect = GUILayoutUtility.GetLastRect();
        BeginWindows();
        // All GUI.Window or GUILayout.Window must come inside here
        GUILayout.Window(1, new Rect(rect.x, rect.yMax + 4 , 200, 200), DoWindow, "Hi There");
        EndWindows();
        GUILayout.EndVertical();
    }

    void DoWindow(int unusedWindowID)
    {
        GUILayout.Button("Hi");
        // GUI.DragWindow();
    }
 
    
}