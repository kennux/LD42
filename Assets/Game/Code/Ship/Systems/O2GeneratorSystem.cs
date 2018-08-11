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
        Ship.instance.oxygen.value += this.generationRate * currentEfficiency * Time.deltaTime;
    }

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        float wouldGenerate = this.generationRate * predictedEfficiency * Time.deltaTime;
        float willGenerate = Mathf.Min(wouldGenerate, Ship.instance.oxygen.maxDelta);

        return willGenerate / wouldGenerate;
    }
}