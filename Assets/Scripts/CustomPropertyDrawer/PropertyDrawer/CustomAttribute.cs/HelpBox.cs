using System;
using UnityEngine;
using UnityEditor;

public enum HelpBoxType {
    None,
    Info,
    Warning,
    Error,
}

public sealed class HelpBoxAttribute : PropertyAttribute {
    public string Message;
    public HelpBoxType Type;

    public HelpBoxAttribute (string message, HelpBoxType type = HelpBoxType.None, int order = 0) {
        Message = message;
        Type = type;
        this.order = order; //PropertyAttribute.order 在多个DecoratorDrawer叠加时 设置调用次序
    }
}

public class HelpBox : MonoBehaviour {
    public int a = 0;
    [HelpBoxAttribute ("警告：填写下面数据时需要谨慎", HelpBoxType.Warning)]
    [Space (22)]
    public string text = "warn";
}