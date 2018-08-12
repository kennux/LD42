using UnityEngine;
using UnityTK;

public class EnergyGeneratorSystem : ShipSystem
{
    public override ShipSystemType shipSystemType
    {
        get
        {
            return ShipSystemType.POWER_GENERATOR;
        }
    }

    /// <summary>
    /// The generation rate in units / s
    /// </summary>
    public float generationRate = 0.35f;

    protected override void UpdateSystem(float currentEfficiency)
    {
        Ship.instance.energy.value += this.generationRate * currentEfficiency * Time.fixedDeltaTime;
    }

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        float wouldGenerate = this.generationRate * predictedEfficiency * Time.fixedDeltaTime;
        float willGenerate = Mathf.Min(wouldGenerate, Ship.instance.energy.maxDelta);

        return willGenerate / wouldGenerate;
    }
}