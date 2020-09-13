using HeavyDutyInspector;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    [AssetPathAttribute (typeof (Health), PathOptions.RelativeToAssets)]
    public string AssetPathAttribute = "Assets/_Prefabs/CustomPropertyDrawer";

    [Background (ColorEnum.Cyan)]
    public string BackgroundAttr = "This Attr Has Color!";

    [ButtonAttribute ("function", "SO", true)]
    public string s;

    [Dictionary("dic")]
    public Dictionary<string, int> dic = new Dictionary<string, int>();
    
    public void SO () {
        Debug.Log ("SS");
    }

}