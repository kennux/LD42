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

    public HullBreach hullBreachPrefab;
    public HullBreachAnchor[] hullBreachAnchors;

    [System.Serializable]
    public class HullBreachAnchor
    {
        public Transform anchor;
        public HullBreach occupant;
    }

    public ObservableList<ShipSystem> systems = new ObservableList<ShipSystem>(new System.Collections.Generic.List<ShipSystem>());

    public override void Awake()
    {
        base.Awake();

        this.velocity.Init();
        this.energy.Init();
        this.oxygen.Init();
    }

    public void FixedUpdate()
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
        this.velocity.value -= this.drag * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Spawns a hull breach.
    /// </summary>
    public void SpawnHullBreach(Vector3 closestTo, float maxDist = 5f)
    {
        // Find closest anchor without occupation in range.
        List<HullBreachAnchor> anchors = ListPool<HullBreachAnchor>.Get();

        foreach (var a in this.hullBreachAnchors)
            if (Vector3.Distance(closestTo, a.anchor.position) <= maxDist && Essentials.UnityIsNull(a.occupant))
                anchors.Add(a);

        float closestDist = float.PositiveInfinity;
        HullBreachAnchor closestAnchor = null;
        foreach (var a in anchors)
        {
            float d = Vector3.Distance(closestTo, a.anchor.position);
            if (d < closestDist)
            {
                closestDist = d;
                closestAnchor = a;
            }
        }

        if (!ReferenceEquals(closestAnchor, null))
        {
            // Spawn!
            var hullBreachGo = Instantiate(this.hullBreachPrefab.gameObject, closestAnchor.anchor.position, this.hullBreachPrefab.transform.rotation);
            var hullBreach = hullBreachGo.GetComponent<HullBreach>();
            closestAnchor.occupant = hullBreach;
        }

        ListPool<HullBreachAnchor>.Return(anchors);
    }
}