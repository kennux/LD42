using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct MinMaxResoruce
{
    public float max { get { return this._max; } }
    public float initial { get { return this._initial; } }

    [Header("Config")]
    [FormerlySerializedAs("max")]
    [SerializeField]
    private float _max;
    [FormerlySerializedAs("initial")]
    [SerializeField]
    private float _initial;

    public float value { get { return this._value; } set { this._value = Mathf.Clamp(value, 0, this._max); } }
    public float percentage { get { return this._value / this._max; } }

    public void Init()
    {
        this.value = this._initial;
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