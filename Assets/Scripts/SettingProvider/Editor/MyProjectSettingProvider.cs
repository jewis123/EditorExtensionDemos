using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SettingProvider
{
    public class MyProjectSettingProvider: SettingsProvider
    {
        internal const string MenuPath = "ZZ/CustomProjectSetting";
        private SerializedObject m_CustomSettings;
        
        class Styles
        {
            public static GUIContent number = new GUIContent("My Number");
            public static GUIContent someString = new GUIContent("Some string");
        }
        
        public MyProjectSettingProvider(): base(MenuPath, SettingsScope.Project)
        {

        }
        
        
        [SettingsProvider]
        static UnityEditor.SettingsProvider CreateMyCustomSettingsProvider()
        {
            var provider = new MyProjectSettingProvider();
            provider.keywords = new string[] { "setting" };     //搜索栏关键字
            return provider;
        }
        
        /// <summary>
        /// 选中Setting项执行
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="rootElement"></param>
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            // This function is called when the user clicks on the MyCustom element in the Settings window.
            m_CustomSettings = MyProjectSetting.GetSerializedSettings();
            
            
            //如果用UIElement做界面展示， 在这里可以加载VisualElement
            // ...
        }
        
        /// <summary>
        /// 选别的Setting项执行
        /// </summary>
        public override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        /// <summary>
        /// 使用IMGUI绘制UI调用
        /// </summary>
        /// <param name="searchContext"></param>
        public override void OnGUI(string searchContext)
        {
            // Use IMGUI to display UI:
            EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_Number"), Styles.number);
            EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_SomeString"), Styles.someString);
            m_CustomSettings.ApplyModifiedPropertiesWithoutUndo();
        }
        
        
    }
}