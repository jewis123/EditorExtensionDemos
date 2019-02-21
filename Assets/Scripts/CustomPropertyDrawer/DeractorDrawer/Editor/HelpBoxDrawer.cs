using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer( typeof( HelpBoxAttribute ) )]
public sealed class HelpBoxDrawer : DecoratorDrawer
{
    private HelpBoxAttribute HelpBoxAttribute { get { return attribute as HelpBoxAttribute; } }

    public override void OnGUI( Rect position )
    {
        var helpBoxPosition = EditorGUI.IndentedRect( position );
        helpBoxPosition.height = GetHelpBoxHeight();
        EditorGUI.HelpBox( helpBoxPosition, HelpBoxAttribute.Message, GetMessageType( HelpBoxAttribute.Type ) );
    }
    
    public override float GetHeight()
    {
        return GetHelpBoxHeight();
    }

    public MessageType GetMessageType( HelpBoxType type )
    {
        switch ( type )
        {
            case HelpBoxType.Error:     return MessageType.Error;
            case HelpBoxType.Info:      return MessageType.Info;
            case HelpBoxType.None:      return MessageType.None;
            case HelpBoxType.Warning:   return MessageType.Warning;
        }
        return 0;
    }

    public float GetHelpBoxHeight()
    {
        var style   = new GUIStyle( "HelpBox" );
        var content = new GUIContent( HelpBoxAttribute.Message );
        return Mathf.Max( style.CalcHeight( content, Screen.width - ( HelpBoxAttribute.Type != HelpBoxType.None ? 53 : 21) ), 40);
    }
}