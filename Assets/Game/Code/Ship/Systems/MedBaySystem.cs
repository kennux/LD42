using UnityEngine;
using UnityTK;
using System.Collections.Generic;

public class MedBaySystem : ShipSystem
{
    public override ShipSystemType shipSystemType
    {
        get
        {
            return ShipSystemType.MEDBAY;
        }
    }

    public float healPerSecond = 5;

    [Header("Debug")]
    [SerializeField]
    private List<Crewman> crewmanInRange = new List<Crewman>();

    public void OnTriggerEnter(Collider other)
    {
        var cm = other.GetComponentInParent<Crewman>();
        this.crewmanInRange.Add(cm);
    }

    public void OnTriggerExit(Collider other)
    {
        var cm = other.GetComponentInParent<Crewman>();
        this.crewmanInRange.Remove(cm);
    }

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        return 0;
    }

    protected override void UpdateSystem(float currentEfficiency)
    {

    }
}