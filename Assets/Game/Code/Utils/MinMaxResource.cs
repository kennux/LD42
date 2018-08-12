using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct MinMaxResoruce
{
    public float max { get { return this._max; } }
    public float min { get { return this._min; } }
    public float initial { get { return this._initial; } }

    [Header("Config")]
    [FormerlySerializedAs("max")]
    [SerializeField]
    private float _max;
    [SerializeField]
    private float _min;
    [FormerlySerializedAs("initial")]
    [SerializeField]
    private float _initial;

    public float maxDelta { get { return _max - this._value; } }
    public float value { get { return this._value; } set { this._value = Mathf.Clamp(value, this._min, this._max); } }
    public float percentage { get { return (this._value - this._min) / (this._max - this._min); } }

    public void Init()
    {
        this.value = this._initial;
    }

    /// <summary>
    /// Returns a number between 1 to 0 representing the amount of drain that could be drained (removed, -) from this resource right now.
    /// </summary>
    public float GetDrainPercentage(float drain)
    {
        return Mathf.Min(_value, drain) / drain;
    }

    /// <summary>
    /// The current value
    /// </summary>
    [SerializeField]
    private float _value;

    public static implicit operator float(MinMaxResoruce res)
    {
        return res.value;
    }
}