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

    private struct Heal
    {
        public Crewman man;
        public float amt;
    }

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

    private void DistributeHeal(List<Heal> heal, float efficiency, float healAmt)
    {
        // Update systems
        List<DistributionUtil<Crewman>.DistributionInput> distribInput = ListPool<DistributionUtil<Crewman>.DistributionInput>.Get();
        List<DistributionUtil<Crewman>.DistributionResult> distribOutput = ListPool<DistributionUtil<Crewman>.DistributionResult>.Get();

        // Build distrib input
        foreach (var crewman in this.crewmanInRange)
        {
            distribInput.Add(new DistributionUtil<Crewman>.DistributionInput()
            {
                obj = crewman,
                requestedAmount = crewman.model.health.maxHealth.Get() - crewman.model.health.health.Get()
            });
        }

        // Run distrib algorithm
        DistributionUtil<Crewman>.ReqDistribute(distribInput, distribOutput, healAmt);

        // Consume
        foreach (var res in distribOutput)
        {
            heal.Add(new Heal()
            {
                man = res.obj,
                amt = res.amount
            });
        }

        ListPool<DistributionUtil<Crewman>.DistributionInput>.Return(distribInput);
        ListPool<DistributionUtil<Crewman>.DistributionResult>.Return(distribOutput);
    }

    protected override float ComputeWorkLoad(float predictedEfficiency)
    {
        float healAmt = this.healPerSecond * Time.deltaTime;
        List<Heal> heal = ListPool<Heal>.Get();
        DistributeHeal(heal, predictedEfficiency, healAmt);

        float hVal = 0;
        foreach (var h in heal)
            hVal += h.amt;
        
        ListPool<Heal>.Return(heal);
        return hVal / healAmt;
    }

    protected override void UpdateSystem(float currentEfficiency)
    {
        float healAmt = this.healPerSecond * Time.deltaTime;
        List<Heal> heal = ListPool<Heal>.Get();
        DistributeHeal(heal, currentEfficiency, healAmt);

        foreach (var h in heal)
        {
            h.man.model.health.heal.Fire(h.amt);
        }

        ListPool<Heal>.Return(heal);
    }
}