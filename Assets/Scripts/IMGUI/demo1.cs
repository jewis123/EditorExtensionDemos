/* 闪烁按钮示例 */
using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{
    //Unity 的 IMGUI 控件使用一个名为 OnGUI() 的特殊函数。只要启用包含脚本，就会在每帧调用 OnGUI() 函数，就像 Update() 函数一样。
    void OnGUI()
    {
        if (Time.time % 2 < 1)
        {
            if (GUI.Button(new Rect(10, 10, 200, 20), "Meet the flashing button"))
            {
                print("You clicked me!");
            }
        }
    }
}