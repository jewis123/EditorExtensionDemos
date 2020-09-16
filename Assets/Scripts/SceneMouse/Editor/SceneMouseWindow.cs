using UnityEditor;
using UnityEngine;

public class SceneMouseWindow : EditorWindow
{
    [MenuItem("CustomWindow/Scene Mouse")]
    public static void ShowWindow()
    {
        var window = GetWindow<SceneMouseWindow>();
        window.Show();
    }

    void OnFocus()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
        Repaint();
    }

    void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        // 当前屏幕坐标，左上角是（0，0）右下角（camera.pixelWidth，camera.pixelHeight）
        Vector2 mousePosition = Event.current.mousePosition;


        // 转换成摄像机可接受的屏幕坐标，左下角是（0，0，0）右上角是（camera.pixelWidth，camera.pixelHeight，0）
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y;

        // 近平面往里一些，才能看得到摄像机里的位置
        Vector3 fakePoint = mousePosition;
        fakePoint.z = 20;
        Vector3 point = sceneView.camera.ScreenToWorldPoint(fakePoint);

        Handles.SphereHandleCap(0, point, Quaternion.identity, 2, EventType.Repaint);

        // 刷新界面，才能让球一直跟随
        sceneView.Repaint();
        HandleUtility.Repaint();
    }
}