using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using System.Reflection;
using System;

public class ReflectionBuiltInWindow : EditorWindow {

    [MenuItem("CustomWindow/ReflectionBuiltInWindow")]
    private static void ShowWindow() {
        var window = GetWindow<ReflectionBuiltInWindow>();
        window.titleContent = new GUIContent("ReflectionBuiltInWindow");
        window.Show();
    }


    private void OnGUI() {
        if (GUILayout.Button("打开内置编辑器窗口")){
            Assembly assembly = Assembly.Load("UnityEditor");     //程序集名和Type名都能在代码顶部注释看到
            Type type = assembly.GetType("UnityEditor.ProfilerWindow");
            Debug.Log(type);
            MethodInfo methodInfo=type.GetMethod("ShowProfilerWindow", BindingFlags.Static | BindingFlags.NonPublic);
            Debug.Log(methodInfo);
            methodInfo?.Invoke(null,null);
        }
        else if(GUILayout.Button("打开Package编辑器窗口")){
            Assembly assembly = Assembly.Load("Unity.2D.TileMap.Editor");
            Type type = assembly.GetType("UnityEditor.Tilemaps.GridPaintPaletteWindow");
            // Debug.Log(type);
            // MethodInfo[] methods = type.GetMethods();
            // foreach (var item in methods)
            // {
            //     Debug.Log(item.ToString());
            // }
            MethodInfo methodInfo=type.GetMethod("OpenTilemapPalette", BindingFlags.Public | BindingFlags.Static);
            Debug.Log(methodInfo);
            methodInfo?.Invoke(null,null);
        }
    }
}
