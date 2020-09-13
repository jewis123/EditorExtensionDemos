using UnityEngine;

public class Health : MonoBehaviour {
    [RangeAttribute(0,2)]
    public float health;
}

public class RangeAttribute : PropertyAttribute
{
    public float min;
    public float max;

    public RangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}