using UnityEngine;
using UnityTK;

public class O2GeneratorSystem : ShipSystem
{
    public override ShipSystemType shipSystemType
    {
        get
        {
            return ShipSystemType.O2_GENERATOR;
        }
    }

    /// <summary>
    /// The generation rate in units / s
    /// </summary>
    public float generationRate = 0.35f;

    protected override void UpdateSystem(float currentEfficiency)
    {
        Ship.instance.oxygen.value += this.generationRate * currentEfficiency;
    }

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        float wouldGenerate = this.generationRate * predictedEfficiency;
        float willGenerate = Mathf.Min(wouldGenerate, Ship.instance.oxygen.max - Ship.instance.oxygen.value);

        return willGenerate / wouldGenerate;
    }
}