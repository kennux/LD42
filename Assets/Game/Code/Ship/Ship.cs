using UnityEngine;
using UnityTK;
using System.Collections.Generic;

public class Ship : SingletonBehaviour<Ship>
{
    public MinMaxResoruce velocity;
    public MinMaxResoruce energy;
    public MinMaxResoruce oxygen;
    public float damageMitigation;
    public float drag = 2f;

    public ObservableList<ShipSystem> systems = new ObservableList<ShipSystem>(new System.Collections.Generic.List<ShipSystem>());

    public override void Awake()
    {
        base.Awake();

        this.velocity.Init();
        this.energy.Init();
        this.oxygen.Init();
    }

    public void Update()
    {
        // Update systems
        List<DistributionUtil<ShipSystem>.DistributionInput> distribInput = ListPool<DistributionUtil<ShipSystem>.DistributionInput>.Get();
        List<DistributionUtil<ShipSystem>.DistributionResult> distribOutput = ListPool<DistributionUtil<ShipSystem>.DistributionResult>.Get();

        // Build distrib input
        foreach (var system in this.systems)
        {
            distribInput.Add(new DistributionUtil<ShipSystem>.DistributionInput()
            {
                obj = system,
                requestedAmount = system.GetEnergyRequired()
            });
        }

        // Run distrib algorithm
        DistributionUtil<ShipSystem>.ReqDistribute(distribInput, distribOutput, this.energy);

        // Consume
        foreach (var res in distribOutput)
        {
            // Debug.Log("Ship system " + res.obj + " is consuming " + res.amount + " of " + res.obj.GetEnergyRequired());
            res.obj.ConsumeEnergy(res.amount);
        }

        ListPool<DistributionUtil<ShipSystem>.DistributionInput>.Return(distribInput);
        ListPool<DistributionUtil<ShipSystem>.DistributionResult>.Return(distribOutput);

        // Simulate drag
        this.velocity.value -= this.drag * Time.deltaTime;
    }

    /// <summary>
    /// Spawns a damage rip.
    /// </summary>
    public void SpawnRip(Vector3 closestTo, float maxDist = 5f)
    {

    }
}