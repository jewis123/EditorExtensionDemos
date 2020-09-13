using UnityEngine;

public class TimeAttribute : PropertyAttribute {
  public readonly bool DisplayHours;
  public TimeAttribute (bool displayHours = false) { 
    DisplayHours = displayHours;
  }
}

public class Clock:MonoBehaviour{
    [TimeAttribute(true)]
    public int seconds = 10000;

}