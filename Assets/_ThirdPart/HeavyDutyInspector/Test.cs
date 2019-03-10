using System.Collections;
using System.Collections.Generic;
using HeavyDutyInspector;
using UnityEngine;

public class Test : MonoBehaviour {
    [AssetPathAttribute (typeof (Health), PathOptions.RelativeToAssets)]
    public string AssetPathAttribute = "Assets/_Prefabs/CustomPropertyDrawer";

    [Background (ColorEnum.Cyan)]
    public string BackgroundAttr = "This Attr Has Color!";

    [ButtonAttribute ("function", "SO", true)]
    public string s;
    public void SO () {
        Debug.Log ("SS");
    }

}