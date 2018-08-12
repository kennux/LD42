using UnityEngine;
using UnityTK;

public class ShipShieldSystem : ShipSystem
{
    public override ShipSystemType shipSystemType
    {
        get
        {
            return ShipSystemType.SHIELD;
        }
    }
    
    public float damageMitigation;

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        return 1;
    }

    protected override void UpdateSystem(float currentEfficiency)
    {
        Ship.instance.damageMitigation = this.damageMitigation * currentEfficiency;
    }
}