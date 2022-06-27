using UnityEditor;
using UnityEngine;

namespace SettingProvider
{
    // Create a new type of Settings Asset.
    class MyProjectSetting : ScriptableObject
    {
        public const string k_MyCustomSettingsPath = "Assets/Scripts/SettingProvider/Editor/MyCustomSettings.asset";

        [SerializeField]
        private int m_Number;

        [SerializeField]
        private string m_SomeString;

        internal static MyProjectSetting GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<MyProjectSetting>(k_MyCustomSettingsPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<MyProjectSetting>();
                settings.m_Number = 42;
                settings.m_SomeString = "The answer to the universe";
                AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
}
