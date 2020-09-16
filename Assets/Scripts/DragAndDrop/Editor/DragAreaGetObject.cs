using UnityEngine;
using System.Collections;
using UnityEditor;

public class DragAreaGetObject : EditorWindow
{
    object dragData = "dragData";
    Vector2 offset;
    Color col = new Color(1, 0, 0, 0.6f);
    Rect rect1 = new Rect(20, 10, 100, 20);
    Rect rect2 = new Rect(20, 60, 100, 20);
    Rect drect;
    bool isDraging;
    int cid;

    [MenuItem("CustomWindow/drag and drop")]
    private static void Init()
    {

        var window = GetWindow<DragAreaGetObject>("Default Window");
        window.Show();
    }

    private void OnGUI()
    {
        GUI.Box(rect1, "rect1");
        GUI.Box(rect2, "rect2");

        Event e = Event.current;
        cid = GUIUtility.GetControlID(FocusType.Passive);
        switch (e.GetTypeForControl(cid))
        {
            case EventType.MouseDown:
                if (rect1.Contains(e.mousePosition))
                    GUIUtility.hotControl = cid;
                break;
            case EventType.MouseUp:
                if (GUIUtility.hotControl == cid)
                    GUIUtility.hotControl = 0;
                break;
            case EventType.MouseDrag:
                Debug.Log("MouseDrag");
                if (GUIUtility.hotControl == cid && rect1.Contains(e.mousePosition))
                {
                    DragAndDrop.PrepareStartDrag();
                    //DragAndDrop.objectReferences = new Object[] { };
                    DragAndDrop.SetGenericData("dragflag", dragData);
                    DragAndDrop.StartDrag("dragtitle");
                    offset = e.mousePosition - rect1.position;
                    drect = rect1;
                    isDraging = true;
                    e.Use();
                }
                break;
            case EventType.DragUpdated:
                Debug.Log("DragUpdated");
                drect.position = e.mousePosition - offset;
                if (rect2.Contains(e.mousePosition))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                    drect = rect2;
                }

                e.Use();
                break;
            case EventType.DragPerform:
                Debug.Log("DragPerform");
                DragAndDrop.AcceptDrag();
                Debug.Log("DragPerform : " + DragAndDrop.GetGenericData("dragflag"));
                e.Use();
                break;
            case EventType.DragExited:
                Debug.Log("DragExited");
                isDraging = false;
                if (GUIUtility.hotControl == cid)
                    GUIUtility.hotControl = 0;
                e.Use();
                break;
        }

        if (isDraging)
        {
            EditorGUI.DrawRect(drect, col);
        }
    }
}