using UnityEngine;
using UnityTK;

public class ThrusterSystem : ShipSystem
{
    public override ShipSystemType shipSystemType
    {
        get
        {
            return isCockpit ? ShipSystemType.COCKPIT : ShipSystemType.THRUSTER;
        }
    }

    public bool isCockpit = false;

    /// <summary>
    /// The acceleration rate in units / s
    /// </summary>
    public float acceleration = 0.35f;

    protected override void UpdateSystem(float currentEfficiency)
    {
        Ship.instance.velocity.value += this.acceleration * currentEfficiency * Time.deltaTime;
    }

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        float wouldGenerate = this.acceleration * predictedEfficiency * Time.deltaTime;
        float willGenerate = Mathf.Min(wouldGenerate, Ship.instance.velocity.maxDelta);

        return willGenerate / wouldGenerate;
    }
}